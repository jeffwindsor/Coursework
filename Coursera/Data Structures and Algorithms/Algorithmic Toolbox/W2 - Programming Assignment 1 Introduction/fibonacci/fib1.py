def calc_fib(n):
	def inner(a,b,i,n):
		return a + b if (i == n) else inner(b, a+b, i+1, n)

	return n if (n < 2) else inner(0,1,2,n)

n = int(input())
print(calc_fib(n))
