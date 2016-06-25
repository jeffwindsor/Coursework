# Uses python3
from qs2 import randomized_quick_sort

def test(name, actual, expected):
    print(name, " : ", actual == expected, "[ ", actual, " : " ,expected, " ]")

#a = [2,3,9,2,2]
#randomized_quick_sort(a, 0, 5 - 1)
#test("Sample 1", a, [2,2,2,3,9])

a = [2,3,9,2,2,1]
randomized_quick_sort(a, 0, 6 - 1)
test("Sample 2", a, [1,2,2,2,3,9])