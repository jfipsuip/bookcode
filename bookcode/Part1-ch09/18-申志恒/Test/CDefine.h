#ifndef CDEFINE_H
#define CDEFINE_H
#include <vector>
#include <string>
#include <fstream>
#include <sstream>
#include <iostream>

using namespace std;

const double a=6378137.0;
const double f=1.0/298.257223563;
const double b=a-f*a;
const double e2=2*f-f*f;
const double e2_=(a*a-b*b)/(b*b);

class CVect3
{
public:
	double a,b,c;
	// a b c ��˳��ֱ���� X Y Z �� N E U �� B L H 
	CVect3(){}
	CVect3(const double& x,const double& y,const double& z){a=x;b=y;c=z;}

	friend CVect3 operator-(const CVect3& v1,const CVect3& v2);
};

struct Point
{
	string name;
	CVect3 Pos;
};


class CFile
{
public:
	string filename; // �ļ���
	int count;   // ��ĸ���
	vector<Point> Vec_XYZ;

	CFile(){}
	CFile(const string& filename);


};





#endif