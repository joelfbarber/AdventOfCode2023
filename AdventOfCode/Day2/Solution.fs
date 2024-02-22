module Day2

open System.IO

type HandfulOfCubes =
    {
        Green: int
        Blue: int
        Red: int
    }

type GameResult =
    {
        GameId: int
        Handfuls: HandfulOfCubes[]
    }

type GameConfig =
    {
        MaxGreen: int
        MaxRed: int
        MaxBlue: int
    }

let parseLine (line:string) =
    let lineSplit = line.Split([|':'; ';'|]) 
    let gameId = 
        let gameIdString = lineSplit |> Array.head
        gameIdString.Split(" ").[1] |> int
    let handfulStrings = lineSplit |> Array.tail //[| "4 red, 18 green, 15 blue"; "17 green, 18 blue, 9 red"; "8 red, 14 green, 6 blue"; "14 green, 12 blue, 2 red" |]
    let handfulOfCubes = 
        handfulStrings
        |> Array.map (fun x ->
            let colorCountDictionary = 
                x.Trim().Split(",")
                |> Array.map (fun y ->
                    match y.Trim().Split(" ") with
                    | [| count; color|] -> color, count |> int
                    | _ ->
                        failwith "Invalid input"
                )
                |> dict
            {
                Green = match colorCountDictionary.TryGetValue "green" with | true, value -> value | _ -> 0
                Blue = match colorCountDictionary.TryGetValue "blue" with | true, value -> value | _ -> 0
                Red = match colorCountDictionary.TryGetValue "red" with | true, value -> value | _ -> 0
            }
        )
    {
        GameId = gameId
        Handfuls = handfulOfCubes
    }

let isHandfulPossible (config:GameConfig) (handful: HandfulOfCubes) : bool =
    if handful.Blue > config.MaxBlue then
        false
    else if handful.Red > config.MaxRed then
        false
    else if handful.Green > config.MaxGreen then
        false
    else
        true

let isGamePossible (config:GameConfig) (gameResult: GameResult) : bool =
    let isHandfulPossible' = isHandfulPossible config

    let isGamePossible =
        gameResult.Handfuls
        |> Array.map (isHandfulPossible')
        |> Array.forall (fun x -> x)

    isGamePossible

let getMinimumGameConfig (gameResult: GameResult) : GameConfig =
    //gameResult.Handfuls
    {
        MaxBlue = 1
        MaxGreen = 1
        MaxRed = 1
    }

let solvePart1() =
    let lines = File.ReadAllLines ".\Day2\input.txt"
    let gameResults = lines |> Array.map parseLine

    let gameConfig = { MaxBlue = 14; MaxGreen = 13; MaxRed = 12 }
    let isGamePossible' = isGamePossible gameConfig

    let sumOfPossibleGameIds = 
        gameResults 
        |> Array.choose(fun game -> 
            let isPossible = isGamePossible' game
            match isPossible with
            | true -> Some game.GameId
            | false -> None
        )
        |> Array.sum

    sumOfPossibleGameIds

let solvePart2() =
    let lines = File.ReadAllLines ".\Day2\input.txt"
    let gameResults = lines |> Array.map parseLine


    0