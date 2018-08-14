{- 
Chapter Exercises

Multiple Choice
1:c 2:a 3:b 4:c

Determine the type
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

Does it compile
1:No
2:yes
3:No
4:No

Type variable or specific type constructor?
1: constrained, fully, fully, con, con
2: con, con
3: constained, full, full, con
4: full, full, con
-} 

--Write a type signature
functionH :: [a] -> a
functionH (x:_) = x

functionC :: Ord a => a -> a -> Bool
functionC x y = if (x > y) then True else False

functionS :: (a,b) -> b
functionS (x, y) = y

--Given a type, write the function
--1. Thereisonlyonefunctiondefinitionthattypechecksanddoesn’t go into an infinite loop when you run it.
i :: a -> a
i a = a

--2. There is only one version that works.
c :: a -> b -> a 
c a _ = a

--3. Given alpha equivalence are c'' and c (see above) the same thing?
c'' :: b -> a -> b 
c'' b _ = b

--4. Only one version that works.
c' :: a -> b -> b 
c' _ b = b

--5. There are multiple possibilities, at least two of which you’ve seen in previous chapters.
r :: [a] -> [a] 
r [a] = [a] ++ [a] 

--6. Only one version that will typecheck.
co :: (b -> c) -> (a -> b) -> a -> c 
co b2c a2b a = b2c $ a2b a 

--7. One version will typecheck.
a :: (a -> c) -> a -> a
a _ a = a  --why is this not a2b

--8. One version will typecheck.
a' :: (a -> b) -> a -> b 
a' a2b a = a2b a 
