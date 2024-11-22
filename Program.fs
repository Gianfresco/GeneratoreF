module Program

open System
open System.IO
open System.Text

open Functions

let Program (fileConsegna:string, fileCodice:string) =

    if String.IsNullOrWhiteSpace(fileConsegna) || String.IsNullOrWhiteSpace(fileCodice) then
        eprintf "Parametri '%s' '%s' non validi." fileConsegna fileCodice
        Environment.Exit 1

    // lettura testi
    let testoTitoloConsegna = (letturaStringhe fileConsegna)
    let testoCodice = (letturaTesto fileCodice)

    // composizione tipo domanda a seconda del contenuto del codice solo in presenza di incertezze (terminanti con '_')
    let mutable tipoDomanda = getTipoFile(fileCodice)
    if tipoDomanda.Equals("UNCERTAIN") then
        printfn "Attenzione! '%s' è un file di testo: sarà necessaria la modifica manuale del linguaggio su Moodle." fileCodice
        tipoDomanda <- ""
    if tipoDomanda.EndsWith("_") then
        if testoCodice.Contains("main()") then          // usato per determinare se il codice è una funzione o un programma
            tipoDomanda <- tipoDomanda + "program"
        else
            tipoDomanda <- tipoDomanda + "function"

    // estrae il primo elemento ovvero il titolo della domanda (prima riga file)
    let testoTitolo = testoTitoloConsegna[0]
    // estrae e concatena il resto degli elementi, saltanto i primi due (titolo e 'newline' di separazione)
    let testoConsegna = String.concat ""(testoTitoloConsegna |> Array.skip 2)


    // aggiunta stringhe con punto di inserimento "pos" variabile
    let mutable pos = 0
    pos <- pos + 133
    let insTitolo = inserisciTesto getTemplate testoTitolo pos
    pos <- pos + 77 + testoTitolo.Length       // "cc xvv" -Romeo
    let insConsegna = inserisciTesto insTitolo testoConsegna pos
    pos <- pos + 251 + testoConsegna.Length
    let insTipo = inserisciTesto insConsegna tipoDomanda pos
    pos <- pos + 592 + tipoDomanda.Length
    let insCodice = inserisciTesto insTipo testoCodice pos

    // scrittura testo completo su file XML
    // il nome del file è lo stesso del nome del file contenente la consegna
    try
        File.WriteAllText((Path.GetFileNameWithoutExtension(fileConsegna) + ".xml"), insCodice)
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