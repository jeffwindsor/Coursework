
safe_root :: (Floating a, Ord a) => a -> Maybe a
safe_root x
  | x >= 0     = Just $ sqrt x
  | otherwise  = Nothing

safe_recip ::  (Floating a, Ord a) => a -> Maybe a
safe_recip x
  | x == 0    = Nothing
  | otherwise = Just ( 1 / x )

safe_root_recip :: (Floating a, Ord a) => a -> Maybe a
safe_root_recip = (handle_option safe_root) . safe_recip
  where handle_option _ Nothing  = Nothing
        handle_option f (Just x) = f x
