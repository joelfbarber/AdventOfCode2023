module Day3

open System.IO

let parseText (text: string) : char[][] =
    text.Split([|'\n'; '\r'|], System.StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun line -> line.ToCharArray())

let parsedText = 
    File.ReadAllText ".\Day3\input.txt"
    |> parseText


