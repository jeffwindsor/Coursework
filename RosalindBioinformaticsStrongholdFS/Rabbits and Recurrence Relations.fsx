#load "Collections.fs"
open Bioinformatics

// Adults Breed (k) new borns each cycle
// New Borns become adults after one cycle
type Ecosystem = {Babies:int64; Adults:int64}
let cycle breedRate eco = 
    let population = eco.Babies + eco.Adults
    Some (population, {Babies=(eco.Adults*breedRate); Adults=population})
    
let answer n k = 
    Seq.Fibonacci.item (cycle k) {Babies=1L; Adults=0L} n

//Problem: 31 2L -> 715827883L
answer 31 2L |> printfn "%i"
