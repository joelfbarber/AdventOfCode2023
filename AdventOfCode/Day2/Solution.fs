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
    gameResult.Handfuls
    |> Array.map (isHandfulPossible config)
    |> Array.forall (fun x -> x)

let getGameIdIfPossible (config: GameConfig) (gameResult: GameResult) =
    match isGamePossible config gameResult with
    | true -> Some gameResult.GameId
    | false -> None

let getMinimumGameConfig (gameResult: GameResult) : GameConfig =
    {
        MaxBlue = gameResult.Handfuls |> Array.map (fun x -> x.Blue) |> Array.max
        MaxGreen = gameResult.Handfuls |> Array.map (fun x -> x.Green) |> Array.max
        MaxRed = gameResult.Handfuls |> Array.map (fun x -> x.Red) |> Array.max
    }

let calculatePowerOfCubes (config: GameConfig) =
    config.MaxBlue * config.MaxGreen * config.MaxRed

let solvePart1() =
    let gameConfig = { MaxBlue = 14; MaxGreen = 13; MaxRed = 12 }
    let getGameIdIfPossible' = getGameIdIfPossible gameConfig
    
    File.ReadAllLines ".\Day2\input.txt"
    |> Array.map parseLine
    |> Array.choose getGameIdIfPossible'
    |> Array.sum

let solvePart2() =
    File.ReadAllLines ".\Day2\input.txt"
    |> Array.map parseLine
    |> Array.map getMinimumGameConfig
    |> Array.map calculatePowerOfCubes
    |> Array.sum