module Main (main) where
  import System.Environment
  import SchemeParser

  main :: IO()
  main = do
    (expr:_) <- getArgs
    putStrLn (readExpr expr)
