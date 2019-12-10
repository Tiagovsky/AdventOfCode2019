module Program

open System
open Common.Files

let is6Digits (input:string) =
    input.Length = 6

let isAscendant (input:string) =
    let orderedInput = new System.String(Array.sort (explode input))
    input = orderedInput

let hasAdjacency (input:string) =
    [|1 .. input.Length - 1|]
    |> Array.exists (fun i -> input.[i] = input.[i-1])

let hasSingleAdjacency (input:string) =
    let getCountOccurencesInString c = String.filter ((=) c) input |> String.length

    [|1 .. input.Length - 1|]
    |> Array.exists (fun i -> input.[i] = input.[i-1] 
                            && getCountOccurencesInString input.[i] = 2)

let isValidPassword rules input =
    rules
    |> Array.forall (fun x -> x input)

let isValidPasswordWithOldRules input =
    isValidPassword [|is6Digits; isAscendant; hasAdjacency|] input

let isValidPasswordWithNewRules input =
    isValidPassword [|is6Digits; isAscendant; hasSingleAdjacency|] input

let getPasswordCountInRange validPasswordFun lowerBound upperBound =
    [|lowerBound .. upperBound|]
    |> Array.map string
    |> Array.where validPasswordFun
    |> Array.length

let getPasswordCountInRangeWithOldRules lowerBound upperBound =
    getPasswordCountInRange isValidPasswordWithOldRules lowerBound upperBound
    

let getPasswordCountInRangeWithNewRules lowerBound upperBound =
    getPasswordCountInRange isValidPasswordWithNewRules lowerBound upperBound
    
