#load "..\Collections.fs"
#load "..\Files.fs"
open Rosalind

let lines = 
    @"AlgorithmicHeights\Binary Search.txt"
    |> Files.toLinesFromProjectFile
    |> Seq.toArray

let n = int lines.[0]
let m = int lines.[1]
let indexes = String.toInts lines.[2]
let lookups = String.toInts lines.[3]

assert(n = Array.length indexes)
assert(m = Array.length lookups)

lookups
|> Array.map (Array.binarySearch indexes)
|> Array.iter (function 
               | Some i -> printf "%i " (i + 1) 
               | None -> printf "-1 " )
