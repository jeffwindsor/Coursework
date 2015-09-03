#load "Collections.fs"
open Bioinformatics

let n = 7

let ps = 
    [1..n]
    |> List.permutations

ps |> Seq.length  |> printfn "%i" 
ps |> Seq.iter (fun xs -> Seq.iter (printf "%A ") xs; printfn "")
