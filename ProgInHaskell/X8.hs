module Chapter8Exercises where
    
--1 
    data Nat = Zero | Succ Nat
    
    nat2int :: Nat -> Int
    nat2int Zero = 0
    nat2int (Succ n) =  1 + nat2int n

    int2nat :: Int -> Nat
    int2nat 0 = Zero
    int2nat n = Succ( int2nat (n - 1) )

    add :: Nat -> Nat -> Nat 
    add Zero n      = n
    add (Succ m) n  = Succ (add m n)

    mult ::  Nat -> Nat -> Nat
    mult _ Zero     = Zero
    -- adds first Nat recursively, iterations = second Nat times and a Zero
    mult n (Succ m) = add n (mult n m)

--2
    -- data NodeTree a = Leaf a | Node (Tree a) a (Tree a)

    -- occurs :: Eq a => a -> NodeTree a -> Bool
    -- occurs x (Leaf y)     = x == y
    -- occurs x (Node l y r) = x == y || occurs x l || occurs x r 

    -- occurs' :: Eq a => a -> Tree a -> Bool
    -- occurs' x (Leaf y)     = x == y
    -- occurs' x (Node l y r) 
    --     | ord == LT = occurs' x l
    --     | ord == EQ = True
    --     | ord == GT = occurs' x r
    --     where ord = compare x y

--3
    data Tree a = Leaf a | Node (Tree a) (Tree a)
    leaves :: Tree a -> Int
    leaves (Leaf _)     = 1
    leaves (Node l r) = leaves l + leaves r

    balanced :: Tree a -> Bool
    balanced (Leaf _)     = True
    balanced (Node l r) = abs (leaves l - leaves r) <= 1

--4
    balance :: [a] -> Tree a
    balance [x] = Leaf x
    balance xs  = let (l,r) = splitAt (length xs `div` 2) xs
                  in Node (balance l) (balance r)
--5 
    data Expr = Val Int | Add Expr Expr

    folde :: (Int -> a) -> (a -> a -> a) -> Expr -> a
    folde f _ (Val i)   = f i
    folde f g (Add l r) = g (folde f g l) (folde f g r)

    eval :: Expr -> Int
    eval = folde id (+)

    size :: Expr -> Int
    size = folde (const 1) (+) 
