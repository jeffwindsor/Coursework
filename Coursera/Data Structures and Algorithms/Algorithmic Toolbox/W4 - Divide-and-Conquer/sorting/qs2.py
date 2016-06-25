# Uses python3
import sys
import random

def partition(a, l, r):
    print(l, " : ",r)
    x = a[l]
    j = l;
    for i in range(l + 1, r + 1):
        if a[i] <= x:
            print("swap")
            j += 1
            a[i], a[j] = a[j], a[i]

        print("[",i,"] ","j:",j," ",a)

    a[l], a[j] = a[j], a[l]
    print(a)
    return j

def randomized_quick_sort(a, l, r):
    if l >= r:
        return a
    k = random.randint(l, r)
    a[l], a[k] = a[k], a[l]
    m = partition(a, l, r)
    randomized_quick_sort(a, l, m - 1);
    randomized_quick_sort(a, m + 1, r);

if __name__ == '__main__':
    input = sys.stdin.read()
    n, *a = list(map(int, input.split()))
    randomized_quick_sort(a, 0, n - 1)
    for x in a:
        print(x, end=' ')
