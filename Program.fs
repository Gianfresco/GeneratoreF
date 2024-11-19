module Program

open System
open System.IO
open System.Text

open Functions

[<EntryPoint>]

let main argv =

    let testoConsegna = (letturaFile argv.[0])
    let testoCodice = (letturaFile argv.[1])

    let main : bool = testoCodice.Contains("main")   // usato per determinare se il codice è una funzione o un programma
    let mutable tipoDomanda = getTipoFile(argv.[1])
    if tipoDomanda.EndsWith("_") then
        if main then
            tipoDomanda <- tipoDomanda + "program"
        else
            tipoDomanda <- tipoDomanda + "function"

    
    let mutable pos = 0
    pos <- pos + 231        // "cc xvv" -Romeo
    let insConsegna = inserisciTesto getTemplate testoConsegna pos
    pos <- pos + 230 + testoConsegna.Length
    let insTipo = inserisciTesto insConsegna tipoDomanda pos
    pos <- pos + 586 + testoConsegna.Length
    let insCodice = inserisciTesto insTipo testoCodice pos

    
    try
        File.WriteAllText(Path.GetFileName(argv.[0]), insCodice)
    with
        | :? UnauthorizedAccessException ->
            printfn "Accesso negato al percorso."
        | :? DirectoryNotFoundException ->
            printfn "Il file specificato non esiste."
        | :? IOException as ex ->
            printfn "Errore di I/O durante la scrittura del file: %s" ex.Message
        | ex ->
            printfn "Si è verificato un errore inatteso: %s" ex.Message


    0