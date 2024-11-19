module Functions

open System
open System.IO
open System.Text

// funzione che fornisce il testo con i campi vuoti
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

// funzione che legge il file dato come argomento e ne restuisce il contenuto come stringa unica
let letturaTesto (percorso:string) : string =
    try
        let contenuto = File.ReadAllText(percorso)
        contenuto
    with
        | :? IOException as e ->
            eprintfn "Errore di I/O"
            e.Message |> ignore
            null
        | e ->
            eprintfn "Errore inatteso"
            e.Message |> ignore
            null

// funzione che legge il file dato come argomento e ne restuisce il contenuto come array di stringhe
let letturaStringhe (percorso:string) : string[] =
    try
      let contenuto = File.ReadAllLines(percorso)
      contenuto
    with
      | :? IOException as e ->
          eprintfn "Errore di I/O"
          e.Message |> ignore
          null
      | e ->
          eprintf "Errore inatteso"
          e.Message |> ignore
          null


// funzione per l'inserimento del testo (con controllo della posizione)
let inserisciTesto (testo:string) (aggiunta:string) (posizione:int) : string =
    if posizione < 0 || posizione > testo.Length then
        printfn "Errore: posizione al di fuori dei limiti del testo"
        testo
    else
        // spezza il testo originale nella posizione indicata e ne inserisce l'aggiunta in mezzo
        let sx = testo.Substring(0, posizione)
        let dx = testo.Substring(posizione)
        sx + aggiunta + dx

// funzione che, a seconda dell'estesione del nome del file dato in argomento, restituisce il tipo di file
let getTipoFile (file:string) : string =
    let estensione = Path.GetExtension(file)
    match estensione with
    | ".txt" -> "UNCERTAIN"           // incerto, perchè .txt poterbbe essere un directed_graph o un undirected_graph, quindi è necessario l'intervento dell'utente.
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