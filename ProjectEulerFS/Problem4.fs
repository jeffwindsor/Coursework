module Problem4

open NUnit.Framework
open FsUnit

let isPalendrome n = 
    let s = n.ToString().ToCharArray()
    s = (Array.rev s)

type ``Given all products of two 3-digit numbers`` ()=
    let productsOfTwoThreeDigitNumbersHighToLow = 
        seq{
            for a in 999..-1..100 do
                for b in a..(-1)..100 do
                    yield a * b
        }

    [<Test>] member test.
        ``What is the largest palindrome`` ()=
            Seq.filter isPalendrome productsOfTwoThreeDigitNumbersHighToLow
            |> Seq.max 
            |> should equal 906609