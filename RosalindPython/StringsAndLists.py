#!/usr/bin/python

def slice2(s,a,b,c,d):
	sa = s[a:b+1];
	sb = s[c:d+1];
	return sa + " " + sb;

sx = "ngNm9KHtKVPQ0MMjt4gw96rb2vEQdd1CKTKElZFLhPHz5Dbzp5PET8XTK2VYFtRi4gwTjSmBBjwQeLChnppzwsynCharadriusqxSOyRmv4r9kEhA9tTmschneideriI7nomMa8PGPvN7sR9sRt6DUEPZfotS42Qmr9CsZ84p8BvvJ5uqkw62oE6IP989e.";
print slice2(sx,88, 97, 117, 126);