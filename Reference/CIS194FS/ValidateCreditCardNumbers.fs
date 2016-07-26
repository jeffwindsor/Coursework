namespace CIS194

module ValidateCreditCardNumbers =
    let private toDigits xs =
        [0; 1; 2; 3]

    let private doubleEvenIndex i x =
        match i % 2 with
        | 0 ->  x * 2
        | _ -> x

    let validate cc =
        toDigits cc
        |> List.rev
        |> List.mapi doubleEvenIndex
        |> List.sum
        |> fun x -> x % 10 = 0
        
