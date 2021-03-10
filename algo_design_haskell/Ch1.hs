module Ch1 where

--E 1.1
-- maximum :: (Foldable t, Ord a) => t a -> a 
-- minimum :: (Foldable t, Ord a) => t a -> a
-- take :: Int -> [a] -> [a]
-- drfoldOp :: Int -> [a] -> [a]
-- takeWhile :: (a -> Bool) -> [a] -> [a]
-- drfWhile :: (a -> Bool) -> [a] -> [a]
-- inits :: [a] -> [[a]]
--      >>> inits "abc"
--      ["","a","ab","abc"]
-- tails :: [a] -> [[a]]
--      >>> tails "abc"
--      ["abc","bc","c",""]
-- splitAt :: Int -> [a] -> ([a],[a])
-- span :: (a -> Bool) -> [a] -> ([a],[a]) 
-- null :: Foldable t => t a -> Bool
-- all ::  Foldable t => (a -> Bool) -> t a -> Bool
--      Do all elements in the list match predicate ?
-- elem :: a -> [a] -> Bools!!
--      Does the element occur in the structure?
-- (!!) :: [a] -> Int -> a
--      list index startnig at 0
-- zipWith :: (a -> b -> c) -> [a] -> [b] -> [c]

--E 1.2
uncons' :: [a] -> Maybe(a,[a])
uncons' []     = Nothing
uncons' (x:xs) = Just(x,xs)

--E 1.3
wrap' :: a -> [a]
wrap' x = [x]

unwrap' :: [a] -> a
unwrap' [x] = x
unwrap' _   = error "not allowed"

single' :: [a] -> Bool
single' [_] = True
single' _   = False

--E 1.4
reverse' :: Foldable t => t a -> [a]
reverse' = foldl (flip(:)) []

--E 1.5
map' :: (a -> b) -> [a] -> [b]
map' f = foldr ((:) . f) []

filter' :: (a -> Bool) -> [a] -> [a]
filter' p = foldr foldOp []
  where foldOp x acc = if p x then x : acc else acc 

--E 1.6
foldfilter' :: Foldable t => (a -> Bool) -> (a -> b -> b) -> b -> t a -> b
foldfilter' p f = foldr foldOp
  where foldOp x acc = if p x then f x acc else acc

--E 1.7
takeWhile' :: (a -> Bool) -> [a] -> [a]
takeWhile' p = foldr foldOp []
  where foldOp x acc = if p x then x : acc else []

--debugFoldr :: [String] -> String -> String
--debugFoldr xs s = foldr foldOp s xs
--  where foldOp x y = "(" ++ x ++ "+" ++ y ++ ")"

--debugFoldl :: [String] -> String -> String
--debugFoldl xs s = foldl foldOp s xs
--  where foldOp x y = "(" ++ x ++ "+" ++ y ++ ")"

--E 1.8
drfWhileEnd' :: (a -> Bool) -> [a] -> [a]
drfWhileEnd' p = foldr foldOp []
  where foldOp x acc = if p x && null acc then [] else x : acc 

--E 1.10
-- associative and identity (for e), so monoidal

--E 1.11
integer' :: [Integer] -> Integer
integer' xs = fst $ foldr foldOp (0,0) xs
  where foldOp x (value, power) = (value + (x * (10 ^ power)), power + 1)
-- answer from key
integer'' ::  Integral a => [a] -> a 
integer'' = foldl foldOp 0
  where foldOp acc x = 10 * acc + x

fraction' :: (Fractional b, Integral a) => [a] -> b 
fraction' = foldr foldOp 0
  where foldOp x acc= (fromIntegral x + acc) / 10
  
