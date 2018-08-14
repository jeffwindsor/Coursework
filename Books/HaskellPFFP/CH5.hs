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

-------------------------------------------------
--Write a type signature
functionH :: [a] -> a
functionH (x:_) = x

functionC :: Ord a => a -> a -> Bool
functionC x y = if (x > y) then True else False

functionS :: (a,b) -> b
functionS (x, y) = y

-------------------------------------------------
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

-------------------------------------------------
--Fixit

--Sing
fstString :: [Char] -> [Char]
fstString x = x ++ " in the rain"

sndString :: [Char] -> [Char]
sndString x = x ++ " over the rainbow"

rainbow = if (x > y) then fstString x else sndString y
    where x = "Singin"
          y = "Somewhere"

rain = if (x < y) then fstString x else sndString y
    where x = "Singin"
          y = "Somewhere"

--Artih3Broken
main :: IO () 
main = do
    print $ 1 + 2
    print 10
    print $ negate (-1)
    print $ (+) 0 blah
        where blah = negate 1


-------------------------------------------------
--TypeKwonDo
--1
f :: Int -> String
f = undefined

g :: String -> Char
g = undefined

h :: Int -> Char
h a = g $ f a

--2
data A 
data B 
data C

q :: A -> B
q = undefined

w :: B -> C
w = undefined

e :: A -> C
e a = w $ q a

--3. 
data X 
data Y 
data Z

xz :: X -> Z 
xz = undefined

yz :: Y -> Z 
yz = undefined

xform :: (X, Y) -> (Z, Z) 
xform (x,y) = (xz x, yz y)

--4. 
munge :: (x -> y)
        -> (y -> (w, z))
        -> x
        -> w 
munge x2y y2wz x = fst $ y2wz $ x2y x