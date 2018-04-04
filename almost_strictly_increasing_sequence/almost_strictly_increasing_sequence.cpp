#include <fstream>
#include <algorithm>

using namespace std;

int* sourceSequence;
int* ind;
unsigned int n;

bool compare_left_to_right(int j, int Ai)
{
	return sourceSequence[j] < Ai;
}

bool compare_right_to_left(int j, int Ai)
{
	return sourceSequence[j] > Ai;
}


int main()
{
	ifstream fin("input.txt");
	ofstream fout("output.txt");

	fin >> n;
	if (n == 1)
	{
		fout << 1;
		return 0;
	}

	sourceSequence = new int[n + 1];
	sourceSequence[0] = INT_MAX;
	for (unsigned int i = 1; i <= n; i++)
		fin >> sourceSequence[i];

	ind = new int[n + 1];
	for (int i = 1; i <= n; i++)
		ind[i] = 0;

	int* B1 = new int[n + 1];
	int* B2 = new int[n + 1];

	for (int i = 1; i <= n; i++)
	{
		int j = lower_bound(ind + 1, ind + n, sourceSequence[i], compare_left_to_right) - ind;
		ind[j] = i;
		B1[i] = j;
	}

	delete[] ind;
	ind = new int[n + 1];

	for (int i = 1; i <= n; i++)
		ind[i] = 0;

	sourceSequence[0] = INT_MIN;
	for (int i = n; i > 0; i--)
	{
		int j = lower_bound(ind + 1, ind + n, sourceSequence[i], compare_right_to_left) - ind;;
		ind[j] = i;
		B2[i] = j;
	}
	delete[]ind;

	int* B1max = new int[n + 1];
	int* B2max = new int[n + 1];

	B1max[1] = B1[1];
	B2max[n] = B2[n];

	for (int i = 2; i <= n; i++)
		B1max[i] = B1max[i - 1] > B1[i] ? B1max[i - 1] : B1[i];

	for (int i = n - 1; i >= 1; i--)
		B2max[i] = B2max[i + 1] > B2[i] ? B2max[i + 1] : B2[i];

	int max = INT_MIN, sum;
	for (int i = 1; i < n; i++)
	{
		sum = B1max[i] + B2max[i + 1];
		if (sum > max)
			max = sum;
	}
	fout << max;
	return 0;
}