--LET.IN SYNTAX
bindExp :: Integer -> String 
bindExp x =
    let z = y + x
        y = 5 
    in "answer: " ++ show x ++ " : " ++ show z

--PATTERN MATCH SYNTAX
triple :: Integer -> Integer 
triple 0 = 0  --shown for contrast to catch all case
triple x = x * 3

--LAMBDA SYNTAX
anonymousTriple = (\x -> x * 3) :: Integer -> Integer

--CASE SYNTAX


{-  
Exercises: Grab Bag PG224
    1. a,b,c,d
    2. a
    3.a.
        addOneIfOdd n = case odd n of 
            True  -> n + 1
            False -> n
    3.b addFive = \x -> \y -> (if x > y then y else x) + 5
    3.c mflip f x y = f y x
-}