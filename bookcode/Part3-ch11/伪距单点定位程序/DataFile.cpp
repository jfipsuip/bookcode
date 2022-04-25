#include "stdafx.h"
#include "DataFile.h"
#include "utils.h"

#include <fstream>
#include <cstdio>
#include <stdlib.h>
#include <vector>

using namespace std;

bool DataRead(CString StrPath,vector<CSPP> data)
{
	if(!StrPath)
		return FALSE;

	string approx = "APPROX_POSITION��";
	string sate_num = "Satellite Number:";
	string gps_time = "GPS time��";

	string file_name = CStr_to_str(StrPath);
	ifstream s;
	s.open(file_name,ios::in);

	string line;
	vector<string> str_list;
	size_t idx;
	getline(s,line);

	//APPROX_POSITION
	string_split(line,",",str_list);
				
	tagDATAHEAR1 head1;
	idx = str_list[0].find(approx);		  
	head1.X = stod(str_list[0].substr(approx.size()));
	head1.Y = stod(str_list[1]);
	idx = str_list[2].find("(m)");		  
	head1.Z = stod(str_list[2].substr(0,idx));
		
	//�ڶ�������
	getline(s,line);
		
	bool bInfo = false;
	CSPP info;
	//		
	while(!s.eof())
	{
		getline(s,line);
		if(line.size() == 0)
			continue;

		string_split(line,",",str_list);
		if(str_list[0].substr(0,9) == "Satellite")
		{
			if(bInfo)
			{
				data.push_back(info);
				info = CSPP();
			}

			bInfo = true;
			idx = str_list[0].find(sate_num);
			info.m_SymbolSN = stoi(str_list[0].substr(idx + sate_num.size(),-1));

			idx = str_list[1].find(gps_time);
			info.m_GPSTIME = stoi(str_list[1].substr(idx + gps_time.size(),-1));
		}
		else
		{
			info.m_Symbol.push_back(str_list[0]);

			info.m_X.push_back(stod(str_list[1]));   //��stof(str_list[1]����str_list[1]ת��Ϊ�����͡����ηŵ�info.m_X��ĩ��)
			info.m_Y.push_back(stod(str_list[2]));
			info.m_Z.push_back(stod(str_list[3]));        //����������λ��
			info.m_Clock.push_back(stof(str_list[4]));    //�����Ӹ�����
			info.m_Elevation.push_back(stof(str_list[5]));//���Ǹ߶Ƚ�
			info.m_Cl.push_back(stod(str_list[6]));       //������
			info.m_TtopDelay.push_back(stof(str_list[7]));//�����ӳ�
		}
	}	

	if(bInfo)
		data.push_back(info);

	return true;

}


void DataOut(CListCtrl m_Grid)
{
	CString str; 
    CFile file; 
    file.Open("Position.txt",CFile::modeCreate|CFile::modeWrite); 
    int nRow = m_Grid.GetItemCount(); 
    int nCol=m_Grid.GetHeaderCtrl()->GetItemCount(); 
    for(int i=0;i <nRow;i++) 
    { 
        str.Empty();
        for (int j=0;j<nCol;j++)
        {
            CString tmp;
            tmp= m_Grid.GetItemText(i, j);
            str+=tmp + " ";
        }
        str+="\r\n";
        file.Write(str,str.GetLength()); 
    } 
    file.Close();
}