namespace CIS194

module TowerOfHanoi =
    type Peg = string
    type Disc = int
    type Move = {fromPeg:Peg; toPeg:Peg; discNumber:Disc} 
    
    let private move fromPeg toPeg discNumber = [ {fromPeg=fromPeg; toPeg=toPeg; discNumber=discNumber} ]
    
    let rec hanoi discs source temp target =
        match discs with
        | 0 -> [ ]
        | _ -> (hanoi (discs-1) source target temp) 
                @ move source target discs
                @ (hanoi (discs-1) temp source target)

    let rec hanoiK discs pegs =
        match discs, pegs with
        | 0, _           -> [ ]
        //| 1, (source::target::_) -> move source target discs
        | n, (source::temp::target::[]) 
            -> (hanoiK (discs-1) pegs) @
               move source target discs @
               (hanoiK (discs-1) (target::temp::source::[]))

        | n, (source::temp::target::others) 
            -> let k = if others.IsEmpty then discs-1 else int(discs/2)
               (hanoiK k (source::target::temp::others)) @ 
               (hanoiK (discs-k) (source::target::others)) @ 
               (hanoiK k (target::temp::source::others))

        | _ -> failwith "Well that was unexpected"


    //=====================================================================
    // Tests
    //=====================================================================
    let private printMoves moves = 
        moves |> List.iter (fun move -> printf "Move disc %i from %A to %A\n" move.discNumber move.fromPeg move.toPeg)

    //let testHanoi = printMoves (hanoi 4 "a" "b" "c")

    let testHanoiK = printMoves (hanoiK 4 ["a";"b";"c";"d"])