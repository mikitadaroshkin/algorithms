# Binary search tree semipath
## Statement
Find the largest semipath between the vertices with different number of descendants with the minimum sum of the keys of the extreme vertices (the length of the desired semipath must be greater than 0). If there are several such semipaths, then choose one from them, for which the root node has a minimum key value. Delete (right-clicked), if exists, the middle-most vertex of this semipath.
<br><br>
In the case of ambiguous selection of the removed vertex (for example, several semipaths maximum lengths between vertices with different number of descendants and with a minimum sum of keys, the extreme vertices have the same root, but the average vertices of these semipaths do not coincide). You do not need to delete anything from the tree.
<br><br>
If there is no subtree at the vertex, then the number of vertices of this subtree is set to 0.
## Time and memory limit
Time limit: 1 s<br>
Memory limit: no
## Input file format
The input file contains a sequence of numbers - the vertex keys in the order of adding to the tree.
## Output file format
The output file must contain a sequence of vertex keys, obtained by a direct left traversal the resulting tree.
## Example
| input         |   output      |
| ------------- | ------------- |
| 0 40 50 60 70 80 90 2 1  |  0 40 2 1 60 70 80 90 |
