# Words
## Statement
A sequence of words is specified. The game is that players take turns to name words from a given sequence. The rule by which the word is called is as follows: if a word is named, the next player can name the word that begins with the letter to which the previous word ends and which has not yet been named. It is necessary to determine whether it is possible to build a chain of all words during the game, and the last word must end with the letter with which the first word begins.<br> For example, for a sequence of words
<br>
+ team, nut, man

The required string of words has the form

+ team, man, nut

## Time and memory limit
Time limit: 1 s<br>
Memory limit: no
## Input file format
The first line contains the number n of words. The next n lines contain words (one word per line). Words can contain Russian and Latin letters. The size of the input file does not exceed one mebibyte.
## Output file format
If you can compose a string of all the words, then output the message Yes. If the chain can not be made up, then display message No.
## Example
| input         |   output      |
| ------------- | ------------- |
| 3<br>team<br>nut<br>man  |  Yes |
