import Data.List (intersperse)
{-
Intermission: Exercise pg 282

    applyTimes :: (Eq a, Num a) => a -> (b -> b) -> b -> b
    applyTimes 0 f b = b
    applyTimes n f b = f . applyTimes (n-1) f $ b

    applyTimes 5 (+1) 5
    (+1) . applyTimes 4 (+1) 5
    (+1) . (+1) . applyTimes 3 (+1) 5
    (+1) . (+1) . (+1) . applyTimes 2 (+1) 5
    (+1) . (+1) . (+1) . (+1) . applyTimes 1 (+1) 5
    (+1) . (+1) . (+1) . (+1) . (+1) . applyTimes 0 (+1) 5
    (+1) . (+1) . (+1) . (+1) . (+1) $ 5

8.6 Chapter Exercises pg 294
Review of types
1. Whatisthetypeof[[True, False], [True, True], [False, True]]?
    a) Bool
    b) mostly True
    c) [a]
*   d) [[Bool]]
2. Which of the following has the same type as [[True, False], [True, True], [False, True]]?
    a) [(True, False), (True, True), (False, True)] 
*   b) [[3 == 3], [6 > 5], [3 < 4]]
    c) [3 == 3, 6 > 5, 3 < 4]
    d) ["Bool", "more Bool", "Booly Bool!"]
3. For the following function
    func :: [a] -> [a] -> [a] 
    func x y = x ++ y
which of the following is true?
    a) xandymustbeofthesametype 
    b) x and y must both be lists
    c) ifxisaStringthenymustbeaString 
*   d) all of the above
4. For the func code above, which is a valid application of func to both of its arguments?
    a) func "Hello World"
*   b) func "Hello" "World"
    c) func [1, 2, 3] "a, b, c"
    d) func ["Hello", "World"] 
    
Reviewing currying
Given the following definitions, tell us what value results from further applications.
    cattyConny :: String -> String -> String 
    cattyConny x y = x ++ " mrow " ++ y
-- fill in the types

    String -> String -> String 
    flippy = flip cattyConny 

    String -> String
    appedCatty = cattyConny "woops"

    String -> String
    frappe = flippy "haha"

1. What is the value of appedCatty "woohoo!" ? Try to determine the 
answer for yourself, then test in the REPL.
    "woops mrow woohoo!"
2. frappe "1"
    "1 mrow haha"
3. frappe (appedCatty "2")
    "woops mrow 2 mrow haha"
4. appedCatty (frappe "blue")
    "woops mrow blue mrow haha"
5. cattyConny (frappe "pink") (cattyConny "green" (appedCatty "blue"))
    "pink mrow haha mrow green mrow woops mrow blue"
6. cattyConny (flippy "Pugs" "are") "awesome"
    "are mrow Pugs mrow awesome"

Recursion
1. WriteoutthestepsforreducingdividedBy 15 2toitsfinalanswer according to the Haskell code.
2. Write a function that recursively sums all numbers from 1 to n, 
nbeingtheargument. Sothatifnwas5,you’dadd1+2+3+4+5toget15. Thetypeshouldbe
-}

sumN :: (Eq a, Num a) => a -> a
sumN 0 = 0
sumN n = n + (sumN (n-1))

--3. Write a function that multiplies two integral numbers using recursive summation
mult2 :: (Integral a) => a -> a -> a
mult2 1 y = y
mult2 x y = (+y) . mult2 (x-1) $ y       
-- ???  I DONT UNDERSTAND HOW THE RECURSIVE CALL WORKS WITH THE $ Y

--FIX DIVIDEDBY
data DividedResult = Result Integer | DividedByZero 
    deriving Show

dividedBy :: Integral a => a -> a -> DividedResult
dividedBy _ 0 = DividedByZero
dividedBy numerator denomenator = 
        let result = if (numerator < 0 && denomenator > 0) || (numerator > 0 && denomenator < 0) then (Result . negate) else (Result)
            go n d count
                | n < d = result count
                | otherwise = go (n - d) d (count + 1)
        in go (abs numerator) (abs denomenator) 0

--MC 91
-- TEST WITH : map mc91 [95..110]
mc91 n 
    | n > 100   = n - 10
    | otherwise = mc91 . mc91 $ n + 11

--DIGIT TO WORDS
intToDigit :: Int -> String 
intToDigit 0 = "zero"
intToDigit 1 = "one"
intToDigit 2 = "two"
intToDigit 3 = "three"
intToDigit 4 = "four"
intToDigit 5 = "five"
intToDigit 6 = "six"
intToDigit 7 = "seven"
intToDigit 8 = "eight"
intToDigit 9 = "nine"
intToDigit _ = "???"

digits :: Int -> [Int] 
digits n = go (abs n) []
    where go n acc
            | n < 10    = n : acc
            | otherwise = go (div n 10) ((mod n 10) : acc) 

wordNumber :: Int -> String
wordNumber n = concat $ intersperse "-" $ map intToDigit $ digits n
