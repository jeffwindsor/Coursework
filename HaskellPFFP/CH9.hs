--PG 307 >> Exercise: EnumFromTo - provide an enumFromTo implementation
eftBool :: Bool -> Bool -> [Bool]
eftBool True False = [True, False]
eftBool False True = [False, True]
eftBool n _ = [n]

eftOrd :: Ordering
       -> Ordering
       -> [Ordering]
eftOrd = undefined
-- ???

eftInt :: Int -> Int -> [Int]
eftInt n m
    | n > m  = []
    | n <= m = n : eftInt (n+1) m

eftChar :: Char -> Char -> [Char]
eftChar n m | n > m  = []
            | n <= m = n : eftChar (succ n) m

-- PG 311 Thy fearful Symmetry
--1) takewhile dropWhile create 􏰜􏰅􏰀􏰍􏰂􏰃􏰆􏰔􏰏􏰈􏰍􏰀􏰎􏰏􏰍􏰐􏰃􏰍􏰍􏰃􏰉􏰂􏰌􏰃􏰌􏰍􏰅􏰀􏰏􏰑 􏰃􏰏􏰄
myWords :: [Char] -> [[Char]]
myWords = split ' '

--2)
firstSen = "Tyger Tyger, burning bright\n"
secondSen = "In the forests of the night\n"
thirdSen = "What immortal hand or eye\n"
fourthSen = "Could frame thy fearful\
\ symmetry?"
sentences = firstSen ++ secondSen ++ thirdSen ++ fourthSen

-- What we want 'myLines sentences' -- to equal
shouldEqual =
        [ "Tyger Tyger, burning bright"
        , "In the forests of the night"
        , "What immortal hand or eye"
        , "Could frame thy fearful symmetry?"
        ]
-- The main function here is a small test -- to ensure you've written your function -- correctly.
main :: IO ()
main = print $
        "Are they equal? "
        ++ show (myLines sentences == shouldEqual)
-- Implement this
myLines :: String -> [String]
myLines = split '\n'

--3) generic function for myLines and myWords
split :: Char -> [Char] -> [[Char]]
split _ [] = []
split c s  =
  let head = takeWhile (/=c) s
      tail = dropWhile (==c) $ dropWhile (/=c) s
  in head : split c tail

--9.7 List Comprehensions-----------------------------------------------