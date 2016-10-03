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