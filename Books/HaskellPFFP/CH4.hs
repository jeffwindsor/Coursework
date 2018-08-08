awesome = ["Papuchon", "curry", ":)"] 
also = ["Quake", "The Simons"] 
allAwesome = [awesome, also]

-- 1)  length :: [a] -> Integer
-- 2)  a) 5  b) 3 c) 2 d) 5
-- 3) length returns an Int which does not have an implementation of / that is for fractionals
-- 4) change (/) to `div`
-- 5) Bool, True
-- 6) Bool, False
-- 7)  True: Prelude> length allAwesome == 2
    -- cannot mix types in list: Prelude> length [1, 'a', 3, 'b']
    -- 5: Prelude> length allAwesome + length awesome
    -- False: Prelude> (8 == 8) && ('b' < 'a')
    -- 9 is not Bool: Prelude> (8 == 8) && 9
-- 8)
reverse' :: [a] -> [a]
reverse' []     = []
reverse' (x:xs) = (reverse' xs) ++ [x]  

isPalindrome :: (Eq a) => [a] -> Bool 
isPalindrome x = x == (reverse x)

-- 9)
myAbs :: Integer -> Integer 
myAbs x = if (x < 0) then (-x) else x

-- 10)
f :: (a, b) -> (c, d) -> ((b, d), (a, c)) 
f x y = ((snd x, snd y), (fst x, fst y))

--Correcting syntax
--1
x = (+)
f' xs = w `x` 1
        where w = length xs
--2 \x -> x
--3 f(a,b) = a
--Match the function names to their types 
--1:c 2:b 3:c 4:d