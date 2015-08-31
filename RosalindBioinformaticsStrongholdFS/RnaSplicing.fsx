#load "Files.fs"
#load "Collections.fs"
#load "Cellular.fs"
open Rosalind
open Rna

// Load Lines, First line = DNA, Rest = exons
// Remove exons from DNA (jeff's note: order desc by length just in case a small exon is contained in a large exon)
// Resulting DNA as coding string -> RNA -> AminoAcids using Reading Frames -> Extract Valid Proteins 
let answer =
    let values = "RnaSplicing.txt" |> Files.Fasta.read
    let dna   = values |> Seq.head
    let exons = values |> Seq.tail

    splice dna exons
    |> Dna.lex
    |> Dna.CodingStrand.toRna
    |> Rna.toAminoAcids
    |> AminoAcids.Extract.largestValidProtein

//Problem sample -> MVYIADKQHVASREAYGHMFKVCA
answer |> Seq.printAsLine