#include <fstream>
#include <stack>
#include <algorithm>
#include <vector>

using namespace std;


class Point
{
public:
	int x;
	int y;
	Point(int _x, int _y) : x(_x), y(_y) {}
	Point() : x(-1), y(-1) {}
};


void init(vector<Point>&v, int XMAX, int YMAX)
{
	v.push_back(Point(0, 0));
	v.push_back(Point(XMAX, 0));
}

bool x_increasing_comparer(Point& a, Point& b)
{
	return a.x < b.x || (a.x == b.x && a.y < b.y);
}

bool y_decreasing_comparer(Point& a, Point& b)
{
	return a.y > b.y || (a.y == b.y && a.x < b.x);
}


int main()
{
	ifstream fin("input.txt");
	ofstream fout("output.txt");

	int N, X_MAX, Y_MAX;
	fin >> N >> X_MAX >> Y_MAX;

	vector<Point> x_inc;
	x_inc.reserve(N);
	vector<Point> y_dec;
	y_dec.reserve(N);

	init(x_inc, X_MAX, Y_MAX);
	init(y_dec, X_MAX, Y_MAX);

	int tmpx, tmpy;
	Point tmp;
	for (int i = 0; i < N; i++)
	{
		fin >> tmpx >> tmpy;
		if (tmpy == 0 || tmpy == Y_MAX || tmpx == 0 || tmpx == X_MAX)
			continue;
		tmp = Point(tmpx, tmpy);
		x_inc.push_back(tmp);
		y_dec.push_back(tmp);
	}

	N = y_dec.size();
	if (N == 2)
	{
		fout << X_MAX*Y_MAX;
		return 0;
	}

	sort(x_inc.begin(), x_inc.end(), x_increasing_comparer);
	sort(y_dec.begin(), y_dec.end(), y_decreasing_comparer);

	int max_area = -1;

	for (int i = 0; i < N; i++)
	{
		stack<Point> result;
		result.push(x_inc[0]);
		int floor = y_dec[i].y;
		int max_delta_x = 0;
		int x_prev = 0;

		for (int j = 1; j < N - 1; j++)
		{
			if (x_inc[j].y <= floor)
				continue;

			int curr_diff = x_inc[j].x - x_prev;
			if (curr_diff > max_delta_x)
				max_delta_x = curr_diff;
			x_prev = x_inc[j].x;

			if (result.top().y < x_inc[j].y)
				result.push(x_inc[j]);
			else
			{
				while (result.top().y > x_inc[j].y)
				{
					Point curr_point = result.top();
					result.pop();
					int tmp_area = (x_inc[j].x - result.top().x)*(curr_point.y - floor);
					if (tmp_area > max_area)
						max_area = tmp_area;
				}
				result.push(x_inc[j]);
			}
		}

		while (!(result.size() == 1))
		{
			Point curr_point = result.top();
			result.pop();
			int tmp_area = (x_inc[N - 1].x - result.top().x)*(curr_point.y - floor);
			if (tmp_area > max_area)
				max_area = tmp_area;
		}

		if ((X_MAX - x_prev)*(Y_MAX - floor) > max_area)
			max_area = (X_MAX - x_prev)*(Y_MAX - floor);
		if (max_delta_x != 0 && max_delta_x*(Y_MAX - floor) > max_area)
			max_area = max_delta_x*(Y_MAX - floor);
	}

	fout << max_area;
	return 0;
}