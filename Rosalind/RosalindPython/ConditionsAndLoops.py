
def sumInclusive(a,b):
    odds =  [x for x in range(a,b) if x%2<>0];
    return sum(odds);

print sumInclusive(4517, 8646);