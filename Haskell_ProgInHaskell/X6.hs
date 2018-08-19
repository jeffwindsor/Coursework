module Chapter6Exercises where
    
    --Exercise 6.1
    fac :: Int -> Int
    fac n | n == 0    = 1
          | n > 0     = n * fac (n - 1)
          | otherwise = error "Negative Numbers Not Allowed"

    --Exercise 6.2
    sumdown :: Int -> Int
    sumdown n | n > 0 = n + sumdown(n - 1)
              | otherwise = 0

    --Exercise 6.3
    (^) :: Int -> Int -> Int
    m ^ 0 = 1
    m ^ n = m * (m Chapter6Exercises.^ (n-1)) 

    --Exercise 6.4
    euclid :: Int -> Int -> Int
    euclid a b | a == b = a
               | a < b  = euclid a (b - a)
               | a > b  = euclid b (a - b)

    --Exercise 6.6
    --a
    and :: [Bool] -> Bool
    and []      = True
    and (x:xs)  = x && Chapter6Exercises.and xs
    
    --b    
    concat :: [[a]] -> [a]
    concat []     = []
    concat (xs:xss) = xs ++ Chapter6Exercises.concat xss 

    --c
    replicate :: Int -> a -> [a]
    replicate 0 item = []
    replicate n item = item : Chapter6Exercises.replicate (n-1) item

    --d
    (!!) :: [a] -> Int -> a
    [] !! _     = error "index to large"
    (x:xs) !! 0 = x
    (x:xs) !! n = xs Chapter6Exercises.!! (n-1)

    --e
    elem :: Eq a => a -> [a] -> Bool
    elem _ [] = False

{-
    --Exercise 7
    merge :: Ord a => [a] -> [a] -> [a]
    merge xs [] = xs
    merge [] ys = ys
    merge xs@(x:xs') ys@(y:ys') 
        | x < y     = x : merge xs ys'
        | otherwise = y : merge xs' ys 

    mergeSort :: Ord a => [a] -> [a] -> [a]
    mergeSort []  = []
    mergeSort [x] = [x]
    mergeSort xs  = merge (mergeSort ys) (mergeSort zs)
        where 
-}

    sum' :: Num a => [a] -> a
    sum' [x]    = x
    sum' (x:xs) = x + sum' xs 

    take' :: Int -> [a] -> [a]
    take' 0 _      = []
    take' n (x:xs) = x : take' (n-1) xs

    last' :: [a] -> a
    last' [x]    = x
    last' (x:xs) = last' xs