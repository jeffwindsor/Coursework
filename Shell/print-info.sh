#!/bin/bash

to_eol() { printf "${1}$(seq -s "${2}" $((${#1} + 1)) $(tput cols) | tr -d '[:digit:]')"; }
section(){ printf "\e[48;5;27m$(to_eol "" '¯')\n$(to_eol "${1}" ' ')\n$(to_eol "" '_')\e[0m"; echo;}
info()   { echo -e "$(bg 28)$(fg 255)$(fill "${1}" " ")$(eof)";  }
warning(){ echo -e "$(bg 208)$(fg 0)$(fill "${1}" " ")$(eof)";  }
error()  { echo -e "$(bg 196)$(fg 255)$(fill "${1}" " ")$(eof)";  }
detail() { echo -e "$(fg 250)$(fill "${1}" " ")$(eof)";  }
