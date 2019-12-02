module Tests

open Xunit
open Program
open System

[<Theory>]
[<InlineData(12, 2)>]
[<InlineData(14, 2)>]
[<InlineData(1969, 654)>]
[<InlineData(100756, 33583)>]
let ``Calculate Fuel Test Cases`` (input:int, expected:int) =
    let result = CalculateFuel input
    Assert.Equal (expected, result)

[<Fact>]
let ``Real Case Calculate Fuel`` () =
    let result = CalculateFuelFromFile "input.txt"
    Console.WriteLine result

[<Theory>]
[<InlineData(12, 2)>]
[<InlineData(1969, 966)>]
[<InlineData(100756, 50346)>]
let ``Calculate Fuel With Fuel Mass Test`` (input:int, expected:int) =
    let result = CalculateFuelWithFuelMass input
    Assert.Equal (expected, result)

[<Fact>]
let ``Real Case Calculate Fuel With FuelMass`` () =
    let result = CalculateFuelWithFuelMassFromFile "input.txt"
    Console.WriteLine result