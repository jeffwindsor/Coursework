module Problem18
    
open NUnit.Framework
open FsUnit

//Actionable Code
let maxSumTriangle t = 
    let dropRight = List.rev >> List.tail >> List.rev
    let maxSum n n1 = List.map2 max (dropRight n1 |> List.map2 (+) n) (List.tail n1 |> List.map2 (+) n )
    let rec inner t =
        match t with
        |   []              -> 0            //FAIL
        |   n :: []         -> List.max n   //Return Maximum sum in list
        |   n1 :: n :: tail -> inner ((maxSum n n1)::tail)
    inner t    

//Tests
type ``Given the problem triangle`` ()=
    let triangle = [
        [75;];
        [95; 64;];
        [17; 47; 82;];
        [18; 35; 87; 10;];
        [20; 04; 82; 47; 65;];
        [19; 01; 23; 75; 03; 34;];
        [88; 02; 77; 73; 07; 63; 67;];
        [99; 65; 04; 28; 06; 16; 70; 92;];
        [41; 41; 26; 56; 83; 40; 80; 70; 33;];
        [41; 48; 72; 33; 47; 32; 37; 16; 94; 29;];
        [53; 71; 44; 65; 25; 43; 91; 52; 97; 51; 14;];
        [70; 11; 33; 28; 77; 73; 17; 78; 39; 68; 17; 57;];
        [91; 71; 52; 38; 17; 14; 91; 43; 58; 50; 27; 29; 48;];
        [63; 66; 04; 68; 89; 53; 67; 30; 73; 16; 69; 87; 40; 31;];
        [04; 62; 98; 27; 23; 09; 70; 98; 73; 93; 38; 53; 60; 04; 23]
    ]

    [<Test>] member test.
        ``When I find the maximum total from top to bottom is 1074`` ()=
            maxSumTriangle (List.rev triangle) |> should equal 1074

[<TestFixture>] 
type ``Given a simple triangle`` ()=
    let triangle = [
        [0001];
        [0009;0010];
        [0009;0100;0001]
        [0009;1000;0009;0001]
    ]

    [<Test>] member test.
        ``When I find the maximum total from top to bottom is 1111`` ()=
            maxSumTriangle (List.rev triangle) |> should equal 1111