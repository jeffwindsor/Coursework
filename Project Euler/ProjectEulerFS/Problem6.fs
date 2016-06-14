module Problem6

open NUnit.Framework
open FsUnit

let square x = x * x
let sumOfSquares = List.map square >> List.sum
let squareOfSums = List.sum >> square

type ``Given the first one hundred natural numbers`` ()=
    let numbers = [1..100]

    [<Test>] member test.
        ``What is the difference between the sum of the squares and square of sums`` ()=
            (squareOfSums numbers) - (sumOfSquares numbers)
            |> should equal 25164150

