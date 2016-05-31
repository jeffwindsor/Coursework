#load "..\Collections.fs"
#load "..\Files.fs"
open Bioinformatics

let insertionSortSwapCount (arr : 'a []) =
    let mutable c = 0
    for i = 1 to arr.Length - 1 do
        let mutable j = i
        while j >= 1 && arr.[j] < arr.[j-1] do
            Array.swap j (j-1) arr
            c <- c + 1
            j <- j - 1
    (arr, c)

"AlgorithmicHeights\Insertion Sort.txt"
|> Files.toLinesFromProjectFile
|> Seq.head
|> String.toInts
|> insertionSortSwapCount
|> snd
|> (printf "%i")