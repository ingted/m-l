﻿module ML.Core.Readers

let readLines (filePath:string) = seq {
    use sr = new System.IO.StreamReader (filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


    

// raed csv, all columns contain decimal numbers
let readCSV (filePath:string) isHeader (inputCols: int []) (outputCol: int) = 
    
    let mapLine (str: string) = 
        let cols = str.Split([|','|])
        let outs = seq { for i in inputCols -> System.Double.Parse(string cols.[i]) } |> Seq.toList
        outs, System.Double.Parse(cols.[outputCol])
        
    readLines filePath
    |> Seq.skip (if isHeader then 1 else 0)
    |> Seq.map mapLine
    |> Seq.toList
    |> List.unzip        


let readCSV2 (filePath:string) isHeader (inputCols: int []) (outputCol: int) (cnt: int) = 
    
    let mapLine (str: string) = 
        let cols = str.Split([|','|])
        let outs = seq { for i in inputCols -> System.Double.Parse(string cols.[i]) } |> Seq.toList
        outs, System.Double.Parse(cols.[outputCol])
        
    readLines filePath
    |> Seq.skip (if isHeader then 1 else 0)
    |> Seq.take cnt
    |> Seq.map mapLine
    |> Seq.toList
    |> List.unzip        

let readCSV3 (filePath:string) isHeader (inputCols: int []) (outputCol: int) (outputCats: Map<string, float>) = 
    
    let mapLine (str: string) = 
        let cols = str.Split([|','|])
        let outs = seq { for i in inputCols -> System.Double.Parse(string cols.[i]) } |> Seq.toList
        outs, outputCats.[cols.[outputCol]]
        
    readLines filePath
    |> Seq.skip (if isHeader then 1 else 0)
    |> Seq.map mapLine
    |> Seq.toList
    |> List.unzip        

