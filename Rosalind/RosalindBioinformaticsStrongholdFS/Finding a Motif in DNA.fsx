﻿#load "Seq.fs"
#load "Nucleotides.fs"
open Nucleotides

let findMotifInDna s t = 
    Seq.findAlli (Dna.lex t) (Dna.lex s)
    |> Seq.map (fun t -> fst t)

findMotifInDna "GATATATGCATATACTT" "ATAT" 
|> Seq.print
//  2 4 10

findMotifInDna "CGTTAGGACTTAGGACTGCTATTAGGACTCATTAGGACGGGTTAGGACCAAAGTTAGGACAGCTTAGGACACGGAGCCTTCATTAGGACTTAGGACGAGGTCCTCTTAGGACCTTGCAAAGGGTACTTTAGGACCGTCCATTAGGACATTAGGACTTAGGACCTCTTAGGACTTAGGACCCAGGAGTTAGGACTTAGGACCATTAGGACCGGATCGTTAGGACTTAGGACAGGTATTTACAGTTTAGGACTTAGGACTTAGGACCGTTAGGACTTAGGACACTTAGGACCTGTTAGGACCGAGGACAACTTAGGACTTAGGACCGTAGTTAGGACGTTAGGACCTTAGGACGATTAGGACTTAGGACTTAGGACTTAGGACCTCTTTAGGACTTTAGGACGTTAGGACATTAGGACTTAGGACGTTAGGACCTTAGGACGTGCCATTAGGACTTAGGACTTAGGACTATTAGGACTACTTAGGACCGCTTAGGACTTAGGACTATTTAGGACTTTTAGGACGTTAGGACGTTTAGGACTTAGGACCACATTAGGACGACAATCCTTAGGACGTTAGGACATTAGGACCGGGCCTTAGGACCGGTATTTAGGACATTAGGACGTTTAGGACTTTAGGACGTTAGGACATCTTTAGGACCATTAGGACGGTTAGGACCATTAGGACCTTTAGGACGATTAGGACCTTAGGACTTAGGACGGAATTAGGACTATTAGGACTTAGGACCTTAGGACTTAGGACCGCTTAGGACCGTGGTTAGGACAGTTTTTTAGGACTTAGGACCACACAGATTAGGACTTAGGACTCTTAGGACCCTTAGGACGATTAGGACACGGAATTAGGACTTAGGACTTAGGACACTTAGGACTTAGGACCGTATTAGGACATTAGGACCAC" "TTAGGACTT"
|> Seq.print
//  3 83 149 166 187 217 244 251 267 310 354 361 368 386 410 446 453 489 506 532 624 704 731 746 788 810 857 864 880
