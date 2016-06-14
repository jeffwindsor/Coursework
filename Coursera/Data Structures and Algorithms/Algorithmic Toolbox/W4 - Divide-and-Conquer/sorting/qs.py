# Uses python3
import sys
import random

def partition3(a, l, r):
    x = a[l]
    j = k = l;
    for i in range(l + 1, r + 1):
        if a[i] < x:
            j += 1

            a[i], a[j] = a[j], a[i]
        elif a[i] = x


    a[l], a[j] = a[j], a[l]
    return j, k

def randomized_quick_sort(array, l, r):
    if l >= r:
        return array
    pivot = random.randint(l, r)
    array[l], array[pivot] = array[pivot], array[l]
    
    j, k = partition3(array, l, r)
    randomized_quick_sort(array, l, j - 1);
    randomized_quick_sort(array, k + 1, r);

if __name__ == '__main__':
    input = sys.stdin.read()
    n, *a = list(map(int, input.split()))
    randomized_quick_sort(a, 0, n - 1)
    for x in a:
        print(x, end=' ')

#def partition2(a, l, r):
#    x = a[l]
#    j = l;
#    for i in range(l + 1, r + 1):
#        if a[i] <= x:
#            j += 1
#            a[i], a[j] = a[j], a[i]
#    a[l], a[j] = a[j], a[l]
#    return j


#def randomized_quick_sort(a, l, r):
#    if l >= r:
#        return a
#    k = random.randint(l, r)
#    a[l], a[k] = a[k], a[l]
#    #use partition3
#   m = partition2(a, l, r)
#    randomized_quick_sort(a, l, m - 1);
#    randomized_quick_sort(a, m + 1, r);
