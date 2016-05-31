# CodeSkulptor runs Python programs in your browser.
# Click the upper left button to run this simple demo.

# CodeSkulptor runs in Chrome 18+, Firefox 11+, and Safari 6+.
# Some features may work in other browsers, but do not expect
# full functionality.  It does NOT run in Internet Explorer.

import simplegui

#global constants
canvas_width = 400
canvas_height = 400
font_size = 20
font_color = "White"
counter_draw_point = (canvas_width / 2, canvas_height / 2)
score_draw_point = (canvas_width - (font_size * 3.8), 0 + (font_size * 1.5))


#global variables
counter = 0
stops = 0
successful_stops = 0

#helper functions
def mins(t):
    return int(t / 600)

def secs(t):
    return int((t % 600) / 10)

def tenths(t):
    return int(t % 10)

def format_trailing(t):
    if t < 10:
        return "0" + str(t)
    else:
        return str(t)
    
def format(t):
    return str(mins(t)) + ":" + format_trailing(secs(t)) + "." + str(tenths(t)) 

def get_score():
    return str(successful_stops) + "/" + str(stops)

#event handlers
def draw(canvas):
    canvas.draw_text(get_score(), score_draw_point, font_size, font_color)
    canvas.draw_text(format(counter), counter_draw_point, font_size, font_color)
    
def tick():
    global counter
    counter += 1 
    
def button_start_click():
    if not timer.is_running():
        timer.start()
    
def button_stop_click():
    if timer.is_running():
        timer.stop()
        #record the stop
        global stops, successful_stops
        stops += 1
        #record success if stopped on the second
        if tenths(counter) == 0:
            successful_stops += 1
    
def button_reset_click():
    #stop timer
    if timer.is_running():
        timer.stop()
    #reset global values
    global counter, stops, successful_stops
    counter = 0
    stops = 0
    successful_stops = 0
    
# elements
frame = simplegui.create_frame("Stopwatch: The Game", canvas_width, canvas_height)
timer = simplegui.create_timer(100, tick)
frame.add_button("Start",button_start_click) 
frame.add_button("Stop",button_stop_click) 
frame.add_button("Reset",button_reset_click) 

# register event handlers
frame.set_draw_handler(draw)

#begin
frame.start()