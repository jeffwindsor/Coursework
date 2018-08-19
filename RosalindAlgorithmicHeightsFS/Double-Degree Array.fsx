#load "..\Collections.fs"
#load "..\Files.fs"
open Bioinformatics

let lines = 
    @"AlgorithmicHeights\Double-Degree Array.txt"
    |> Files.toLinesFromProjectFile
    |> Seq.toArray
    |> Seq.map String.toInts
let header = lines |> Seq.head
let vCount, eCount = header.[0], header.[1]
let verticiePairs = lines |> Seq.tail

let addEdge (verticies: int list array) a b =
    verticies.[a-1] <- b::verticies.[a-1]
    verticies.[b-1] <- a::verticies.[b-1]
    verticies

let sumVertices (verticies: int list array) verticie =
    verticie
    |> List.map (fun i -> List.length verticies.[i])
    |> List.sum

let verticies = 
    verticiePairs
    |> Seq.fold (fun acc x -> addEdge acc x.[0] x.[1]) (Array.create vCount [])


verticies
|> Array.map (sumVertices verticies)     
|> Seq.iter (printf "%i ")