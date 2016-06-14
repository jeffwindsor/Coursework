#load "Files.fs"
#load "Collections.fs"
#load "Cellular.fs"
open Bioinformatics
open Nucleotides

type ProfileNucleotide = { Nucleotide: char; mutable Count:int;}
let newPN n c = {Nucleotide = n; Count = c}
let accumulatePN t1 t2 = t1.Count <- (t1.Count + t2.Count)
let isNucleotide n np =
    if np.Nucleotide = n then Some([np.Count]) else None

let newProfile a t c g = 
    [(newPN 'A' a); (newPN 'C' c); (newPN 'G' g); (newPN 'T' t)]
let accumulateProfile p1 p2 = List.iter2 accumulatePN p1 p2       
let toProfile = function
    | A -> newProfile 1 0 0 0
    | T -> newProfile 0 1 0 0
    | C -> newProfile 0 0 1 0
    | G -> newProfile 0 0 0 1
    | _ -> failwith "Not DNA Nucleotide"
let toProfiles xs = Seq.map toProfile xs

let consensus ps =
    let find p =
        let c = p |> List.sortByDescending (fun pn -> pn.Count) |> List.head
        c.Nucleotide
    ps |> List.map find

let profile fastaFileName = 
    let profiles = 
        Files.Fasta.read fastaFileName
        |> Seq.map Nucleotides.Dna.lex 
        |> Seq.map toProfiles
    let totals = profiles |> Seq.head |> Seq.toList  
    //Accumulate Profile Totals - using first profile to avoid looking up length
    Seq.iter (Seq.iter2 accumulateProfile totals) (profiles |>  Seq.skip 1)
    totals


//=============================
//Print Answer
//  ATGCAACT
//  A: 5 1 0 0 5 5 0 0
//  C: 0 0 1 4 2 0 6 1
//  G: 1 1 6 3 0 1 0 0
//  T: 1 5 0 0 0 1 1 6
//=============================    
let ps = profile "10_Consensus and Profile.txt"
consensus ps |> System.String.Concat |> printfn "%s"  
['A';'C';'G';'T']
|> List.iter (fun n -> 
    printf "%c: " n
    List.collect (List.pick (isNucleotide n)) ps 
    |> List.iter (printf "%i ")
    printfn ""
    )
