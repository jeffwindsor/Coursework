#load "..\Collections.fs"
#load "..\Files.fs"
open Bioinformatics

let addEdge (verticies: int array) a b =
    verticies.[a-1] <- verticies.[a-1] + 1
    verticies.[b-1] <- verticies.[b-1] + 1
    verticies

let lines = 
    @"AlgorithmicHeights\Degree Array.txt"
    |> Files.toLinesFromProjectFile
    |> Seq.toArray
    |> Seq.map String.toInts

let header = lines |> Seq.head
let verticies, edges = header.[0], header.[1]
let verticiePairs = lines |> Seq.tail

verticiePairs
|> Seq.fold (fun acc x -> addEdge acc x.[0] x.[1]) (Array.create verticies 0)
|> Seq.iter (printf "%i ")