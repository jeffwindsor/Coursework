## Symbol Guide

    $     function application (used to avoid parentheses)
    &     reverse function application (like a Unix pipe)
    .     right-to-left function composition
    ++    list append
    <>    semigroup append
    <$>   infix fmap (like ($) for functors)
    <&>   flipped infix fmap (like (&) for functors)
    <*>   applicative apply
    <$    replace all elements with a value (fmap . const)
    $>    flipped version of (<$)
    <*    like (<*>), but ignore result of second argument
    *>    like (<*>), but ignore result of first argument
    >>=   bind
    =<<   flipped bind
    >=>   left-to-right monadic composition
    <=<   right-to-left monadic composition
    <|>   alternative or
    <-    bind (when used in do block)
    ~     type equality constraint
    =>    is a constraint on
    ~>    natural transformation