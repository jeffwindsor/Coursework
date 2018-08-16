
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

1. What is the value of appedCatty "woohoo!" ? Try to determine the answer for yourself, then test in the REPL.
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
2. Write a function that recursively sums all numbers from 1 to n, nbeingtheargument. Sothatifnwas5,you’dadd1+2+3+4+5toget15. Thetypeshouldbe
-}

sumN :: (Eq a, Num a) => a -> a
sumN 0 = 0
sumN n = n + (sumN (n-1))

--3. Write a function that multiplies two integral numbers using recursive summation. The type should be 
mult2 :: (Integral a) => a -> a -> a
mult2 1 y = y
mult2 x y = (+y) . mult2 (x-1) $ y       -- ???  I DONT UNDERSTAND HOW THE RECURSIVE CALL WORKS WITH THE $ Y

--FIX DIVIDEDBY
data DividedResult = Result Integer | DividedByZero 
dividedBy :: Integral a => a -> a -> a
dividedBy _ 0 = DividedByZero
dividedBy numerator denomenator = go (abs numerator) (abs denomenator) 0
    where go n d count
           | n < d = result numerator denominator count
           | otherwise = go (n - d) d (count + 1)
    where result n d c
               | n < 0 && d > 0 = Result -c
               | n >= 0 && d < 0 = Result -c
               | otherwise Result c

               -- what
--MC 91
-- mc91 = undefined



-- import Data.List (intersperse)
-- digitToWord :: Int -> String 
-- digitToWord n = undefined

-- digits :: Int -> [Int] 
-- digits n = undefined

-- wordNumber :: Int -> String 
-- wordNumber n = undefined
