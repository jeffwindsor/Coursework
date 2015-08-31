module Problem5

open NUnit.Framework
open FsUnit

let rec gcd a b =
    match b with
    | 0 ->  abs a
    | _ -> gcd b (a % b)

let accumulate acc b = acc * (b / (gcd acc b))

type ``Given all of the numbers from 1 to 20`` ()=
    let numbers = [1..20]

    [<Test>] member test.
        ``What is the smallest positive number that is evenly divisible by all`` ()=
            Seq.fold accumulate 1 numbers 
            |> should equal 232792560

 

