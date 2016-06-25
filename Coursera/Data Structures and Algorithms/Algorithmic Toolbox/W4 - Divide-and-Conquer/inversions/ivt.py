# Uses python3
from Iv import get_number_of_inversions

def test(name, actual, expected):
    print(name, " : ", actual == expected, "[ ", actual, " : " ,expected, " ]")

def m_test(name,initial,expected):
	test(name, merge_sort(initial),expected)

m_test("Princeton 1", 
	['M','E','R','G','E','S','O','R','T','E','X','A','M','P','L','E'], 
	['A','E','E','E','E','G','L','M','M','O','P','R','R','S','T','X'])

