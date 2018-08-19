//http://www.fssnip.net/an

// [snippet: (*) Problem 1 : Find the last element of a list.]
/// Example in F#: 
/// > last [1; 2; 3; 4];;
/// val it : int = 4
/// > last ['x';'y';'z'];;
/// val it : char = 'z'
let last xs = 
    match xs with
    | []    -> failwith "EmptyList"
    | _     -> xs |> List.rev |> List.head   

let lastTest1 = last [1; 2; 3; 4] = 4
let lastTest2 = last ['x';'y';'z'] = 'z'

// [snippet: (*) Problem 2 : Find the last but one element of a list.]
/// (Note that the Lisp transcription of this problem is incorrect.) 
///
/// Example in F#: 
/// secondToLast [1; 2; 3; 4];;
/// val it : int = 3
/// > secondToLast ['a'..'z'];;
/// val it : char = 'y'
let rec secondToLast = function    
    | []    -> failwith "Empty List"
    | [x]   -> failwith "Only one item in list"
    | [x;_] -> x
    | _::t  -> secondToLast t

// [snippet: (*) Problem 3 : Find the K'th element of a list. The first element in the list is number 1.]
/// Example: 
/// * (element-at '(a b c d e) 3)
/// c
/// 
/// Example in F#: 
/// > elementAt [1; 2; 3] 2;;
/// val it : int = 2
/// > elementAt (List.ofSeq "fsharp") 5;;
/// val it : char = 'r'
let rec elementAt xs n = 
    match xs,n with
    | []   ,_   -> failwith "Empty List"
    | x::_ ,1   -> x
    | _::xs,n   -> elementAt xs (n-1)

// [snippet: (*) Problem 4 : Find the number of elements of a list.]
/// Example in F#: 
/// 
/// > myLength [123; 456; 789];;
/// val it : int = 3
/// > myLength <| List.ofSeq "Hello, world!"
/// val it : int = 13 
let length xs = 
    let rec inner acc = function 
        | [] -> acc
        | x::xs -> inner (acc + 1) xs
    inner 0 xs

// [snippet: (*) Problem 5 : Reverse a list.]
/// Example in F#: 
///
/// > reverse <| List.ofSeq ("A man, a plan, a canal, panama!")
/// val it : char list =
///  ['!'; 'a'; 'm'; 'a'; 'n'; 'a'; 'p'; ' '; ','; 'l'; 'a'; 'n'; 'a'; 'c'; ' ';
///   'a'; ' '; ','; 'n'; 'a'; 'l'; 'p'; ' '; 'a'; ' '; ','; 'n'; 'a'; 'm'; ' ';
///   'A']
/// > reverse [1,2,3,4];;
/// val it : int list = [4; 3; 2; 1]
let reverse xs = 
    let rec inner acc = function 
        | [] -> acc
        | x::xs -> inner (x::acc) xs
    inner [] xs


// [snippet: (*) Problem 6 : Find out whether a list is a palindrome.]
/// A palindrome can be read forward or backward; e.g. (x a m a x).
/// 
/// Example in F#: 
/// > isPalindrome [1;2;3];;
/// val it : bool = false
/// > isPalindrome <| List.ofSeq "madamimadam";;
/// val it : bool = true
/// > isPalindrome [1;2;4;8;16;8;4;2;1];;
/// val it : bool = true
let isPalindrome xs = 
    xs = List.rev xs

let isPalindromeTest0 = isPalindrome [1;2;3] = false
let isPalindromeTest1 = isPalindrome <| List.ofSeq "madamimadam" = true
let isPalindromeTest2 = isPalindrome [1;2;4;8;16;8;4;2;1] = true


// [snippet: (**) Problem 7 : Flatten a nested list structure.]
/// Transform a list, possibly holding lists as elements into a `flat' list by replacing each 
/// list with its elements (recursively).
///  
/// Example: 
/// * (my-flatten '(a (b (c d) e)))
/// (A B C D E)
///  
/// Example in F#: 
///
/// > flatten (Elem 5);;
/// val it : int list = [5]
/// > flatten (List [Elem 1; List [Elem 2; List [Elem 3; Elem 4]; Elem 5]]);;
/// val it : int list = [1;2;3;4;5]
/// > flatten (List [] : int NestedList);;
/// val it : int list = []
type 'a NestedList = 
    ElemList of 'a NestedList list 
  | Elem of 'a

let flatten ls = 
    let rec loop acc = function 
        | Elem x -> x::acc
        | ElemList xs -> List.foldBack(fun x acc -> loop acc x) xs acc
    loop [] ls

let flattenTest0 = flatten (Elem 5) = [5]
let flattenTest1 = flatten (ElemList [Elem 1; ElemList [Elem 2; ElemList [Elem 3; Elem 4]; Elem 5]]) = [1;2;3;4;5]
let flattenTest2 = flatten (ElemList [] : int NestedList) = []


// [snippet: (**) Problem 8 : Eliminate consecutive duplicates of list elements.] 
/// If a list contains repeated elements they should be replaced with a single copy of the 
/// element. The order of the elements should not be changed.
///  
/// Example: 
/// * (compress '(a a a a b c c a a d e e e e))
/// (A B C A D E)
///  
/// Example in F#: 
/// 
/// > compress ["a";"a";"a";"a";"b";"c";"c";"a";"a";"d";"e";"e";"e";"e"];;
/// val it : string list = ["a";"b";"c";"a";"d";"e"]
let compress = function
    | [] -> []
    | x::xs -> 
        List.fold (fun acc y -> if y = List.head acc then acc else y::acc) [x] xs 
        |> List.rev

let compressTest0 = compress ["a";"a";"a";"a";"b";"c";"c";"a";"a";"d";"e";"e";"e";"e"] = ["a";"b";"c";"a";"d";"e"]

// [snippet: (**) Problem 9 : Pack consecutive duplicates of list elements into sublists.] 
/// If a list contains repeated elements they should be placed 
/// in separate sublists.
///  
/// Example: 
/// * (pack '(a a a a b c c a a d e e e e))
/// ((A A A A) (B) (C C) (A A) (D) (E E E E))
///  
/// Example in F#: 
/// 
/// > pack ['a'; 'a'; 'a'; 'a'; 'b'; 'c'; 'c'; 'a'; 
///         'a'; 'd'; 'e'; 'e'; 'e'; 'e']
/// val it : char list list =
///  [['a'; 'a'; 'a'; 'a']; ['b']; ['c'; 'c']; ['a'; 'a']; ['d'];
///   ['e'; 'e'; 'e'; 'e']]
let pack xs =
    let collect last = function
        | (current::xs)::xss when last = current -> (last::current::xs)::xss
        | xss -> [last]::xss
    List.foldBack collect xs []

let packTest0 = pack ['a'; 'a'; 'a'; 'a'; 'b'; 'c'; 'c'; 'a'; 'a'; 'd'; 'e'; 'e'; 'e'; 'e'] = [['a'; 'a'; 'a'; 'a']; ['b']; ['c'; 'c']; ['a'; 'a']; ['d'];['e'; 'e'; 'e'; 'e']]
let packTest1 = pack [] = []

// [snippet: (*) Problem 10 : Run-length encoding of a list.]
/// Use the result of problem P09 to implement the so-called run-length 
/// encoding data compression method. Consecutive duplicates of elements 
/// are encoded as lists (N E) where N is the number of duplicates of the element E.
///  
/// Example: 
/// * (encode '(a a a a b c c a a d e e e e))
/// ((4 A) (1 B) (2 C) (2 A) (1 D)(4 E))
///  
/// Example in F#: 
/// 
/// encode <| List.ofSeq "aaaabccaadeeee"
/// val it : (int * char) list =
///   [(4,'a');(1,'b');(2,'c');(2,'a');(1,'d');(4,'e')]
let encode xs =
    xs 
    |> pack 
    |> List.map (fun xs -> (List.length xs, List.head xs))
 
let encodeTest0 = (encode <| List.ofSeq "aaaabccaadeeee") =  [(4,'a');(1,'b');(2,'c');(2,'a');(1,'d');(4,'e')]
