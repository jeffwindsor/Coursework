
let expectedValue n probFunc =
    [1..n]
    |> List.map probFunc
    //|> List.sum

let urvExpectdValue min max =
    (min + max) / 2.0

//Parent Positions
//AA-AA  1 + 1 / 2 = 1
//AA-Aa  1 + 1 / 2 = 1
//AA-aa  1 + 1 / 2 = 1
//Aa-Aa  1 + 0.5 / 2 = 0.75
//Aa-aa  1 + 0 / 2 = 0.5
//aa-aa  0 + 0 / 2 = 0
let probabilitiesOfDominance = [1.0; 1.0; 1.0; 0.75; 0.5; 0.0]
let expectedChildrenWithDominance i couples childrenPerCouple =
    let r = couples * childrenPerCouple * (probabilitiesOfDominance |> List.item i)
    printfn "%A %A %A %A = %A" i couples childrenPerCouple (probabilitiesOfDominance |> List.item i) r
    r

let calculatingExpectedDominance a b c d e f =
    [(float a);(float b);(float c);(float d);(float e);(float f);]
    |> List.mapi (fun i couples -> expectedChildrenWithDominance i couples 2.0)
    |> List.sum 

calculatingExpectedDominance 17149 17001 16173 18291 17667 17789
|> printfn "%f"

// 1 0 0 1 0 1 -> 3.5
// 19111 19710 17020 17912 17943 17507-> 156493.000000
// 17149 17001 16173 18291 17667 17789 -> 145749.5