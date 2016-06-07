# Uses python3
import sys
from collections import namedtuple

Segment = namedtuple('Segment', 'start end')

def maxSegment():
    return Segment(-1,1000000001)

def optimal_points(segments):
    points = []

    window = maxSegment()
    for s in sorted(segments):
        if(s.start <= window.end):
            window.start = max(window.start, s.start)
            window.end = min(window.end, s.end)
        else:
            points.append(window.end)
            window = maxSegment()

    return points

if __name__ == '__main__':
    input = sys.stdin.read()
    n, *data = map(int, input.split())
    segments = list(map(lambda x: Segment(x[0], x[1]), zip(data[::2], data[1::2])))
    points = optimal_points(segments)
    print(len(points))
    for p in points:
        print(p, end=' ')
