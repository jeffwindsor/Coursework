

--PG 307 >> Exercise: EnumFromTo - provide an enumFromTo implementation
eftBool :: Bool -> Bool -> [Bool]
eftBool True False = [True, False]
eftBool False True = [False, True]
eftBool n _ = [n]

eftOrd :: Ordering
       -> Ordering
       -> [Ordering] 
eftOrd = undefined
-- ???

eftInt :: Int -> Int -> [Int] 
eftInt n m
    | n > m  = []
    | n <= m = n : eftInt (n+1) m

eftChar :: Char -> Char -> [Char] 
eftChar n m | n > m  = []
            | n <= m = n : eftChar (succ n) m