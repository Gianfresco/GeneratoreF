#!/bin/bash

DIR_CONSEGNE=$1
shift
DIR_CODICI=$2
shift

ESEGUIBILE=GeneratoreF

if test "$($#)" -lt 2
then
    echo "Argomenti isufficienti, uso: $0 <dir_dirConsegne> <dirDIR_CODICI>"
    exit 1
fi

if test ! -x "$(DIR_CONSEGNE)" -o ! -d  "$(DIR_CONSEGNE)"
then
    echo "$DIR_CONSEGNE non è una directory o non si hanno i permessi necessari"
    exit 2
fi

if test ! -x "$(DIR_CODICI)" -o ! -d  "$(DIR_CODICI)"
then
    echo "$DIR_CODICI non è una directory o non si hanno i permessi necessari"
    exit 2
fi

FILES_DIR_CONSEGNE=$(find "$DIR_CONSEGNE" -maxdepth 1 -type f -printf "%f\n")
FILES_DIR_CODICI=$(find "$DIR_CODICI" -maxdepth 1 -type f -printf "%f\n")

NOMEFILE_NOEXT_DIR_CONSEGNE=$(echo "$FILES_DIR_CONSEGNE" | sed 's/\.[^.]*$//' | sort | uniq)
NOMEFILE_NOEXT_DIR_CODICI=$(echo "$FILES_DIR_CODICI" | sed 's/\.[^.]*$//' | sort | uniq)

NOMIFILE_COM=$(comm -12 <(echo "$NOMEFILE_NOEXT_DIR_CONSEGNE") <(echo "$NOMEFILE_NOEXT_DIR_CODICI"))

if test -z "$NOMIFILE_COM"
then
    echo "Nessuna coppia di file trovata."
    exit 4
fi

for FILE in $NOMIFILE_COM
do
    FILE_CONSEGNA=$(find "$DIR_CONSEGNE" -maxdepth 1 -type f -name "${FILE}.*" | head -n 1)
    FILE_CODICE=$(find "$DIR_CODICI" -maxdepth 1 -type f -name "${FILE}.*" | head -n 1)

    if test -f "$FILE_CONSEGNA" -a test -f "$FILE_CODICE"
    then
        echo "Esecuzione di $ESEGUIBILE con $FILE_CONSEGNA e $FILE_CODICE"
        "$ESEGUIBILE" "$FILE_CONSEGNA" "$FILE_CODICE"

        if test $? -ne 0
        then
            echo "Attenzione, l'esecuzione con $FILE_CONSEGNA e $FILE_CODICE ha generato un errore."
        fi
    else
        echo "Attenzione, un file nella coppia $FILE è mancante."
    fi
done

exit 0