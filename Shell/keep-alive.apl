delay 5
repeat
	set current_app to getCurrentApp()
	tell application "Citrix Viewer"
		activate
		tell application "System Events" to keystroke "1"
	end tell
	tell application current_app
		activate
		tell application "System Events" to keystroke "2"
	end tell
	delay 10
end repeat

to getCurrentApp()
	return (path to frontmost application as text)
end getCurrentApp
