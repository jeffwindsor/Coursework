#!/bin/bash
user_name = "Jeff Windsor"
user_email = "jeff.windsor@gmail.com"


###############################################################################
div = "###############################################################################"
print_header () {
    echo -e "\033[0;31m${div}\n$1$\n${div}\033[0m"
}

###############################################################################
print_header "HomeBrew" 
# https://brew.sh/
ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
# https://caskroom.github.io/
brew tap caskroom/cask

###############################################################################
print_header "Core Utils"
brew install coreutils wget

###############################################################################
print_header "Git"
brew install git
## Configure Git
git config --global user.name user_name
git config --global user.email user_email
# git config --global alias.c commit -m 
# git config --global alias.s status
# git config --global alias.b branch
# git config --global alias.type 'cat-file -t'
# git config --global alias.dump 'cat-file -p'
git config --global alias.hist "log --graph --max-count=100 --pretty=format:‘%C(green)%h%C(reset) | %C(yellow)%d%C(reset) %s %C(cyan)%an : %C(dim)%cr%C(reset)' --abbrev-commit"
## KEY FOR GITHUB/GITLAB
#ssh-keygen -t rsa

###############################################################################
print_header "Development"
brew cask install visual-studio-code 
## Haskell (optional)
brew install haskell-stack
## Python
brew install python3 pylint
## Scala / Java (optional)
#brew cask install java intellij-idea 
#brew install scala
## Javascript (optional)
#brew install node

###############################################################################
print_header "iTerm2"
brew cask install iterm2

###############################################################################
print_header "Cloning Useful Github Repos"
git clone https://github.com/mbadolato/iTerm2-Color-Schemes.git ~/github/iTerm2-Color-Schemes
## online preview : https://fonts.google.com/
## I like : Fira Mono, Anonymous Pro, UbuntuMono, RobotoMono, SourceCodePro, 
git clone https://github.com/google/fonts.git ~/github/google-fonts
## NICE UNICODE ENABLED FONTS FOR SOME FISH PROMTPS
git clone https://github.com/powerline/fonts.git ~/github/powerline-fonts --depth=1
git clone https://github.com/ryanoasis/nerd-fonts.git ~/github/nerd-fonts

###############################################################################
print_header "Fish"
brew install fish
# oh my fish framework
curl -L https://get.oh-my.fish | fish
# Make fish the default shell
sudo echo /usr/local/bin/fish >> /etc/shells
chsh -s /usr/local/bin/fish
#reset
## Themes
omf install agnoster lambda simple-ass-prompt 
echo "OMF: if the below does not show icons, switch to a powerline font when using 'agnoster' theme"
echo "\ue0b0 \u00b1 \ue0a0 \u27a6 \u2718 \u26a1 \u2699"
