import Data.List(sort)

{-
6.14 Chapter Exercises
Multiple choice
    1:c 2:b 3:a 4:c 5:a
-}
--1. Does the following code typecheck? If not, why not?
--NO does not derive show 
data Person = Person Bool 
    deriving Show

printPerson :: Person -> IO ()
printPerson person = putStrLn (show person)

--2. Does the following typecheck? If not, why not?
--No instance of EQ
data Mood = Blah | Woot 
    deriving Show
instance Eq Mood where
    (==) Blah Blah = True
    (==) Woot Woot = True
    (==) _ _ = False

settleDown x = 
    if x == Woot 
    then Blah
    else x

--3. If you were able to get settleDown to typecheck:
    -- a) What values are acceptable inputs to that function?
    -- any Mood
    -- b) What will happen if you try to run settleDown 9? Why?
    -- no instance of (Num Mood)
    -- c) What will happen if you try to run Blah > Woot? Why?
    --no instance of ORD Mood
--4. Does the following typecheck? If not, why not?
--yes, no use of EQ or Show just data constructor

type Subject = String
type Verb = String
type Object = String
data Sentence = Sentence Subject Verb Object 
    deriving (Eq, Show)

s1 = Sentence "dogs" "drool"
s2 = Sentence "Julie" "loves" "dogs"

-- Given a datatype declaration, what can we do?
-- Given the following datatype definitions:
data Rocks = Rocks String 
    deriving (Eq, Show)
data Yeah = Yeah Bool 
    deriving (Eq, Show)
data Papu = Papu Rocks Yeah 
    deriving (Eq, Show)
-- Which of the following will typecheck? For the ones that don’t typecheck, why don’t they?
-- phew = Papu "chases" True  --no, Papu takes Rocks Yeah not String Bool

truth = Papu (Rocks "chomskydoz") (Yeah True) --yes

--yes
equalityForall :: Papu -> Papu -> Bool 
equalityForall p p' = p == p'

--No - Papu, Rokcs, Yeah do not implment Ord
-- comparePapus :: Papu -> Papu -> Bool 
-- comparePapus p p' = p > p'

--Match the types
--NOT SPECIFIC ENOUGH i :: a 
i :: Num a => a
i = 1

--NOT SPECIFIC ENOUGH f :: Num a => a 
f :: Float 
f = 1.0

f' :: Fractional a => a
--f' :: Float 
f' = 1.0

f'' :: RealFrac a => a
--f'' :: Float 
f'' = 1.0

freud :: Ord a => a -> a
--freud::a->a
freud x = x

--freud'::a->a 
freud' :: Int -> Int
freud' x = x

myX = 1::Int
--NO to broad sigmund :: a -> a
sigmund :: Int -> Int 
sigmund x = myX

myX' = 1::Int 
--NO TO BROAD sigmund' :: Num a => a -> a
sigmund' :: Int -> Int 
sigmund' x = myX'

jung :: [Int] -> Int
--jung :: Ord a => [a] -> a
jung xs = head (sort xs)

young :: Ord a => [a] -> a
--young :: [Char] -> [Char]
young xs = head (sort xs)

mySort :: [Char] -> [Char]
mySort = sort

--signifier :: Ord a => [a] -> a
signifier :: [Char] -> Char
signifier xs = head (mySort xs)
