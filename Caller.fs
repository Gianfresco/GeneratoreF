module Caller

open System
open System.IO
open Program
open Functions

[<EntryPoint>]
let main args =
    // controllo argomenti
    if args.Length <> 2 then
        printfn "Numero argomenti insufficiente."

    // assegnazione nomi agli argomenti
    let dirConsegne = args.[0]
    let dirCodici = args.[1]

    // controllo validità cartelle
    controlloDirectory dirConsegne
    controlloDirectory dirCodici

    // elencazione (enumerazione) del contenuto come sequenza e isolamento del nome del file dal percorso
    let getNomiNoEstensione dir = 
        Directory.EnumerateFiles(dir)
        |> Seq.map (fun file -> Path.GetFileNameWithoutExtension(file))

    // chiamate alla funzione antecedente sulle due cartelle date come argomenti 
    let nomiFileNoExtConsegne = getNomiNoEstensione dirConsegne |> Set.ofSeq
    let nomiFileNoExtCodici = getNomiNoEstensione dirCodici |> Set.ofSeq

    // intersezione dei file con lo stesso nome tra le due cartelle
    let nomiFileComuni = Set.intersect nomiFileNoExtCodici nomiFileNoExtConsegne

    // gestione del set di intersezioni uoto
    if Set.isEmpty nomiFileComuni then
        eprintf "Nessuna coppia di file consegna-codice trovata."
        Environment.Exit 2 |> ignore
    
    // per ogni "nomeFile" nel set di intersezioni
    for nomeFile in nomiFileComuni do
        // ricerca ed enumerazione di "nomeFile" nelle due cartelle
        let fileConsegna =
            Directory.EnumerateFiles(dirConsegne, nomeFile + ".*")
            |> Seq.tryHead
        let fileCodice =
            Directory.EnumerateFiles(dirCodici, nomeFile + ".*")
            |> Seq.tryHead


        match fileConsegna, fileCodice with
            // se entrabi hanno qualche tipo (Some) significa che i file sono stati trovati
            | Some consegna, Some codice ->
                try
                    // chiamata a "Program" con i due file
                    Program consegna codice |> ignore
                with
                    | :? ArgumentOutOfRangeException as e ->
                        e.Message |> ignore
            | _ ->
                printfn "Coppia %s non completa" nomeFile
            
    0