module Problem2

open NUnit.Framework
open FsUnit
open System.Numerics 

type ``Given the terms in the Fibonacci sequence`` ()=
    let fibonacci = Seq.unfold(fun (a,b) -> Some(a, (b, a+b))) (0, 1)        //Infinite fibonacci sequence
    let isEven a = (a % 2 = 0)
    let doesNotExceedFourMillion a = (a <= 4000000)

    [<Test>] member test.
        ``Find the sum of the even-valued terms whose values do not exceed four million`` ()=
            fibonacci
            |> Seq.filter(isEven)
            |> Seq.takeWhile(doesNotExceedFourMillion)
            |> Seq.sum
            |> should equal 4613732