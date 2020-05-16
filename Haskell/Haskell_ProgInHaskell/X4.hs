
safetail :: [a] -> [a]
safetail xs = if null xs then [] else tail xs

safetail' :: [a] -> [a]
safetail' xs | null xs   = [] 
             | otherwise = tail xs

safetail'' :: [a] -> [a]
safetail'' [] = [] 
safetail'' xs = tail xs

(|||) :: Bool -> Bool -> Bool
False ||| False = False
_ ||| _ = True

mult :: Int -> Int -> Int -> Int
mult = \x -> (\y -> (\z -> x*y*z))

ldouble :: Int -> Int
ldouble n | 0 <= n && n < 5  = n * 2
          | 5 <= n && n < 10 = (n * 2) - 9
          | otherwise = error "not a digit"

luhn :: Int -> Int -> Int -> Int -> Bool
luhn a b c d = (ldouble a + b + ldouble c + d) `mod` 10 == 0

