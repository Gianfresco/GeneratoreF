module Program

open System
open System.IO
open System.Text

open Functions

[<EntryPoint>]

let main argv =

    // lettura testi
    let testoConsegna = (letturaFile argv.[0])
    let testoCodice = (letturaFile argv.[1])

    // composizione tipo domanda a seconda del contenuto del codice
    // solo in presenza di incertezze (terminanti con '_')
    let mutable tipoDomanda = getTipoFile(argv.[1])
    if tipoDomanda.Equals("UNCERTAIN") then
        printfn "Attenzione! '%s' è un file di testo: sarà necessaria la modifica manuale del linguaggio su Moodle." argv.[1]
        tipoDomanda <- ""
    if tipoDomanda.EndsWith("_") then
        if testoCodice.Contains("main()") then          // usato per determinare se il codice è una funzione o un programma
            tipoDomanda <- tipoDomanda + "program"
        else
            tipoDomanda <- tipoDomanda + "function"

    // aggiunta stringhe con punto di inserimento "pos" variabile
    let mutable pos = 0
    pos <- pos + 210        // "cc xvv" -Romeo
    let insConsegna = inserisciTesto getTemplate testoConsegna pos
    pos <- pos + 251 + testoConsegna.Length
    let insTipo = inserisciTesto insConsegna tipoDomanda pos
    pos <- pos + 569 + testoConsegna.Length
    let insCodice = inserisciTesto insTipo testoCodice pos

    // scrittura testo completo su file XML
    // il nome del file è lo stesso del nome del file contenente la consegna
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