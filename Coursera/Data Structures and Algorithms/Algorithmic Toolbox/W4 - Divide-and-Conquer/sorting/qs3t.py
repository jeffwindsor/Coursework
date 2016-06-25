# Uses python3
from qs3 import randomized_quick_sort, partition

def test(name, actual, expected):
    print(name, " : ", actual == expected, "[ ", actual, " : " ,expected, " ]")

def qs_test(name, array, expected):
	randomized_quick_sort(array, 0, len(array) - 1)
	test(name, array, expected)


#qs_test("Sample 1", [2,3,9,2,2], [2,2,2,3,9])
#qs_test("Sample 2", [2,3,9,2,2,1], [1,2,2,2,3,9])
#qs_test("Stepped",  [5,3,9,5,5,3,9,3,9], [3,3,3,5,5,5,9,9,9])
#qs_test("Stepped Large",  
#	[500000000,300000000,999999999,500000000,500000000,300000000,900000000,300000000,900000000], 
#	[300000000,300000000,300000000,500000000,500000000,500000000,900000000,900000000,999999999])
qs_test("Princeton Dijkstra Example", ['R','B','W','W','R','W','B','R','R','W','B','R'], ['B','B','B','R','R','R','R','R','W','W','W','W'])