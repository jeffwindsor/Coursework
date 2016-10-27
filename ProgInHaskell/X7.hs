module Chapter7Exercises where
    import Data.Char

    even' :: Int -> Bool
    even' n = (n `mod` 2) == 0

--1
    one :: (a -> b) -> (a -> Bool) -> [a] -> [b]
    one f p = map f . filter p

--2
    all' :: (a -> Bool) -> [a] -> Bool
    all' p [] = True
    all' p (x:xs)
        | p x = all' p xs
        | otherwise = False

    any' :: (a -> Bool) -> [a] -> Bool
    any' p []       = False
    any' p (x:xs)
        | p x       = True
        | otherwise = any' p xs

    takeWhile' :: (a -> Bool) -> [a] -> [a]
    takeWhile' p [] = []
    takeWhile' p (x:xs)
        | p x       = x : takeWhile' p xs
        | otherwise = []

    dropWhile' :: (a -> Bool) -> [a] -> [a]
    dropWhile' p [] = []
    dropWhile' p (x:xs)
        | p x       = dropWhile' p xs
        | otherwise = x : dropWhile' p xs

--3
    mapFilter' :: (a -> Bool) -> (a -> b) -> [a] -> [b]
    mapFilter' p f = foldr (\x acc -> if p x then f x : acc else acc) []

--4 
    dec2int :: [Int] -> Int
    dec2int = foldl (\x acc -> x + acc*10) 0

--5
    paired (x,y) = x*y
    unpaired x y = x+y

    curry' :: ((a,b) -> c) -> (a -> b -> c)
    curry' f x y = f (x,y) 

    uncurry' :: (a -> b -> c) -> ((a,b) -> c)
    uncurry' f (x,y) = f x y

--6
    unfold' :: (a -> Bool) -> (a -> b) -> (a -> a) -> a -> [b]
    unfold' p h t x | p x       = []
                    | otherwise = h x : unfold' p h t (t x)

    type Bit = Int

    int2bin' :: Int -> [Bit]
    int2bin' = unfold' (== 0) (`mod` 2) (`div` 2)

    chop8' :: [Bit] -> [[Bit]]
    chop8' = unfold' (==[]) (take 8) (drop 8)

    map' :: Eq a => (a -> b) -> [a] -> [b]
    map' f = unfold' (==[]) (f . head) tail

    iterate' :: (a -> a) -> a -> [a]
    iterate' = unfold' (const False) id

--7 

    bin2int :: [Bit] -> Int
    bin2int = foldr (\x y -> x + 2*y) 0

    int2bin :: Int -> [Bit]
    int2bin 0 = []
    int2bin n = n `mod` 2 : int2bin (n `div` 2)

    make :: Int -> [Bit] -> [Bit]
    make n bits = take n (bits ++ repeat 0)
    
    chop :: Int -> [Bit] -> [[Bit]]
    chop _ []   = []
    chop n bits = take n bits : chop n (drop n bits)

    parityBit :: [Bit] -> Bit
    parityBit = (`mod` 2) .length . filter (==1)

    addParity :: [Bit] -> [Bit]
    addParity bits = parityBit bits : bits
    
    removeParity :: [Bit] -> [Bit]
    removeParity (pbit:bits) | pbit == parityBit bits = bits
                             | otherwise = error "Transmission Error"

    encode :: String -> [Bit]
    encode = concatMap (addParity . make 8 . int2bin . ord)

    decode :: [Bit] -> String
    decode = map (chr . bin2int . removeParity) . chop 9 

    transmit :: ([Bit] -> [Bit]) -> String -> String
    transmit channel = decode . channel . encode

--8
    transmitGoodChannel = transmit id
    transmitBadChannel = transmit tail

--9
    altMap :: (a -> b) -> (a -> b) -> [a] -> [b]
    altMap _ _ []     = []
    altMap f g (x:xs) = f x : altMap g f xs 

--10
    luhn :: [Int] -> Bool
    luhn = (==0) . (`mod` 10) . sum . luhnMap
        where luhnMap xs' | even . length $ xs' = altMap double id xs'
                          | otherwise           = altMap id double xs'
              double x | x * 2 > 9 = x * 2 - 9
                       | otherwise = x * 2

