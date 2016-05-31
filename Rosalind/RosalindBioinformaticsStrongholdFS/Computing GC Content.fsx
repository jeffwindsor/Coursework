#load "Files.fs"
#load "Cellular.fs"
open Bioinformatics
open Nucleotides

let gcPercent ns =
    let num = ns |> Seq.filter (function | C | G -> true | _ -> false) |> Seq.length
    let den = ns |> Seq.length
    (float num) / (float den) * 100.0

let answer =
    "Computing GC Content.txt"
    |> Files.Fasta.readAsEntries
    |> Files.Fasta.Entries.mapValues (Dna.lex >> gcPercent)
    |> Seq.maxBy (fun fe -> fe.Value)

//Problem -> Rosalind_9852 52.259560
answer |> Files.Fasta.Entries.print
