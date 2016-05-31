#load "..\Files.fs"
#load "..\Cellular.fs"
#load "..\Collections.fs"
open Bioinformatics
open Dna

    type Node = { Nuc: Nucleotide; mutable MaxId:int; Edges: Node option array}
    type Trie = {MaxId:int; Edges: Node option array}
    let private edgeIndex = 
        function | A -> 0 | T -> 1 | C -> 2 | G -> 3

    let private withSuffixes f id dna node =
        dna
        |> List.fold (fun (t,d) _ -> f id d t; (t, List.tail d) ) (node, dna)
        |> fst

    let rec private add id dna (edges: Node option array) =
        match dna with
        | []         -> ()
        | nuc::tail ->
            match edges.[edgeIndex nuc] with
            | Some node -> 
                add id tail node.Edges
            | None   ->
                let n = { Nuc = nuc; MaxId = id; Edges = Array.create 4 None }
                edges.[edgeIndex nuc] <- (Some n)
                add id tail n.Edges

    let rec private intersect id dna (edges: Node option array) =
        match dna with
        | [] -> ()
        | nuc::tail ->
            match edges.[edgeIndex nuc] with
            // If nucletide has a trie associated with it and was matched by last string id, return
            | Some node when node.MaxId >= (id - 1) -> 
                node.MaxId <- id
                intersect id tail node.Edges
            | _  -> ()  // Do nothing

    let private buildTrie strings =
        let startIndex = 0
        let edges = Array.create 4 None

        strings 
        |> Seq.sortBy String.length
        |> Seq.map Dna.lex
        |> Seq.map Seq.toList

        |> Seq.fold (fun (i, edges) dna -> 
            if i = startIndex then
                //Build initial from shortest dna seq
                ( (i+1), withSuffixes add i dna edges)
            else
                ( (i+1), withSuffixes intersect i dna edges)
            ) (startIndex, edges)
        |> fun (index, edges) -> {MaxId = (index-1); Edges = edges }

    let private findLongestCommonStrings trie =
        let maxId = trie.MaxId
        let rec inner (path:Nucleotide list) (edges: Node option array) (result:Nucleotide list list) =
            let fold r (edge:Node option) :Nucleotide list list = 
                match edge with
                | Some node when node.MaxId = maxId ->
                    inner (node.Nuc::path) node.Edges r
                | _ -> //End of substring - check for longest 
                    let rLen = r |> List.head |> List.length
                    let substring = path |> List.rev
                    match List.length path with
                    | pLen when rLen = pLen -> substring::r |> List.distinct //same length append to result
                    | pLen when rLen < pLen -> [substring] //larger so new result list of that length
                    | _                     -> r //smaller = no change

            edges |> Array.filter (fun x -> x <> None) |> Array.fold fold result

        inner [] trie.Edges [[]]


    let longestCommonStrings strings = 
        strings 
        |> buildTrie 
        |> findLongestCommonStrings

let answer = 
    "Finding a Shared Motif.txt"
    |> Files.Fasta.read
    |> longestCommonStrings

answer |> Seq.printn

