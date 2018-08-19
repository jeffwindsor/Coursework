

def rabbit(k,n):
    if n < 2:
        return n
    return rabbit(k, n-1) + (rabbit(k, n-2) * k)

print rabbit(2, 31);