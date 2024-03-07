module Day3

open System
open System.IO

type IsAdjacentToPartNumber = bool
type xPosition = int
type yPosition = int
type Location = xPosition * yPosition
type Schematic = char[][]

type SchematicNumber =
    {
        Number: int
        Location: Location
    }

type SchematicSpecialCharacter =
    {
        Location: Location
    }

type SchematicCharType =
    | Dot
    | Number of SchematicNumber
    | SpecialCharacter of SchematicSpecialCharacter

type ParsedSchematic = SchematicCharType array

let isDot (char:char) =
    char = '.'

let isSpecialCharacter (char:char) =
    char |> Char.IsSymbol && not (char |> isDot)

let parseSchematicCharacter (char: char) (xPosition, yPosition) = 
    match char with
    | x when x |> isSpecialCharacter = true ->
        SchematicCharType.SpecialCharacter { Location = (xPosition, yPosition) }
    | x when x |> Char.IsNumber = true ->
        SchematicCharType.Number { Number = x |> int; Location = (xPosition, yPosition) }
    | _ -> SchematicCharType.Dot

let parseSchematic (schematic:Schematic) : ParsedSchematic =
    schematic
    |> Array.mapi (fun i schematicRow ->
        schematicRow
        |> Array.mapi (fun j schematicChar ->
            parseSchematicCharacter schematicChar (i, j)
        )
    )
    |> Array.concat

let parseText (text: string) : Schematic =
    text.Split([|'\n'; '\r'|], System.StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun line -> line.ToCharArray())

let solvePart1() =
    File.ReadAllText ".\Day3\input.txt"
    |> parseText
    |> parseSchematic
    |> printf "%A"
    ()