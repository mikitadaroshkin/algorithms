# String roll
## Statement
Bill tries to compactly represent the sequence of capital letters of the alphabet from A to Z, folding the repeating subsequences inside it. For example, one way to represent the sequence AAAAAAAAAABABABCCD is 10 (A) 2 (BA) B2 (C) D. It formally defines the collapsed sequence and the unfold operation as follows:

* A sequence containing a single character from A to Z is considered to be a compressed sequence. Expanding this sequence produces the same single character.
* If S and Q are convoluted sequences, then SQ is also a convoluted sequence. If S unfolds in S ', and Q unfolds in Q', then SQ unfolds in S'Q '.
* If S is a convoluted sequence, then X (S) is also a convoluted sequence, where X is a decimal representation of an integer greater than 1. If S unfolds in S ', then X (S) unfolds in S', repeating X times.

<br>Based on this definition, it is easy to deploy this collapsed sequence. However, Bill is much more interested in the reverse transformation. He wants to collapse this sequence so that the resulting collapsed sequence contains the smallest possible number of characters.

## Time and memory limit
Time limit: 1 s<br>
Memory limit: no
## Input file format
The input is a single string of characters from A to Z, containing from 1 to 500 characters.
## Output file format
Output the shortest possible collapsed sequence that unfolds into the sequence given in the input file. If there are several such sequences, output any of them.
## Example
| input         |   output      |
| ------------- | ------------- |
| NEERCYESYESYESNEERCYESYESYES  | 2(NEERC3(YES))  |
