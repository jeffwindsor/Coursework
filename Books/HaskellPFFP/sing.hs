module Sing where

fstString :: [Char] -> [Char]
fstString x = x ++ " in the rain"

sndString :: [Char] -> [Char]
sndString x = x ++ " over the rainbow"

rainbow = if (x > y) then fstString x else sndString y
    where x = "Singin"
          y = "Somewhere"

rain = if (x < y) then fstString x else sndString y
    where x = "Singin"
          y = "Somewhere"