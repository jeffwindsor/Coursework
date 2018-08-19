import Data.Char
factors :: Int -> [Int]
factors n = [x | x <- [1..n], n `mod` x == 0]

prime :: Int -> Bool
prime n = factors n == [1,n]

primes :: Int -> [Int]
primes n = [x | x <- [2..n], prime x]

pairs :: [a] -> [(a,a)]
pairs xs = zip xs (tail xs)

position :: Eq a => a -> [a] -> [Int] 
position x xs = [i | (x',i) <- zip xs [0..], x' == x]

--Exercises
pyths n = [(x,y,z) | x <- [1..n], y <- [1..n], z <- [1..n], x^2 + y^2 == z^2]

--Exercise #5
find :: (Eq a) => a -> [(a,b)] -> [b]
find x ts = [v | (x', v) <- ts, x == x']

positions :: (Eq a) => a -> [a] -> [Int]
positions x xs = find x (zip xs [0..n])
   where n = length xs - 1

--Exercise #7
shiftFloor floor c n = chr (ord floor + ((ord c - ord floor + n) `mod` 26))
shift n c 
   | isLower c = shiftFloor 'a' c n
   | isUpper c = shiftFloor 'A' c n 
   | otherwise = c
encode n xs = [shift n x | x <- xs]