module Day1.Tests

open Xunit

[<Fact>]
let ``Day1 Part1 answer is 54388`` () =
    let actual = Day1.solvePart1()
    Assert.Equal(54388, actual)

[<Fact>]
let ``Day1 Part2 answer is 53515`` () =
    let actual = Day1.solvePart2()
    Assert.Equal(54388, actual)