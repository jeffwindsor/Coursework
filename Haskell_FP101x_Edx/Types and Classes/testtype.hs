
add :: Int -> (Int -> Int)
add x y = x + y

-- Guard Expressions
abs :: Int -> Int
abs n | n >= 0 	= n
		| otherwise = -n

safetail (_:xs)
	| null xs = []
	| otherwise = tail xs
