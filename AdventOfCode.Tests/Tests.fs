namespace AdventOfCode.Tests

open Xunit

module Day1Tests =
    [<Fact>]
    let ``Day1 Part1 answer is 54388`` () =
        let actual = Day1.solvePart1()
        Assert.Equal(54388, actual)

    [<Fact>]
    let ``Day1 Part2 answer is 53515`` () =
        let actual = Day1.solvePart2()
        Assert.Equal(53515, actual)

module Day2Tests =
    [<Fact>]
    let ``Day2 Part1 answer is 2879`` () =
        let actual = Day2.solvePart1()
        Assert.Equal(2879, actual)