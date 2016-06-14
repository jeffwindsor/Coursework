
def readwrite():
    with open('WorkingWithFiles.txt','r') as f:
        for evenline in list(f)[1::2]:
            print evenline;

readwrite();