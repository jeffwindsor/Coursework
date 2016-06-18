# Uses python3
import sys
import random

def partition(a, l, r):
    def swap(i,j):
        a[i], a[j] = a[j], a[i]

    x = a[l]
    lt = i = l
    gt = r
    while (i <= gt):
        if a[i] < x:
            swap(lt, i); lt += 1; i += 1
        elif a[i] > x:
            swap(gt, i); gt -= 1
        else:
            i += 1
        #print(lt," ",i," ",gt," ",a)
    return lt, gt

def randomized_quick_sort(a, l, r):
    if l >= r:
        return a
    j = random.randint(l, r)
    a[l], a[j] = a[j], a[l]

    lt, gt = partition(a, l, r)
    randomized_quick_sort(a, l, lt - 1);
    randomized_quick_sort(a, gt + 1, r);

if __name__ == '__main__':
    input = sys.stdin.read()
    n, *a = list(map(int, input.split()))
    randomized_quick_sort(a, 0, n - 1)
    for x in a:
        print(x, end=' ')
