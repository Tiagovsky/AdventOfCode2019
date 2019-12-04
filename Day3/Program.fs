module Program

open Common.Files
open Common.Operators
open System

let parseInput (input:string) =
    let ParseDirection (direction:string) =
        (direction.[0], int (direction.Substring 1))

    input.Split ','
    |> Array.map ParseDirection
    |> Array.toList

let getnextCoordinate walkedPath direction distance =
    let getLastCoordinate =
        if (List.length walkedPath = 0) then
            (0, 0, 0)
        else
            List.last walkedPath

    let X, Y, Z = getLastCoordinate
    match direction with
    | 'U' -> (X, Y + distance, Z + distance)
    | 'D' -> (X, Y - distance, Z + distance)
    | 'L' -> (X - distance, Y, Z + distance)
    | 'R' -> (X + distance, Y, Z + distance)
    | _   -> (X, Y, Z)

let rec calculateWirePath (walkedPath:list<int*int*int>) (input:list<char*int>) =
    if (input.Length = 0) then
        Array.ofList walkedPath 
    else
        let direction, distance = input.Head
        let nextCoordinate = getnextCoordinate walkedPath direction distance
        calculateWirePath (walkedPath @ [nextCoordinate]) input.Tail

let getIntersection (i0:int*int*int) (i1:int*int*int) (j0:int*int*int) (j1:int*int*int) =
    let isLineHorizontal lineBegin lineEnd = first lineBegin = first lineEnd
    let areLinesParallel = isLineHorizontal i0 i1 = isLineHorizontal j0 j1
    let isValueInBetween (v:int) (comp1:int) (comp2:int) = v >= Math.Min(comp1, comp2) 
                                                            && v <= Math.Max(comp1, comp2)
    let getTotalStepsForPointInLine (lineBegin:int*int*int) (point:int*int) =
        let delta = if ((first lineBegin) = (fst point)) then 
                        second lineBegin - snd point |> Math.Abs
                    else first lineBegin - fst point |> Math.Abs
        third lineBegin + delta
    if areLinesParallel then
        None
    else
        let possibleX = if (isLineHorizontal i0 i1) then first i0 else first j0
        let possibleY = if (isLineHorizontal j0 j1) then second i0 else second j0
        let doesFitInLines = isValueInBetween possibleX (first i0) (first i1)
                            && isValueInBetween possibleX (first j0) (first j1)
                            && isValueInBetween possibleY (second i0) (second i1)
                            && isValueInBetween possibleY (second j0) (second j1)
        let stepTotal = getTotalStepsForPointInLine i0 (possibleX, possibleY) 
                        + getTotalStepsForPointInLine j0 (possibleX, possibleY) 
        if (doesFitInLines && not (possibleX = 0 && possibleY = 0)) then
            Some (possibleX, possibleY, stepTotal)
        else
            None
        

let calculateIntersections (path1:(int*int*int)[]) (path2:(int*int*int)[]) =
    let getPreviousCoordinate index (path:(int*int*int)[]) =
        if (index = 0) then (0, 0, 0) else (path.[index-1])
    [|
        for i in 0 .. path1.Length - 1 do
            for j in 0 .. path2.Length - 1 do
                let i0 = getPreviousCoordinate i path1
                let j0 = getPreviousCoordinate j path2
                let intersection = getIntersection i0 path1.[i] j0 path2.[j]
                if (intersection.IsSome) then
                    yield intersection.Value

    |]

let getClosestIntersection input1 input2 =
        let path1 = calculateWirePath List.empty (parseInput input1)
        let path2 = calculateWirePath List.empty (parseInput input2)
        calculateIntersections path1 path2
        |> Array.map (fun (x,y,_) -> (x, y, x+y))
        |> Array.sortBy (fun (x,y,d) -> d)
        |> Array.head

let getClosestIntersectionFromFile filePath =
    let lines = getLineValuesFromFilePath filePath
    getClosestIntersection lines.[0] lines.[1]

let getQuickestIntersection input1 input2 =
        let path1 = calculateWirePath List.empty (parseInput input1)
        let path2 = calculateWirePath List.empty (parseInput input2)
        calculateIntersections path1 path2
        |> Array.sortBy (fun (_,_y,d) -> d)
        |> Array.head

let getQuickestIntersectionFromFile filePath =
    let lines = getLineValuesFromFilePath filePath
    getQuickestIntersection lines.[0] lines.[1]