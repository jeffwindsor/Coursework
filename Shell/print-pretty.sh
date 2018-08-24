#!/bin/bash
fg(){ printf "\e[38;5;%sm" $1;} ## $1 COLOR
bg(){ printf "\e[48;5;%sm" $1;} ## $1 COLOR
eof(){ printf "\e[0m"; }
fill() { printf "${1}$(seq -s "${2}" $((${#1} + 1)) $(tput cols) | tr -d '[:digit:]')"; } ## $1 STRING, $REPEAT_CHAR

info()   { echo -e "$(bg 28)$(fg 255)$(fill "${1}" " ")$(eof)";  }
warning(){ echo -e "$(bg 208)$(fg 0)$(fill "${1}" " ")$(eof)";  }
error()  { echo -e "$(bg 196)$(fg 255)$(fill "${1}" " ")$(eof)";  }
detail() { echo -e "$(fg 250)$(fill "${1}" " ")$(eof)";  }
section(){ echo -e "$(bg 27)$(fill "" '¯')\n${1}\n$(fill "" '_')"; }
