#!/bin/sh

consegne=$1
shift
codici=$2
shift

if test "$($#)" -lt 2
then
    echo "Argomenti isufficienti, uso: $0 <dirConsegne> <dirCodici>"
    exit 1
fi

if test ! -x "$(consegne)" -o ! -d  "$(consegne)"
then
    echo "$consegne non è una directory o non si hanno i permessi necessari"
    exit 2
fi

if test ! -x "$(codici)" -o ! -d  "$(codici)"
then
    echo "$codici non è una directory o non si hanno i permessi necessari"
    exit 2
fi

