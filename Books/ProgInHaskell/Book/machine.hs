-- Abstract machine example from chapter 8 of Programming in Haskell,
-- Graham Hutton, Cambridge University Press, 2016.

data Expr = 
    Val Int 
    | Add Expr Expr

type Stack = [Operation]

data Operation = 
    EVAL Expr 
    | ADD Int

--Evaluate Expression
eval :: Expr -> Stack -> Int
eval (Val n)   c = exec n c
eval (Add x y) c = eval x (EVAL y : c)

--Execute Operation Stack
exec :: Int -> Stack -> Int
exec n []           = n
exec n (EVAL y : c) = eval y (ADD n : c)
exec n (ADD  m : c) = exec (n+m) c
 
calculate :: Expr -> Int
calculate e = eval e []
