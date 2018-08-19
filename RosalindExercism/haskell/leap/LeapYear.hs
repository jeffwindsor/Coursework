module LeapYear where
  isLeapYear :: Integer -> Bool
  isLeapYear y
    | (mod y 100 == 0) = (mod y 400 == 0)
    | otherwise        = mod y 4 == 0
