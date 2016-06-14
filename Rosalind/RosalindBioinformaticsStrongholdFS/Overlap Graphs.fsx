#load "Files.fs"
#load "Collections.fs"
open Bioinformatics

type Node = {Id: string; Prefix:string; Suffix:string; All:string}
let overlap (a, b) =
    //printfn "%s %s %s %s" a.Id a.Suffix b.Prefix b.Id
    match a,b with
    | a,b when a.Id = b.Id -> None
    | a,b when a.All = b.All -> None
    | a,b when a.Suffix = b.Prefix -> Some (a.Id, b.Id)
    | _,_ -> None

let toNode k (fasta: Files.Fasta.Entry<string>) = 
    let l = k-1
    let e = (String.length fasta.Value) - 1
    {
        Id      = fasta.Id 
        All     = fasta.Value
        Prefix  = fasta.Value.[0..l]
        Suffix  = fasta.Value.[(e-l)..e]
    }

let answer k =
    let nodes = 
        "Overlap Graphs.txt"
        |> Files.Fasta.readAsEntries 
        |> Seq.map (toNode k)
    
    Seq.cartesian nodes nodes
    |> Seq.choose overlap
    |> Seq.iter (fun (a,b) -> printfn "%s %s" a b)

answer 3
//=========================================
//INPUTS
//=========================================
//>Rosalind_0498
//AAATAAA
//>Rosalind_2391
//AAATTTT
//>Rosalind_2323
//TTTTCCC
//>Rosalind_0442
//AAATCCC
//>Rosalind_5013
//GGGTGGG
//=========================================
//Expected
//=========================================
//Rosalind_0498 Rosalind_2391
//Rosalind_0498 Rosalind_0442
//Rosalind_2391 Rosalind_2323
//=========================================
