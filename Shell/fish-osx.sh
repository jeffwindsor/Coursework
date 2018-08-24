#!/bin/bash
# single line command for execution
# wget -O - <RAW_URL> | bash

###############################################################################
to_eol() { printf "${1}$(seq -s "${2}" $((${#1} + 1)) $(tput cols) | tr -d '[:digit:]')"; }
section(){ printf "\e[48;5;27m$(to_eol "" '¯')\n$(to_eol "${1}" ' ')\n$(to_eol "" '_')\e[0m"; echo;}

###############################################################################
section "Installing Fish"
brew install fish

# oh my fish framework
curl -L https://get.oh-my.fish | fish
## with Themes
fish "omf install agnoster lambda fish-logo"

#"Cloning Powerline Fonts used for some prompts in OMF"
git clone https://github.com/powerline/fonts.git ~/github/powerline-fonts --depth=1

###############################################################################
section "Make fish the default shell"
sudo echo /usr/local/bin/fish >> /etc/shells
chsh -s /usr/local/bin/fish

###############################################################################
section "Configuration"
echo "function fish_greeting
    fish_logo blue cyan green
end" > ~/.config/fish/functions/fish_greeting.fish

echo "alias gd=\"git diff\"
alias ga=\"git add\"
alias gaa=\"git add --all\"
alias gca=\"git commit -a -m\"
alias gcm=\"git commit -m\"
alias gs=\"git status -sb --ignore-submodules\"
alias gph=\"git push\"
alias gpl=\"git pull\"
alias gb=\"git branch\"
alias gcb=\"git checkout -b\"
alias gco=\"git checkout\"" >> ~/.config/fish/config.fish

