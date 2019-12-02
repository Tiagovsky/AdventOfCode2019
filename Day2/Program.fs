module Program

open Common.Files

let resolveValue (operations:int[]) i =
    operations.[operations.[i]]

let setValue (operations:int[]) value i =
    operations.[operations.[i]] <- value

let ProcessOperation operation operations i =
    let operand1 = resolveValue operations <| i + 1
    let operand2 = resolveValue operations <| i + 2
    setValue operations (operation operand1 operand2) (i + 3)
    operations

let rec ProcessIntCode i (operations:int[]) =
    let operation = operations.[i]
    match operation with
        | 99 -> operations
        | 1 -> ProcessOperation (fun x y -> x + y) operations i
               |> ProcessIntCode (i + 4)
        | 2 -> ProcessOperation (fun x y -> x * y) operations i
               |> ProcessIntCode (i + 4)
        | _ -> operations
        

let CalculateIntCode operations =
    operations
    |> ProcessIntCode 0

let ReplaceValues replacements (operations:int[]) =
    for replacement in replacements do
        operations.[fst replacement] <- snd replacement
    operations

let CalculateIntCodeFromFile filePath replacements =
    (getLineValuesFromFilePath filePath).[0].Split ','
    |> Array.map int
    |> ReplaceValues replacements
    |> CalculateIntCode

