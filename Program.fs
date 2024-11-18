open System
open System.IO
open System.Text

let letturaFile (percorso:string) : string =
    try
        let contenuto = File.ReadAllText(percorso)
        contenuto
    with
        | :? IOException as e ->
            e.Message
        | e ->
            e.Message

let getTemplate : string =
    let template = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>
<quiz>
  <!-- question: 0  -->
  <question type=\"coderunner\">
    <name>
      <text><![CDATA[]]></text>
    </name>
    <questiontext format=\"html\">
      <text><![CDATA[]]></text>
    </questiontext>
    <generalfeedback format=\"html\">
      <text><![CDATA[]]></text>
    </generalfeedback>
    <defaultgrade>1</defaultgrade>
    <penalty>0</penalty>
    <hidden>0</hidden>
    <idnumber></idnumber>
    <coderunnertype></coderunnertype>
    <prototypetype>0</prototypetype>
    <allornothing>1</allornothing>
    <penaltyregime>10, 20, ...</penaltyregime>
    <precheck>0</precheck>
    <hidecheck>0</hidecheck>
    <showsource>0</showsource>
    <answerboxlines>18</answerboxlines>
    <answerboxcolumns>100</answerboxcolumns>
    <answerpreload><![CDATA[]]></answerpreload>
    <globalextra></globalextra>
    <useace></useace>
    <resultcolumns></resultcolumns>
    <template></template>
    <iscombinatortemplate></iscombinatortemplate>
    <allowmultiplestdins></allowmultiplestdins>
    <answer><![CDATA[]]></answer>
    <validateonsave>1</validateonsave>
    <testsplitterre></testsplitterre>
    <language></language>
    <acelang></acelang>
    <sandbox></sandbox>
    <grader></grader>
    <cputimelimitsecs></cputimelimitsecs>
    <memlimitmb></memlimitmb>
    <sandboxparams></sandboxparams>
    <templateparams></templateparams>
    <hoisttemplateparams>1</hoisttemplateparams>
    <extractcodefromjson>1</extractcodefromjson>
    <templateparamslang>None</templateparamslang>
    <templateparamsevalpertry>0</templateparamsevalpertry>
    <templateparamsevald>{}</templateparamsevald>
    <twigall>0</twigall>
    <uiplugin></uiplugin>
    <uiparameters></uiparameters>
    <attachments>0</attachments>
    <attachmentsrequired>0</attachmentsrequired>
    <maxfilesize>10240</maxfilesize>
    <filenamesregex></filenamesregex>
    <filenamesexplain></filenamesexplain>
    <displayfeedback>1</displayfeedback>
    <giveupallowed>0</giveupallowed>
    <prototypeextra></prototypeextra>
    <testcases></testcases>
  </question>

</quiz>"
    template

let inserisciTesto (testo:string) (aggiunta:string) (posizione:int) : string =
    if posizione < 0 || posizione > testo.Length then
        printfn "Errore: posizione al di fuori dei limiti del testo"
        testo
    else
        let sx = testo.Substring(0, posizione)
        let dx = testo.Substring(posizione)
        sx + aggiunta + dx

let getTipoFile (file:string) : string =
    let estensione = Path.GetExtension(file)
    match estensione with
    | ".txt" -> "UNCERTAIN"
    | ".graph" -> "undirected_graph"
    | ".java" -> "java_program"
    | ".m" -> "octave_function"
    | ".pas" -> "pascal_"
    | ".js" -> "node_js"
    | ".cpp" -> "cpp_"
    | ".php" -> "php"
    | ".sql" -> "sql"
    | ".c" -> "c_"
    | _ -> "Undefined"

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

    File.WriteAllText("out.xml", insCodice)

    // try
    //     File.WriteAllText(percorsoFileOut, insCodice)
    // with
    //     | :? UnauthorizedAccessException ->
    //     printfn "Accesso negato al percorso '%s'." percorsoFileOut
    //     | :? DirectoryNotFoundException ->
    //         printfn "Il file specificato '%s' non esiste." percorsoFileOut
    //     | :? IOException as ex ->
    //         printfn "Errore di I/O durante la scrittura del file: %s" ex.Message
    //     | ex ->
    //         printfn "Si è verificato un errore inatteso: %s" ex.Message


    0