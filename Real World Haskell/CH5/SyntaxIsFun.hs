module SyntaxIsFun where
  factorial :: (Integral a) => a -> a
  factorial 0 = 1
  factorial n = n * factorial (n - 1)

  fizzbuzz :: (Integral a) => a -> String
  fizzbuzz n
    | ModOf 15 = "FizzBuzz"
    | ModOf 5  = "Fizz"
    | ModOf 3  = "Buzz"
    | _        = show i
    where ModOf d n = (n % d = 0)
