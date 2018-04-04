#include <fstream>
#include <vector>
#include <string>

using namespace std;


string source;
string minimalString;

bool isPeriodic(int l, int r, int interval_count)
{
	int length = r - l + 1;
	if (length % interval_count != 0)
		return false;

	int ptr1 = l;
	int ptr2 = l + length / interval_count;
	while (ptr2 <= r)
		if (source[ptr1++] != source[ptr2++])
			return false;
	return true;
}

int getPeriod(int l, int r)
{
	for (int interval_count = r - l + 1; interval_count >= 2; interval_count--)
	{
		if (isPeriodic(l, r, interval_count))
			return interval_count;
	}
	return -1;
}

int main()
{
	ifstream fin("folding.in");
	ofstream fout("folding.out");

	getline(fin, source);

	int min;
	string mincollapsed;
	string sum;
	int len = source.length();

	string** S = new string*[len];
	for (int i = 0; i < len; i++)
		S[i] = new string[len];
	int** M = new int*[len];
	for (int i = 0; i < len; i++)
		M[i] = new int[len];

	for (int i = 0; i < len; i++)
	{
		S[i][i] = source[i];
		M[i][i] = 1;
	}

	for (int t = 1; t < len; t++)
	{
		for (int i = 0, j = t; i < len - t; i++, j++)
		{
			min = 2147483647;
			int imin1, jmin1, imin2, jmin2;
			for (int k = i; k < j; k++)
			{
				if (M[i][k] + M[k + 1][j] < min)
				{
					min = M[i][k] + M[k + 1][j];
					imin1 = i;
					jmin1 = k;
					imin2 = k + 1;
					jmin2 = j;
				}
			}
			minimalString = S[imin1][jmin1] + S[imin2][jmin2];
			int interval_count = getPeriod(i, j);

			if (interval_count == -1)
			{
				M[i][j] = min;
				S[i][j] = minimalString;
			}
			else
			{
				int patlen = (j - i + 1) / interval_count;
				mincollapsed = to_string(interval_count) + "(" + S[i][i + patlen - 1] + ")";
				int mincollapsedlen = mincollapsed.length();

				if (min < mincollapsedlen)
				{
					M[i][j] = min;
					S[i][j] = minimalString;
				}
				else
				{
					M[i][j] = mincollapsedlen;
					S[i][j] = mincollapsed;
				}
			}

		}
	}
	fout << S[0][len - 1];
	return 0;
}