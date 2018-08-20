#!/bin/bash

###############################################################################
## PERSONAL VARIABLES
user_name="Jeff Windsor"
user_email="jeff.windsor@gmail.com"
vscode_user_settings=$(cat <<EOF
{
    "window.zoomLevel": 0,
    "breadcrumbs.enabled": true,
    "editor.renderWhitespace": "all",
    "editor.renderControlCharacters": true,
    "explorer.confirmDelete": false,
    "files.autoSave": "afterDelay",
    "editor.fontSize": 14,
    "editor.fontFamily": "'Noto Mono for Powerline', 'Courier New', monospace",
    "workbench.colorTheme": "Dark (Molokai)",
    "workbench.iconTheme": "eq-material-theme-icons-ocean",
    "materialTheme.fixIconsRunning": false,
    "gitlens.advanced.messages": {
        "suppressShowKeyBindingsNotice": true
    },
}
EOF
)
###############################################################################
## FUNCTIONS
yes_no(){
    echo -e "$1 [y/N]: "; 
    read -t 5 yes_no_input;
    
    if [[ -z ${yes_no_input} ]]
        then yes_no_input="n"
    fi
}
## PRETTY PRINTER
COLOR_RESET="\e[0m";CHAR_BANNER='#';LINE="\n";
to_eol() { seq -s "${1}" $(($2 + 1)) $(tput cols) | tr -d '[:digit:]'; }
divider(){ to_eol $1 0; }
foreground(){ printf "\e[38;5;%sm" $1;}
background(){ printf "\e[48;5;%sm" $1;}
printc(){ printf "$($1 $2)${3}${COLOR_RESET}"; }
printcln(){ LEN=${#3}; printf "$(printc $1 $2 "${3}$(to_eol ' ' ${#3})")${LINE}"; }
printcln_f(){ printcln "foreground" $1 "${2}"; }
printcln_b(){ printcln "background" $1 "${2}";}

section(){ div=$(divider '¯'); printc "background" 27 "$(divider '¯')${LINE}${1}${LINE}$(divider '_')"; }
info(){ printcln_b 28 "${1}"; }
warning(){ printcln_b 208 "${1}"; }
error(){ printcln_b 196 "${1}"; }
detail(){ printcln_f 244 "${1}"; }

# COLOR CHART IF NEEDED
# for fgbg in 38 48 ; do # Foreground / Background
#     for color in {0..255} ; do # Colors
#         # Display the color
#         printf "\e[${fgbg};5;%sm  %3s  \e[0m" $color $color
#         # Display 6 colors per lines
#         if [ $((($color + 1) % 6)) == 4 ] ; then
#             echo # New line
#         fi
#     done
#     echo # New line
# done

###############################################################################
## OS IDENTIFICATION - NOT SURE THIS IS THE BEST YET
uname=$(uname);
case "$uname" in
    (*Linux*)  
        installPackageCmd='pacman -S '; 
        installVizPackageCmd='pacman -S ';
        ;;
    (*Darwin*) 
        installPackageCmd='brew install '; 
        installVizPackageCmd='brew cask install '; 
        ;;
esac;

###############################################################################
## HOMEBREW : ONLY FOR MAC
###############################################################################
case "$uname" in
    (*Darwin*) 
        section "HomeBrew";
        # https://brew.sh/
        ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)";
        # https://caskroom.github.io/
        brew tap caskroom/cask;
        ;;
esac;

###############################################################################
section "GNU"
###############################################################################
"$installPackageCmd" coreutils wget

###############################################################################
section "Git"
###############################################################################
"$installPackageCmd" git

## Configure Git
git config --global user.name user_name
git config --global user.email user_email
git config --global alias.c commit -m 
git config --global alias.s status
git config --global alias.b branch
git config --global alias.type 'cat-file -t'
git config --global alias.dump 'cat-file -p'
git config --global alias.hist "log --graph --max-count=100 --pretty=format:‘%C(green)%h%C(reset) | %C(yellow)%d%C(reset) %s %C(cyan)%an : %C(dim)%cr%C(reset)' --abbrev-commit"

#https://hub.github.com/
brew install hub

###############################################################################
section "Visual Studio Code"
###############################################################################
"$installVizPackageCmd" visual-studio-code
### VS_THEMES
code --install-extension nonylene.dark-molokai-theme
code --install-extension PKief.material-icon-theme
code --install-extension teabyii.ayu
code --install-extension Equinusocio.vsc-material-theme
### VS_EX : https://github.com/aaron-bond/better-comments
code --install-extension aaron-bond.better-comments
### VS_EX : https://github.com/eamodio/vscode-gitlens
code --install-extension eamodio.gitlens
### VS_EX : https://github.com/DavidAnson/vscode-markdownlint
code --install-extension DavidAnson.vscode-markdownlint


###############################################################################
section "Haskell"
###############################################################################
"$installPackageCmd" haskell-stack
## change haskell prpmpt to something cooler - adjust colors to liking
echo :set prompt "\ESC[44m\STX\ESC[97m\STX λ \ESC[m\STX\ESC[34m \ESC[m\STX" >> .ghci

## VS_EX : https://github.com/JustusAdam/language-haskell
code --install-extension justusadam.language-haskell
## VS_EX : https://gitlab.com/vannnns/haskero, PRE_REQ : https://github.com/commercialhaskell/intero/blob/master/TOOLING.md#installing 
stack build intero
code --install-extension Vans.haskero
## VS_EX : https://github.com/caneroj1/hoogle-vscode
code --install-extension jcanero.hoogle-vscode

###############################################################################
section "Python"
###############################################################################
"$installPackageCmd" python3 pylint
### VS_EXT: https://github.com/Microsoft/vscode-python
code --install-extension ms-python.python

###############################################################################
section "Scala / Java"
###############################################################################
 yes_no "Install Scala and Java"
if [ "$yes_no_input" == "y" ]; 
    then
        "$installVizPackageCmd" java intellij-idea 
        "$installPackageCmd" scala
fi

###############################################################################
section "JavaScript"
###############################################################################
 yes_no "Install Node"
if [ "$yes_no_input" == "y" ]; 
    then
    ## Javascript (optional)
    "$installPackageCmd" node
fi

###############################################################################
section "Cloning Useful Repos to ~/{upstream}"
git clone https://github.com/mbadolato/iTerm2-Color-Schemes.git ~/github/iTerm2-Color-Schemes
## online preview : https://fonts.google.com/
## I like : Fira Mono, Anonymous Pro, UbuntuMono, RobotoMono, SourceCodePro, 
git clone https://github.com/google/fonts.git ~/github/google-fonts
## NICE UNICODE ENABLED FONTS FOR SOME FISH PROMTPS
git clone https://github.com/powerline/fonts.git ~/github/powerline-fonts --depth=1
git clone https://github.com/ryanoasis/nerd-fonts.git ~/github/nerd-fonts

###############################################################################
section "iTerm2"
###############################################################################
"$installVizPackageCmd" iterm2

###############################################################################
section "Fish"
###############################################################################
"$installPackageCmd" fish
# oh my fish framework
curl -L https://get.oh-my.fish | fish
# Make fish the default shell
sudo echo /usr/local/bin/fish >> /etc/shells
chsh -s /usr/local/bin/fish
reset
## Themes
omf install agnoster lambda simple-ass-prompt 
## Plugins
omf install gi gh

###############################################################################
section "POST ITEMS"
###############################################################################
info "OMF: if the below does not show icons, switch to a powerline font in iTerm or your prefered terminal when using 'agnoster' theme"
info "\ue0b0 \u00b1 \ue0a0 \u27a6 \u2718 \u26a1 \u2699"
echo
warning "Put the following into the Visual Studio Code User Settings"
detail $vscode_user_settings
echo

warning "INSTALL KEY ON GITHUB"
 yes_no "Generate RSA Key"
if [ "$yes_no_input" == "y" ]; 
    then
    ## KEY FOR GITHUB/GITLAB
    ssh-keygen -t rsa
fi

info "Copying rsa key to clipboard"
pbcopy < ~/.ssh/id_rsa.pub
info "Goto to https://github.com/settings/profile and paste key"
echo
