#!/bin/bash
# single line command for execution
# wget -O - <RAW_URL> | bash

## INLINED FROM PRETTY-PRINT.SH ###############################################
to_eol() { printf "${1}$(seq -s "${2}" $((${#1} + 1)) $(tput cols) | tr -d '[:digit:]')"; }
section(){ printf "\e[48;5;27m$(to_eol "" '¯')\n$(to_eol "${1}" ' ')\n$(to_eol "" '_')\e[0m"; echo;}
info()   { echo -e "$(bg 28)$(fg 255)$(fill "${1}" " ")$(eof)";  }
warning(){ echo -e "$(bg 208)$(fg 0)$(fill "${1}" " ")$(eof)";  }
error()  { echo -e "$(bg 196)$(fg 255)$(fill "${1}" " ")$(eof)";  }
detail() { echo -e "$(fg 250)$(fill "${1}" " ")$(eof)";  }

###############################################################################
section "Installing Fish"
brew install fish
brew install lolcat
brew install fortune

# oh my fish framework
curl -L https://get.oh-my.fish | fish
## with Themes
fish "omf install agnoster lambda fish-logo"

#"Cloning Powerline Fonts used for some prompts in OMF"
git clone https://github.com/powerline/fonts.git ~/github/powerline-fonts --depth=1

###############################################################################
section "Configuration"

echo "
## Greeting Function can be moved to /functions/fish_greeting.fish
function fish_greeting
    fortune | lolcat
    fish_logo blue cyan green
end

## Git Aliases
alias gd=\"git diff\"
alias gdc=\"git diff --cached\"
alias ga=\"git add\"
alias gaa=\"git add --all\"
alias gca=\"git commit -a -m\"
alias gcm=\"git commit -m\"
alias gs=\"git status -sb --ignore-submodules\"
alias gph=\"git push\"
alias gpl=\"git pull --ff-only\"
alias gmg=\"git merge --ff-only\"
alias gb=\"git branch\"
alias gcb=\"git checkout -b\"
alias gco=\"git checkout\"
" >>  ~/.config/fish/config.fish

###############################################################################
section "Make fish the default shell"
sudo echo /usr/local/bin/fish >> /etc/shells
chsh -s /usr/local/bin/fish