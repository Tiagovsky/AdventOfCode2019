module Program

open Common.Files
open System

let CalculateFuel mass =
    let value = int 
                <| Math.Floor (float mass / 3.0);
    value - 2

let CalculateFuelWithFuelMass mass = 
    let rec CalculateFuelWithFuelMassRec mass acc =
        let fuelValue = Math.Max (0, CalculateFuel mass)

        if (fuelValue = 0) then
            acc
        else
            acc + fuelValue
            |> CalculateFuelWithFuelMassRec fuelValue

    CalculateFuelWithFuelMassRec mass 0

let CalculateTotalFuel (fuelFunction:int -> int) massArr =
    massArr
    |> Array.map fuelFunction
    |> Array.sum

let CalculateFuelFromFile filePath =
    getLineValuesFromFilePath filePath
    |> Array.map int
    |> CalculateTotalFuel CalculateFuel

let CalculateFuelWithFuelMassFromFile filePath =
    getLineValuesFromFilePath filePath
    |> Array.map int
    |> CalculateTotalFuel CalculateFuelWithFuelMass