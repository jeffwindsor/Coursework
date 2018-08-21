{-
===========================================================
MONOIDS: Algebras and Laws
===========================================================
A monoid is a function that takes two arguments and follows two laws: associativity and identity. 
A monoid is the typeclass that generalizes these laws across types.
A monoid is a binary associative operation with an identity.
   [1]         [2]       [3]        [4]              [5]

    1. The thing we’re talking about — monoids. That’ll end up being the name of our typeclass.
    2. Binary, i.e., two. So, there will be two of something.
    3. Associative — means the arguments can be regrouped (or reparenthesized, or reassociated) in 
       different orders and give the same result, as in addition. 
    4. Operation — so called because in mathematics, it’s usually used as an infix operator. 
       You can read this interchangeably as “func- tion.” Note that given the mention of “binary” 
       earlier, we know that this is a function of two arguments.
    5. Identity - means there exists some value such that when we pass it as input to our function, 
       the operation is rendered moot and the other value is returned, such as when we add zero or 
       multiply by one.

MONOIDS are strongly associated with catamorphisms
    CATAMORPHISM - folds (cata = downward)
    ANAMORPHISM - unfolds

For lists, the empty list, [], is the identity value:
    mappend [1..5] [] = [1..5] 
    mappend [] [1..5] = [1..5]

We can rewrite this as a more general rule, using mempty from the Monoid typeclass as a generic 
identity value:
    mappend x mempty = x 
    mappend mempty x = x

NOTE some types can have multiple monoids, such as Integrals which have 2: addition and multiplication
This is solved by creating special single constructor types using the NEWTYPE keyword, for instance:
    newtype Sum a = Sum {getSum :: a}
    newtype Product a = Product {getProduct :: a}
but this may best be expressed as finding the summation of the set, a better example of this is Bool 
with its conjunction and disjunction represented as All and Any


===========================================================
class Monoid m where
    mempty :: m
    mappend :: m -> m -> m    --(<>) operator
    mconcat :: [m] -> m
    mconcat = foldr mappend mempty

instance Monoid [a] where
    mempty = []
    mappend = (++)

-}

--Exercise: Optional Monoid PG 598
import Data.Monoid

data Optional a = 
      Nada
    | Only a
        deriving (Eq, Show)
{-
instance Monoid a => Monoid (Optional a) where
    mempty = undefined 
    mappend = undefined
-}
