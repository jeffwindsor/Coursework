# Rock-paper-scissors-lizard-Spock template

# The key idea of this program is to equate the strings
# "rock", "paper", "scissors", "lizard", "Spock" to numbers
# as follows:
#
# 0 - rock
# 1 - Spock
# 2 - paper
# 3 - lizard
# 4 - scissors
# helper functions
import random

NUMBER_OF_CHOICES = 5

def number_to_name(number):
    if number == 0:
        return "rock"
    elif number == 1:
        return "Spock"
    elif number == 2:
        return "paper"
    elif number == 3:
        return "lizard"
    elif number == 4:
        return "scissors"
    else:
        print number, "is not a valid choice."    	
    
def name_to_number(name):
    if name == "rock":
        return 0
    elif name == "Spock":
        return 1
    elif name == "paper":
        return 2
    elif name == "lizard":
        return 3
    elif name == "scissors":
        return 4
    else:  
        return -1


def rpsls(name): 
    # convert name to player_number using name_to_number
    player_number = name_to_number(name)
    
    # compute random guess for comp_number using random.randrange()
    computer_number = random.randrange(0,NUMBER_OF_CHOICES)

    # convert comp_number to name using number_to_name
    computer_name = number_to_name(computer_number)   

    # compute difference of player_number and comp_number modulo five
    score = (player_number - computer_number) % NUMBER_OF_CHOICES
    print "Player chooses", name
    print "Computer chooses", computer_name

    # use if/elif/else to determine winner and print results
    if player_number == -1:
        print "Redo, Player's choice of", name, "is not a valid!"
    else:
        if score == 0:
            print "Player and computer tie!"
        elif score > 2:
            #Computer Wins on score of 3 or 4
            print "Computer wins!"
        else:
            #Player Wins on score of 1 or 2
            print "Player wins!"
    
    #Space between games  
    print ""
    
# test your code
rpsls("rock")
rpsls("Spock")
rpsls("paper")
rpsls("lizard")
rpsls("scissors")
rpsls("Kirk")

# always remember to check your completed program against the grading rubric


