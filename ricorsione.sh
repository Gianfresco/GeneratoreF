#!/bin/sh

if test "$($#)" -lt 1
then
    echo "Argomenti isufficienti, uso: $0 <dir>"
    exit 1
fi

if test ! -x "$(1)" -o ! -d  "$(1)"
then
    echo "$1 non è una directory o non si hanno i permessi necessari"
    exit 2
fi



