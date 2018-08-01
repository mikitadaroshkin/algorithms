# Segment tree
## Statement
There is a sequence s0, ..., sn-1 consisting of zeros. The following types of queries can be performed on this sequence:
<br>
1. Set the value of si to v;
2. Add to each element with index from the interval [a, b] the number v.
3. Find the sum of elements with indices from the interval [a, b].
4. Find the minimum among elements with indices from the interval [a, b].
5. Find the maximum among elements with indices from the interval [a, b].
<br>
It is required to write a program that will process the specified requests.
<br>

## Time and memory limit
Time limit: 1 s<br>
Memory limit: no

## Input file format
The first line contains one integer n (1 ≤ n ≤ 100 000) - the length of the sequence. The following lines have the following format: the first number is the type of the request (see condition), then for the query of type 1 two numbers: i, the index of the element in the array, and the value of v; for a query of type 2, three numbers: a, b, v; for queries 3-5, two numbers: a and b. The input ends in a string with a single number of 0. The number of lines does not exceed 100 003. The absolute value of the number v in each request does not exceed 1 000 000. The numbers a, b and i in each query satisfy the inequalities 0 ≤ a ≤ b <n, 0 ≤ i <N.
## Output file format
For each query of type 3-5, output the response to the query.
## Example

| input         |   output      |
| ------------- | ------------- |
| 5<br>2 0 4 7<br>3 2 3<br>1 2 -10  |  14<br>-10<br>16<br> |
