module Chapter7Exercises where
    one :: (a -> b) -> (a -> Bool) -> [a] -> [b]
    one f p = map f . filter p

    all' :: (a -> Bool) -> [a] -> Bool
    all' p [] = True
    all' p (x:xs)
        | p x = all' p xs
        | otherwise = False

    even' :: Int -> Bool
    even' n = (n `mod` 2) == 0


