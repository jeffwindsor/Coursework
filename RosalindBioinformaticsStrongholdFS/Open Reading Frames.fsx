#load "Files.fs"
#load "Collections.fs"
#load "Cellular.fs"
open Rosalind
open Nucleotides
 
// Transcribe each DNA strand: original and reverse compliment
// Translate each reading frame: 3 nucleotides per codon = 3 reading frames
// Extract all viable protiens that start with M (start codon amino acid) and end with Stop
let dnaStrand = 
    "Open Reading Frames.txt"
    |> Files.Fasta.read
    |> Seq.map Dna.lex

let answer = 
    Seq.append dnaStrand (dnaStrand |> Seq.map Dna.toReverseComplement)
    |> Seq.map     Dna.CodingStrand.toRna
    |> Seq.collect Rna.ReadingFrames.toAminoAcids
    |> Seq.collect AminoAcids.Extract.validProteins

//Format for Submission
answer |> Seq.printItemsAsLines