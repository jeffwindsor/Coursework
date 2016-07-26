open System.Linq

let show x = printf "%A" x

// [snippet: (*) Problem 11 :  Modified run-length encoding.]
/// Modify the result of problem 10 in such a way that if an element has no duplicates it 
/// is simply copied into the result list. Only elements with duplicates are transferred as
/// (N E) lists.
///  
/// Example: 
/// * (encode-modified '(a a a a b c c a a d e e e e))
/// ((4 A) B (2 C) (2 A) D (4 E))
///  
/// Example in F#: 
/// 
/// > encodeModified <| List.ofSeq "aaaabccaadeeee"
/// val it : char Encoding list =
///   [Multiple (4,'a'); Single 'b'; Multiple (2,'c'); Multiple (2,'a');
///    Single 'd'; Multiple (4,'e')]

type 'a Encoding = 
    Multiple of int * 'a 
    | Single of 'a

let pack xs =
    let collect last = function
        | (current::xs)::xss when last = current -> (last::current::xs)::xss
        | xss -> [last]::xss
    List.foldBack collect xs []

let encode xs =
    xs 
    |> pack 
    |> List.map (fun xs ->
        match (List.length xs, List.head xs) with
        | 1, x -> Single(x)
        | n, x -> Multiple(n,x))
 
let encodeTest0 = (encode <| List.ofSeq "aaaabccaadeeee") =  [Multiple (4,'a'); Single 'b'; Multiple (2,'c'); Multiple (2,'a'); Single 'd'; Multiple (4,'e')]

// [snippet: (**) Problem 12 : Decode a run-length encoded list.]
/// Given a run-length code list generated as specified in problem 11. Construct its 
/// uncompressed version.
///  
/// Example in F#: 
/// 
/// > decodeModified 
///     [Multiple (4,'a');Single 'b';Multiple (2,'c');
///      Multiple (2,'a');Single 'd';Multiple (4,'e')];;
/// val it : char list =
///   ['a'; 'a'; 'a'; 'a'; 'b'; 'c'; 'c'; 'a'; 'a'; 'd'; 'e'; 'e'; 'e'; 'e']
let decode xs = 
    let inner acc = function
        | Multiple(n, x) -> acc @ List.replicate n x 
        | Single x  -> acc @ [x]
    List.fold inner [] xs

let decodeTest = decode [Multiple (4,'a');Single 'b';Multiple (2,'c'); Multiple (2,'a');Single 'd';Multiple (4,'e')] = ['a'; 'a'; 'a'; 'a'; 'b'; 'c'; 'c'; 'a'; 'a'; 'd'; 'e'; 'e'; 'e'; 'e']

// [snippet: (**) Problem 13 : Run-length encoding of a list (direct solution).]
/// Implement the so-called run-length encoding data compression method directly. I.e. 
/// don't explicitly create the sublists containing the duplicates, as in problem 9, 
/// but only count them. As in problem P11, simplify the result list by replacing the 
/// singleton lists (1 X) by X.
///  
/// Example: 
/// * (encode-direct '(a a a a b c c a a d e e e e))
/// ((4 A) B (2 C) (2 A) D (4 E))
///  
/// Example in F#: 
/// 
/// > encodeDirect <| List.ofSeq "aaaabccaadeeee"
/// val it : char Encoding list =
///   [Multiple (4,'a'); Single 'b'; Multiple (2,'c'); Multiple (2,'a');
///    Single 'd'; Multiple (4,'e')]
(*
let encodeDirect xs =
    let collect x = function
        | [] -> [Single x]  //empty acc
        | Single y::xs when x=y -> Multiple(2,x)::xs  //single to multiple when matching
        | Single _::_ as xs -> Single x::xs
        | Multiple(n,y)::xs when y = x -> Multiple(n + 1, x)::xs
        | xs -> Single x::xs
    List.foldBack collect xs [] 

let encodeDirectTest0 = 
    let x = (encodeDirect <| List.ofSeq "aaaabccaadeeee") 
    printf "%A" x
    x = [Multiple (4,'a'); Single 'b'; Multiple (2,'c'); Multiple (2,'a'); Single 'd'; Multiple (4,'e')]
*)
// [snippet: (*) Problem 14 : Duplicate the elements of a list.]
/// Example: 
/// * (dupli '(a b c c d))
/// (A A B B C C C C D D)
///  
/// Example in F#: 
/// 
/// > dupli [1; 2; 3]
/// [1;1;2;2;3;3]
let duplicate xs = 
    List.fold (fun acc x -> acc @ [x;x]) [] xs

let duplicateTest0 = 
    let actual = duplicate [1; 2; 3] 
    printf "%A" actual
    actual = [1;1;2;2;3;3]

// [snippet: (**) Problem 15 : Replicate the elements of a list a given number of times.]
/// Example: 
/// * (repli '(a b c) 3)
/// (A A A B B B C C C)
///  
/// Example in F#: 
/// 
/// > repli (List.ofSeq "abc") 3
/// val it : char list = ['a'; 'a'; 'a'; 'b'; 'b'; 'b'; 'c'; 'c'; 'c']
let replicate xs n = 
    //List.fold (fun acc x -> acc @ List.replicate n x) [] xs
    List.collect (List.replicate n) xs

let replicateTest0 = 
    let actual = replicate (List.ofSeq "abc") 3
    printf "%A" actual
    actual = ['a'; 'a'; 'a'; 'b'; 'b'; 'b'; 'c'; 'c'; 'c']


// [snippet: (**) Problem 16 : Drop every N'th element from a list.]
/// Example: 
/// * (drop '(a b c d e f g h i k) 3)
/// (A B D E G H K)
///  
/// Example in F#: 
/// 
/// > dropEvery (List.ofSeq "abcdefghik") 3;;
/// val it : char list = ['a'; 'b'; 'd'; 'e'; 'g'; 'h'; 'k']
let dropEvery n xs =
    xs
    |> List.mapi (fun i x -> (x, (i+1) % n <> 0))
    |> List.choose (function | (x,true) -> Some(x) | _ -> None)
    
let dropEveryTest0 = 
    let actual = dropEvery 3 (List.ofSeq "abcdefghik")
    printf "%A" actual
    actual = ['a'; 'b'; 'd'; 'e'; 'g'; 'h'; 'k']

// [snippet: (*) Problem 17 : Split a list into two parts; the length of the first part is given.]
/// Do not use any predefined predicates. 
/// 
/// Example: 
/// * (split '(a b c d e f g h i k) 3)
/// ( (A B C) (D E F G H I K))
///  
/// Example in F#: 
/// 
/// > split (List.ofSeq "abcdefghik") 3
/// val it : char list * char list =
///   (['a'; 'b'; 'c'], ['d'; 'e'; 'f'; 'g'; 'h'; 'i'; 'k'])
let split xs n =
    let rec drop n xs =
        match xs,n with
            | xs,0 -> xs
            | [],_ -> []
            | _::xs,n -> drop (n-1) xs
    match List.length xs <= n with
    | true  -> (xs,[])
    | false -> (List.take n xs, drop n xs)

let splitTest0 = 
    let actual = split (List.ofSeq "abcdefghik") 3
    printf "%A" actual
    actual = (['a'; 'b'; 'c'], ['d'; 'e'; 'f'; 'g'; 'h'; 'i'; 'k'])

// [snippet: (**) Problem 18 : Extract a slice from a list.]
/// Given two indices, i and k, the slice is the list containing the elements between the 
/// i'th and k'th element of the original list (both limits included). Start counting the 
/// elements with 1.
///  
/// Example: 
/// * (slice '(a b c d e f g h i k) 3 7)
/// (C D E F G)
///  
/// Example in F#: 
/// 
/// > slice ['a';'b';'c';'d';'e';'f';'g';'h';'i';'k'] 3 7;;
/// val it : char list = ['c'; 'd'; 'e'; 'f'; 'g']
let slice xs i k =
    let rec drop n xs =
        match xs,n with
            | xs,0 -> xs
            | [],_ -> []
            | _::xs,n -> drop (n-1) xs
    List.take (k-i+1) (drop (i-1) xs)

let sliceTest0 = 
    let actual = slice (List.ofSeq "abcdefghik") 3 7
    printf "%A" actual
    actual = ['c'; 'd'; 'e'; 'f'; 'g']

// [snippet: (**) Problem 19 : Rotate a list N places to the left.]
/// Hint: Use the predefined functions length and (@) 
/// 
/// Examples: 
/// * (rotate '(a b c d e f g h) 3)
/// (D E F G H A B C)
/// 
/// * (rotate '(a b c d e f g h) -2)
/// (G H A B C D E F)
///  
/// Examples in F#: 
/// 
/// > rotate ['a';'b';'c';'d';'e';'f';'g';'h'] 3;;
/// val it : char list = ['d'; 'e'; 'f'; 'g'; 'h'; 'a'; 'b'; 'c']
///  
/// > rotate ['a';'b';'c';'d';'e';'f';'g';'h'] (-2);;
/// val it : char list = ['g'; 'h'; 'a'; 'b'; 'c'; 'd'; 'e'; 'f']
let rotate xs n =
    let xsl = List.length xs 
    let f,b = split xs (abs (xsl + n % xsl))
    b @ f
        
let rotateTest0 = rotate ['a';'b';'c';'d';'e';'f';'g';'h'] 3 =  ['d'; 'e'; 'f'; 'g'; 'h'; 'a'; 'b'; 'c']
let rotateTest1 = rotate ['a';'b';'c';'d';'e';'f';'g';'h'] (-2) = ['g'; 'h'; 'a'; 'b'; 'c'; 'd'; 'e'; 'f']

// [snippet: (*) Problem 20 : Remove the K'th element from a list.]
/// Example in Prolog: 
/// ?- remove_at(X,[a,b,c,d],2,R).
/// X = b
/// R = [a,c,d]
///  
/// Example in Lisp: 
/// * (remove-at '(a b c d) 2)
/// (A C D)
///  
/// (Note that this only returns the residue list, while the Prolog version also returns 
/// the deleted element.)
///  
/// Example in F#: 
/// 
/// > removeAt 1 <| List.ofSeq "abcd";;
/// val it : char * char list = ('b', ['a'; 'c'; 'd'])


