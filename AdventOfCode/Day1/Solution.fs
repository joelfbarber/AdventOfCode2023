module Day1

open System.IO

let findFirstAndLastDigit (line:string) =
    let digits = [|'1';'2';'3';'4';'5';'6';'7';'8';'9'|]
    let firstDigit = line.[line.IndexOfAny digits]
    let lastDigit = line.[line.LastIndexOfAny digits]
    firstDigit, lastDigit

let parseLinePart1 (line:string) : int =
    let firstDigit, lastDigit = findFirstAndLastDigit line
    int <| (new string([|firstDigit; lastDigit|]))

let parseLinePart2 (line:string) : int =
    let mutable mutatedLine = line
    let stringNumbers = 
        [|
            "one", "o1e"
            "two", "t2o"
            "three", "th3ee"
            "four", "fo4r"
            "five", "fi5e"
            "six", "s6x"
            "seven", "se7en"
            "eight", "ei8ht"
            "nine", "ni9e"
        |]

    stringNumbers
    |> Array.iter(fun x ->
        let find, replacement = x |> fst, x |> snd
        mutatedLine <- mutatedLine.Replace(find, replacement)
    )

    let firstDigit, lastDigit = findFirstAndLastDigit mutatedLine

    int <| (new string([|firstDigit; lastDigit|]))

let solve (inputLines:string array) (parseLine: string -> int) : int =
    inputLines
    |> Array.map parseLine
    |> Array.sum

let solvePart1() =
    let input = File.ReadAllLines ".\Day1\input.txt"
    solve input parseLinePart1

let solvePart2() =
    let input = File.ReadAllLines ".\Day1\input.txt"
    solve input parseLinePart2

