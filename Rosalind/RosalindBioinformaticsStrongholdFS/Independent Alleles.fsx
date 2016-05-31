let binomial n k =
    List.fold (fun s i -> s * ((float n)-i+1.0)/i ) 1.0 [1.0..(float k)]

let bernoulli offspring p k n =
    let k' = pown offspring k
    (binomial k' n) * (pown p n) * (pown (1.0 - p) (k' - n))

let independentAlleles offspring p k n =
    1.0 - (seq { for i in 0 .. (n-1) do yield bernoulli offspring p k i }
            |> Seq.sum)

// number of offsping is 2
// prob AaBb in any generation is 1/4
// k and n given by datase
independentAlleles 2 0.25 5 9

// 2,1 -> 0.684
// 5,9 -> 0.4064883485