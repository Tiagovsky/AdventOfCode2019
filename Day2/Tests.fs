module Tests

open Xunit
open Program
open System

[<Theory>]
[<InlineData("1,0,0,0,99", "2,0,0,0,99")>]
[<InlineData("2,3,0,3,99", "2,3,0,6,99")>]
[<InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")>]
[<InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")>]
let ``Calculate IntCode Test Cases`` ((input:string), expected) =

    let result = CalculateIntCode (Array.map int
                 <| input.Split ',')

    //let result = CalculateIntCode input
    Assert.Equal (expected, String.Join (',', result))

[<Fact>]
let ``Real Case Calculate IntCode`` () =
    let replacements = seq { (1, 12); (2, 2) }
    let result = CalculateIntCodeFromFile "input.txt" replacements
    Console.WriteLine result.[0]

[<Fact>]
let ``Real Case Calculate IntCode and Find Result`` () =

    for i in 0 .. 99 do
        for j in 0 .. 99 do
            let replacements = seq { (1, i); (2, j) }
            let result = CalculateIntCodeFromFile "input.txt" replacements
            if (result.[0] = 19690720) then
                printfn "%i" (100 * i + j)
