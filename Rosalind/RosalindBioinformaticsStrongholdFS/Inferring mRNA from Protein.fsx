//http://rosalind.info/problems/mrna/
// don't understand question yet

let cmod a b n = (a % n) = (b % n)
let cmodAdd a b c d n = ((a + c) % n) = ((b + d) % n)
let cmodMul a b c d n = ((a * c) % n) = ((b * d) % n)

let testRelationship a b c d n =
    if (cmod a b n) = (cmod c d n) = true
    then (cmodAdd a b c d n) = (cmodMul a b c d n) = true
    else false

testRelationship 29 73 10 32 11