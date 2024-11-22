module Caller

open System
open System.IO
open Program
open Functions

[<EntryPoint>]
let main args =
    if args.Length <> 2 then
        printfn "Numero argomenti insufficiente."

    let dirConsegne = args.[0]
    let dirCodici = args.[1]

    controlloDirectory dirConsegne
    controlloDirectory dirCodici

    let getNomiNoEstensione dir = 
        Directory.EnumerateFiles(dir)
        |> Seq.map (fun file -> Path.GetFileNameWithoutExtension(file))

    let nomiFileNoExtConsegne = getNomiNoEstensione dirConsegne |> Set.ofSeq
    let nomiFileNoExtCodici = getNomiNoEstensione dirCodici |> Set.ofSeq

    let nomiFileComuni = Set.intersect nomiFileNoExtCodici nomiFileNoExtConsegne

    if Set.isEmpty nomiFileComuni then
        eprintf "Nessuna coppia di file consegna-codice trovata."
        Environment.Exit 2 |> ignore
    
    for nomeFile in nomiFileComuni do
        let fileConsegna =
            Directory.EnumerateFiles(dirConsegne, nomeFile + ".*")
            |> Seq.tryHead
        let fileCodice =
            Directory.EnumerateFiles(dirCodici, nomeFile + ".*")
            |> Seq.tryHead

        match fileConsegna, fileCodice with
            | Some consegna, Some codice ->
                try
                    Program consegna codice |> ignore
                with
                    | :? ArgumentOutOfRangeException as e ->
                        e.Message |> ignore
            | _ ->
                printfn "Coppia %s non completa" nomeFile
            
    0