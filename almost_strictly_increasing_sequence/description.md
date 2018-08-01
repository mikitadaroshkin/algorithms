# Almost strictly increasing sequence
## Statement 
It is necessary to delete the minimum number of elements from a given numerical sequence A of length n, so that the remaining elements form a strictly increasing subsequence almost everywhere, that is, containing at most one discontinuity-pairs (ai, ai + 1) of consecutive elements, the second of which is not more than the first. The constructed algorithm must have the complexity O (n log n).
## Time and memory limit
Time limit: 1 s<br>
Memory limit: no
## Input file format
The first line contains the number n (1 ≤ n ≤ 100 000). The next line contains n elements of the sequence A, which are separated by a space (the elements of the sequence are positive integers not exceeding 1 000 000 000).
## Output file format
The length of a subsequence of elements that is strictly increasing almost everywhere.
## Example
| input         |   output      |
| ------------- | ------------- |
|   9<br>1 2 12 7 3 8 14 13 9| 6  |
