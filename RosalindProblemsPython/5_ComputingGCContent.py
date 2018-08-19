
def gc(dna):
    def isGC(n):
        if n == 'C' or n == 'G':
            return 1.0;
        else:
            return 0.0;
    return (sum(map(isGC,dna)) / len(dna)) * 100.0

#dna = "CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGACTGGGAACCTGCGGGCAGTAGGTGGAAT"
#print gc(dna)

id = ""
p = 0.0
with open("5_ComputingGCContent.txt","r") as f:
    while True:
        line1 = f.readline()
        line2 = f.readline()
        if not line2: break
        pn = gc(line2)
        if pn > p:
            id = line1
            p = pn

print id
print p