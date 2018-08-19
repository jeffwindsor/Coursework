#!/bin/bash

COLOR_RESET="\e[0m";CHAR_BANNER='#';LINE="\n";
divider() { seq -s"${1}" 1 $(tput cols) | tr -d '[:digit:]'; }
foreground(){ printf "\e[38;5;%sm" $1;}
background(){ printf "\e[48;5;%sm" $1;}
printc(){ printf "$($1 $2)${3}${COLOR_RESET}"; }
printcln(){ printf "$(printc $1 $2 "${3}")${LINE}"; }
printcln_f(){ printcln "foreground" $1 "${2}"; }
printcln_b(){ printcln "background" $1 "${2}";}

section(){ div=$(divider '#'); printcln_b 20 "${div}${LINE}${1}${LINE}${div}"; }
info(){ printcln_f 28 "${1}"; }
warning(){ printcln_f 226 "${1}"; }
error(){ printcln_f 196 "${1}"; }
detail(){ printcln_f 244 "${1}"; }



:'
## MANUAL TESTS ## 
section "TEST"
info "Info"
warning "Warning"
error "Error"

println "foreground" $COLOR_SECTION "TEST"
print "foreground" $COLOR_ERROR "TEST"
'
