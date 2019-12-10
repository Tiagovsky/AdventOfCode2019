module Tests

open System
open Xunit
open Program

[<Theory>]
[<InlineData("111111", true)>]
[<InlineData("223450", false)>]
[<InlineData("123789", false)>]
let ``Calculate Valid Password With Old Rules Test Cases`` (input, expected) =

    let result = isValidPasswordWithOldRules input

    Assert.Equal (expected, result)

[<Fact>]
let ``Real Case Calculate Valid Password With Old Rules`` () =
    let result = getPasswordCountInRangeWithOldRules 307237 769058
    Console.WriteLine result

[<Theory>]
[<InlineData("112233", true)>]
[<InlineData("123444", false)>]
[<InlineData("111122", true)>]
let ``Calculate Valid Password With New Rules Test Cases`` (input, expected) =

    let result = isValidPasswordWithNewRules input

    Assert.Equal (expected, result)

[<Fact>]
let ``Real Case Calculate Valid Password With New Rules`` () =
    let result = getPasswordCountInRangeWithNewRules 307237 769058
    Console.WriteLine result