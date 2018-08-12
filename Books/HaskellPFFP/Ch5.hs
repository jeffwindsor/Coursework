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
i :: a -> a
i a = a

c :: a -> b -> a 
c a _ = a

c'' :: b -> a -> b 
c'' b _ = b

c' :: a -> b -> b 
c' _ b = b 

r :: [a] -> [a] 
r [xs] = [xs]

co :: (b -> c) -> (a -> b) -> a -> c 
co bTOc aTOb a = bTOc $ aTOb a 

a :: (a -> c) -> a -> a
a aTOc a = a                ??? cannot seem to get the c getting: a :: p1 -> p2 -> p2

a' :: (a -> b) -> a -> b 
a' aTOb a = aTOb a

--------------------------------------------------
Fix it
--------------------------------------------------
1 and 2 - see sing.hs
3 see arith3broken.hs


--------------------------------------------------
Type-Kwon-Do
--------------------------------------------------
see typekwondo.hs


-}