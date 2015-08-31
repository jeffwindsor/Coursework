module Problem1

open NUnit.Framework
open FsUnit

type ``Given the natural numbers below one thousand that are multiples of 3 or 5`` ()=
    let naturalNumber = [for i in 1..999 do if i % 3 = 0 || i % 5 = 0 then yield i]

    [<Test>] member test.
        ``Find the sum`` ()=
            List.sum naturalNumber
            |> should equal 233168

