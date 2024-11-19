module Program

open System
open System.IO
open System.Text

open Functions

[<EntryPoint>]

let main argv =

    let testoConsegna = (letturaFile argv.[0])
    let testoCodice = (letturaFile argv.[1])

    let mutable tipoDomanda = getTipoFile(argv.[1])
    if tipoDomanda.EndsWith("_") then
        if testoCodice.Contains("main()") then          // usato per determinare se il codice è una funzione o un programma
            tipoDomanda <- tipoDomanda + "program"
        else
            tipoDomanda <- tipoDomanda + "function"

    
    let mutable pos = 0
    pos <- pos + 210        // "cc xvv" -Romeo
    let insConsegna = inserisciTesto getTemplate testoConsegna pos
    pos <- pos + 251 + testoConsegna.Length
    let insTipo = inserisciTesto insConsegna tipoDomanda pos
    pos <- pos + 569 + testoConsegna.Length
    let insCodice = inserisciTesto insTipo testoCodice pos

    
    try
        File.WriteAllText((Path.GetFileNameWithoutExtension(argv.[0]) + ".xml"), insCodice)
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