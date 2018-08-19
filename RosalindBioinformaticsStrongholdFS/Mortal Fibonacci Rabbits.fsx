#load "Collections.fs"
open Bioinformatics

// Adults die after (m) cycles
// New Borns become adults after one cycle
type Ecosystem = {Babies:int64; Adults:int64; QueueOfDeath:int64 list}    
let ecosystem births fromPopulation withQueueOfDeath = 
    {
        Babies = births
        Adults = fromPopulation - (List.head withQueueOfDeath)
        QueueOfDeath = (List.tail withQueueOfDeath)@[births]
    }
let cycle s = 
    let population = s.Babies + s.Adults
    Some (population, (ecosystem s.Adults population s.QueueOfDeath))

let answer n m = 
    let eco = ecosystem 1L 0L ([1..m] |> List.map (fun i -> 0L))  //initial
    Seq.Fibonacci.item cycle eco n

//Problem 6 3   -> 4
answer 6 3   |> printfn "%i"
//Problem 81 16 -> 37378265960028808L
answer 81 16 |> printfn "%i"
