# template for "Guess the number" mini-project
# input will come from buttons and an input field
# all output for the game will be printed in the console
import simplegui, random, math

# initialize global variables used in your code
low = 0
high = 0
guess_target = 0
remaining_guesses = 0

def display_guess_result(guess, result):
    print "Your guess of %s %s" %(guess, result)

def evaluate_remaining_guesses():    
    if (remaining_guesses < 1):
        print "You have run out of guesses"
        print "The correct answer was %i, the computer wins.\n" %(guess_target)
        start_new_game()        
    else:
        print "You have %i guesses remaining\n" %(remaining_guesses)
        
def set_target_boundries(new_low, new_high):
    global low, high
    low = new_low
    high = new_high

def start_new_game():
    global guess_target, remaining_guesses
    
    #computer to guess a random int, low <= guess_target < high
    guess_target = random.randrange(low, high)
    
    # number of guesses equals the smallest integer value above the 
    # log of (high - low + 1) 
    remaining_guesses = math.ceil(math.log((high - low + 1), 2))
    
    print "** Starting new game **"
    print "Guess a number between", low, "and", high
    print "Debug: Target", guess_target
    evaluate_remaining_guesses()

# define event handlers for control panel    
def range100():
    # button that changes range to range [0,100) and restarts
    set_target_boundries(0,100)
    start_new_game()

def range1000():
    # button that changes range to range [0,1000) and restarts
    set_target_boundries(0,1000)
    start_new_game()
    
def get_input(guess):
    # main game logic goes here	
    global remaining_guesses
    if guess.isdigit():
        guess = int(guess)    
        if guess == guess_target:
            display_guess_result(guess, "is correct, you win!\n")
            start_new_game()
            
        else:
            display_guess_result(guess, ("is to low" if (guess < guess_target) 
                                         else "is to high"))
            #decrement remaining_guesses
            remaining_guesses -= 1
            evaluate_remaining_guesses()
            
    else:
        display_guess_result(guess, "is not a valid integer, try again.")
    
# set initial game to range of 0 to 100
range100()

# create frame
frame = simplegui.create_frame("Guess the Number", 150, 200)

# register event handlers for control elements
input_guess = frame.add_input("Guess", get_input, 50)
button_restart_100 = frame.add_button("Range: 0 - 100",range100)
button_restart_1000 = frame.add_button("Range: 0 - 1000",range1000)

# start frame
frame.start()

# always remember to check your completed program against the grading rubric
