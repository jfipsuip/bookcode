#ifndef _DATAFILE_H_
#define _DATAFILE_H_

#include <string>
#include <vector>
#include "utils.h"

#pragma once


typedef struct tagDATAHEAR1  //�۲����ݵ�һ��
{
	unsigned char SatPrn;
	long double X;
	long double Y;
	long double Z;
	tagDATAHEAR1()
	{
		X = 0.0;
		Y = 0.0;
		Z = 0.0;
	}
} tagDATAHEAR1;

typedef struct tagDATAHEAR2  //�۲����ݵڶ���
{
	unsigned char SatPrns;
	unsigned char SatS;
	unsigned char SatX;
	unsigned char SatY;
	unsigned char SatZ;
	unsigned char SatCLOCK;
	unsigned char SatELEVATION;
	unsigned char SatC1;
	unsigned char SatTrop;

} tagDATAHEAR2;

class CSPP
{
public:
	int m_SymbolSN;   //Satellite Number
	int m_GPSTIME;    //GPStime
	vector<string> m_Symbol;    //C10-C32
	vector<double> m_X, m_Y, m_Z;//����������λ��
	vector<float> m_Clock;//�����Ӹ�����
	vector<float> m_Elevation;//���Ǹ߶Ƚ�
	vector<double> m_Cl;//������
	vector<float> m_TtopDelay;//�����ӳ�
	
};

bool DataRead(CString strpath,vector<CSPP> data);

void DataOut(CListCtrl m_Grid);

#endif