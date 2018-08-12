{- 
Chapter Exercises

--------------------------------------------------
Multiple Choice
--------------------------------------------------
1:c 2:a 3:b 4:c

--------------------------------------------------
Determine the type
--------------------------------------------------
1:
    a: Num a=>a 54
    b: (Num a, [Char])
    c: (Int, [Char])
    d: Bool
    e: Int
    f: Bool

2: Num a => a
3: Num a => a -> a
4: Fractional a => a
5: [Char]

--------------------------------------------------
Does it compile
--------------------------------------------------
1:No
2:yes
3:No
4:No

--------------------------------------------------
Type variable or specific type constructor?
--------------------------------------------------
1: constrained, fully, fully, con, con
2: con, con
3: constained, full, full, con
4: full, full, con

--------------------------------------------------
Write a type signature
--------------------------------------------------
functionH :: [a] => a
functionH (x:_) = x

functionC :: Ord a => a -> a -> Bool
functionC x y = if (x > y) then True else False

functionS :: (a,b) => b
functionS (x, y) = y

--------------------------------------------------
Given a type, write the function
--------------------------------------------------


-}