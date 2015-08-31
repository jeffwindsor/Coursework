
type Organism = Dominant | Heterozygous | Recessive
type Ecosystem = { Dominant : float; Heterozygous : float; Recessive : float }
let ecosystem d h r = { Dominant = d; Heterozygous = h; Recessive= r}
  
let probOfDominantPhenotype e a b=
    let without e = function
        | Dominant -> ecosystem (e.Dominant-1.0) e.Heterozygous e.Recessive
        | Heterozygous -> ecosystem e.Dominant (e.Heterozygous-1.0) e.Recessive
        | Recessive -> ecosystem e.Dominant e.Heterozygous (e.Recessive-1.0)

    let probOfSelection e org = 
        let probOfSelection' n = n / (e.Dominant + e.Heterozygous + e.Recessive)
        match org with
        | Dominant -> probOfSelection' e.Dominant
        | Heterozygous -> probOfSelection' e.Heterozygous
        | Recessive -> probOfSelection' e.Recessive

    let probOfDominantGene a b = 
        match a,b with
        | Dominant, _ | _, Dominant  -> 1.0
        | Heterozygous, Heterozygous -> 0.75
        | Heterozygous, Recessive | Recessive, Heterozygous -> 0.5
        | _, _                       -> 0.0

    let pa = probOfSelection e a 
    let pb = probOfSelection (without e a) b
    let pg = probOfDominantGene a b
    let result = pa * pb * pg
    //printfn "%A" (a, b, pa, pb, pg, result)
    result
    
let cartesian xs ys = 
    xs |> List.collect (fun x -> ys |> List.map (fun y -> x, y))

let mendelsLaw k m n =
    let e = ecosystem k m n
    let types = [Dominant; Heterozygous; Recessive]
    
    cartesian types types
    |> List.map (fun (a, b) -> probOfDominantPhenotype e a b)
    |> List.sum


let test0 = mendelsLaw 18.0 26.0 24.0