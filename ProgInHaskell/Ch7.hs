module Chapter7 where
    import Data.Char

    type Bit = Int

    bin2int' :: [Bit] -> Int
    bin2int' bits = sum [ w * b | (w,b) <- zip digits bits]
                   where digits = iterate (*2) 1
    
    bin2int :: [Bit] -> Int
    bin2int = foldr (\acc x -> acc + x*2) 0

    int2bin :: Int -> [Bit]
    int2bin 0 = []
    int2bin n = n `mod` 2 : int2bin (n `div` 2)

    make8 :: [Bit] -> [Bit]
    make8 bs = take 8 (bs ++ repeat 0)

    encode :: String -> [Bit]
    encode = concatMap (make8 . int2bin . ord)