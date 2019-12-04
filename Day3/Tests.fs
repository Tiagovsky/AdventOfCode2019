module Tests

open Xunit
open Program
open System

[<Theory>]
[<InlineData("R8,U5,L5,D3", 
             "U7,R6,D4,L4", 6)>]
[<InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", 
             "U62,R66,U55,R34,D71,R55,D58,R83", 146)>]
[<InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
             "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)>]
let ``Calculate Closest Intersection Test Cases`` (input1, input2, expected) =

    let rX, rY, rD = getClosestIntersection input1 input2

    Assert.Equal (expected, rD)

[<Fact>]
let ``Real Case Calculate Closest Intersection`` () =
    let rX, rY, rD = getClosestIntersectionFromFile "input.txt"
    Console.WriteLine rD

[<Theory>]
[<InlineData("R8,U5,L5,D3", 
             "U7,R6,D4,L4", 30)>]
[<InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", 
             "U62,R66,U55,R34,D71,R55,D58,R83", 610)>]
[<InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
             "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)>]
let ``Calculate Quickest Intersection Test Cases`` (input1, input2, expected) =

    let rX, rY, rD = getQuickestIntersection input1 input2

    Assert.Equal (expected, rD)

[<Fact>]
let ``Real Case Calculate Quickest Intersection`` () =
    let rX, rY, rD = getQuickestIntersectionFromFile "input.txt"
    Console.WriteLine rD

