#!/bin/bash

if [ -z "$1" ]
  then
    msg="Aggiornamento al `date`"
  else
    msg="$1"
fi

git pull && git add . && git commit -m "$msg" && git push && git status