#load "Files.fs"
#load "Collections.fs"
#load "Cellular.fs"
open Rosalind

// Palindromes from 4 to 12 chars
// Idea: start at char 4 compare left and right 
//      at anytime mis if s found or reach 12 chars
//      record if between 4-12 long (2-6 each way)
//      move char tothe right

let reversePalindromeLength xs= 
    let l = (Array.length xs)
    let i = (l - 1) / 2
    match xs with
    | [||] -> None
    | _ when l % 2 <> 0 -> None
    | _   -> 
        let left  = xs.[..i] |> Array.rev
        let right = xs.[(i + 1)..] |> Dna.toComplement |> Seq.toArray
        Seq.zip left right
        |> Seq.fold (fun (i,on) (l,r) ->
            match on with
            | false                            //off
            | true when l<>r -> (i,false)      //turn off
            | true           -> ((i+1),true)   //set last match index
            ) (0,false)
        |> fst
        |> fun i -> if i >= 4 then Some i else None

let dna = "TCAATGCATGCGGGTCTATATGCAT" |> Dna.lex
let dnas = sub segments up to 12  @ (dna |> Seq.windowed 12)


|> Seq.mapi (fun i xs -> (i, (reversePalindromeLength xs)))
|> Seq.filter (fun (_, n) -> match n with | Some _ -> true | _ -> false)
//|> Seq.iteri (fun i xs -> printf "%3i: " i; Seq.printn xs; )

