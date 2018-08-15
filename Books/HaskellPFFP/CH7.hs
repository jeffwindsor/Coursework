--LET IN SYNTAX
bindExp :: Integer -> String 
bindExp x =
    let z = y + x; y = 5 in
            "the integer was: " ++ 
            show x ++ 
            " and y was: " ++ 
            show y ++ 
            " and z was: " ++ 
            show z
    
--LAMBDA SYNTAX 
ls = (\x -> x * 3) :: Integer -> Integer

--Exercises: Grab Bag
{-
1. Which (two or more) of the following are equivalent?  --ALL OF THEM
*    a) mTh x y z = x * y * z
*    b) mTh x y = \z -> x * y * z
*    c) mTh x = \y -> \z -> x * y * z
*    d) mTh = \x -> \y -> \z -> x * y * z

2. ThetypeofmTh(above)isNum a => a -> a -> a -> a. Which is the type of mTh 3?
*   a) Integer -> Integer -> Integer 
    b) Num a => a -> a -> a -> a
    c) Num a => a -> a
    d) Num a => a -> a -> a
 
3. Next, we’ll practice writing anonymous lambda syntax. For example, one could rewrite:
        addOne x = x + 1
    Into:
        addOne = \x -> x + 1
    Try to make it so it can still be loaded as a top-level definition by GHCi. This will make it easier to validate your answers.
    a) Rewrite the f function in the where clause.
    
        addOneIfOdd n = case odd n of 
            True  -> n + 1
            False -> n

    b) Rewrite the following to use anonymous lambda syntax:
        addFive = \x -> \y -> (if x > y then y else x) + 5

    c) Rewrite the following so that it doesn’t use anonymous lambda syntax:
        mflip f x y = f y x
-}