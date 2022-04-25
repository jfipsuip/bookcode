#ifndef CMAP_FRAME_INFO_WTH
#define CMAP_FRAME_INFO_WTH

#if _MSC_VER > 1000
#pragma once
#endif

#include "math.h"

 
class CMapFrameInfo
{
private:
public:
	double dL,dB;
	double mL1,mB1;                   //    4---------3
	double mL2,mB2;                   //    |         |
	double mL3,mB3;                   //    |         |
	double mL4,mB4;                   //    1---------2
	int MapScale;
	CString MapCode;
	CString NewMapCode;


	//���ֱ�����ͼ�����γ���λΪ��
	double dL100;
	double dB100;
	double dL50;
	double dB50;
	double dL25;
	double dB25;
	double dL10;
	double dB10;
	double dL5;
	double dB5;
	double dL2d5;
	double dB2d5;
	double dL1;
	double dB1;


	double DMS_DEG(double T0);
	double DEG_DMS(double T0);

	void MapCode2LB(CString MapCode,int MapCodeScale);
	void NeweMapCode2LB(CString NewMapCode,int MapCodeScale);

	CString LB2MapCode(double L, double B, int MapCodeScale=5 , BOOL isNewMapCode=FALSE,BOOL isFirstLetter=TRUE);

	CMapFrameInfo(CString MapCode,int MapScale=5);
	CMapFrameInfo();

	void Number2dLdB(int xh,int dn,double dL,double dB);
 	void MsgBox(char* msg,char* title="ϵͳ��ʾ",UINT uType=MB_OK|MB_ICONINFORMATION,HWND hWnd=NULL);
	};
	

CMapFrameInfo::CMapFrameInfo()
{
	CMapFrameInfo::MapCode="";
	CMapFrameInfo::MapScale=-1;
	dL100=6.0;
	dB100=4.0;
	dL50=3.0;
	dB50=2.0;
	dL25=1.5;
	dB25=1.0;
	dL10=0.5;
	dB10=1./3.0;
	dL5=0.25;
	dB5=1./6.0;
	dL2d5=0.125;
	dB2d5=1./12.0;
	dL1=1./16.0;
	dB1=1./24.0;
}

CMapFrameInfo::CMapFrameInfo(CString MapCode,int MapScale)
{
	CMapFrameInfo::MapCode=MapCode;
	CMapFrameInfo::MapScale=MapScale;

	//���ֱ�����ͼ�����γ���λΪ��
	dL100=6.0;
	dB100=4.0;
	dL50=3.0;
	dB50=2.0;
	dL25=1.5;
	dB25=1.0;
	dL10=0.5;
	dB10=1./3.0;
	dL5=0.25;
	dB5=1./6.0;
	dL2d5=0.125;
	dB2d5=1./12.0;
	dL1=1./16.0;
	dB1=1./24.0;
}

/***************************************************************************/
// ����dn*dn�����а������ҡ����ϵ������е�˳���Ϊxh�Ŀ����½ǵ������
// �����������½ǵľ��γ��
// 
// xhΪ˳��ţ�dnΪ���е���������dL��dBΪÿ�С�ÿ�е������
// 
// ����������������½ǵľ��γ����������๫������dL��dB��
/***************************************************************************/
void CMapFrameInfo::Number2dLdB(int xh,int dn,double dL,double dB)
{
	int Th,Tl;

	Th=dn-(xh-1)/dn-1;
	Tl=xh%dn; 
	
	if(Tl==0)
	{
		Tl=dn; 
	}
	Tl=Tl-1;

    CMapFrameInfo::dL=dL*Tl;
	CMapFrameInfo::dB=dB*Th;
}

/***************************************************************************/
//���ݴ�ͳͼ����ͼ���㾭γ��,ͼ��Ҫ��8λ������8λ�����Զ��ں���
//��������Ӧͼ������γ������๫������dL,dB��
//ͼ���ǵ㾭γ�ȱ������๫��������
/***************************************************************************/
void CMapFrameInfo::MapCode2LB(CString MapCode,int MapCodeScale)
{
	int   TH100,TL100;
	double L100 ,B100;

	int    T50;
	double L50 ,B50;

	int    T25;
	double L25 ,B25;

	int    T10;
	double L10 ,B10;

	int    T5;
	double L5 ,B5;

	int    T2d5;
	double L2d5 ,B2d5;

	int    T1;
	double L1 ,B1;

    CString CMapCode="00000000";
	
	//ͼ�Ų����λ�Ĳ�0
	MapCode.MakeUpper();
	if(MapCode.GetLength()<8)
	{
		MapCode=MapCode+CMapCode.Right(8-MapCode.GetLength());
	}
	
	//�� 075013 ���͵�ͼ��תΪ G5013000
	if(!IsCharAlpha(MapCode[0]))
	{
		CMapCode.SetAt(0,atoi(MapCode.Left(2))+'A'-1);
		for(int i=2;i<MapCode.GetLength();i++)
			CMapCode.SetAt(i-1,MapCode[i]);
	}
	else
	{
		CMapCode=MapCode;
	}
	

	//��ȡ100���кš��кţ�����100��ͼ�����½Ǿ�γ��
	TH100=CMapCode[0]-'A'+1;
	TL100=atoi(CMapCode.Mid(1,2));
	L100=6*(TL100-31);
	B100=4*(TH100-1);
	
	//��ȡ50���кš��кţ�����50��ͼ�����½Ǿ�γ��
	T50=atoi(CMapCode.Mid(3,1));
	Number2dLdB(T50,2,dL50,dB50);
	L50=L100+dL;
	B50=B100+dB;

	//��ȡ25���кš��кţ�����50��ͼ�����½Ǿ�γ��
	T25=atoi(CMapCode.Mid(3,2));
	Number2dLdB(T25,4,dL25,dB25);
	L25=L100+dL;
	B25=B100+dB;
	
	//��ȡ10���кš��кţ�����50��ͼ�����½Ǿ�γ��
	T10=atoi(CMapCode.Mid(3,3));
	Number2dLdB(T10,12,dL10,dB10);
	L10=L100+dL;
	B10=B100+dB;

	//��ȡ5���кš��кţ�����50��ͼ�����½Ǿ�γ��
	T5=atoi(CMapCode.Mid(6,1));
	Number2dLdB(T5,2,dL5,dB5);
	L5=L10+dL;
	B5=B10+dB;

	//��ȡ2.5���кš��кţ�����50��ͼ�����½Ǿ�γ��
	T2d5=atoi(CMapCode.Mid(7,1));
	Number2dLdB(T2d5,2,dL2d5,dB2d5);
	L2d5=L5+dL;
	B2d5=B5+dB;

	//��ȡ1���кš��кţ�����50��ͼ�����½Ǿ�γ��
	T1=atoi(CMapCode.Mid(6,2));
	Number2dLdB(T1,8,dL1,dB1);
	L1=L10+dL;
	B1=B10+dB;


	//����ָ�������������๫������ֵ(ͼ�����½Ǿ�γ�ȼ���Ӧͼ������γ��)
	switch(MapCodeScale)
	{
		case 100:
			mL1=L100;mB1=B100;
			dL=dL100;dB=dB100;
			break;
		case 50:
			mL1=L50;mB1=B50;
			dL=dL50;dB=dB50;
			break;
		case 25:
			mL1=L25;mB1=B25;
			dL=dL25;dB=dB25;
			break;
		case 10:
			mL1=L10;mB1=B10;
			dL=dL10;dB=dB10;
			break;
		case 5:
			mL1=L5;mB1=B5;
			dL=dL5;dB=dB5;
			break;
		case 25000:
			mL1=L2d5;mB1=B2d5;
			dL=dL2d5;dB=dB2d5;
			break;
		case 1:
			mL1=L1;mB1=B1;
			dL=dL1;dB=dB1;
			break;
	}
	mL2=mL1 + dL;
	mB2=mB1;
	mL3=mL1 + dL;
	mB3=mB1 + dB;
	mL4=mL1;
	mB4=mB1 + dB;
}



/***************************************************************************/
//������ͼ�������ͼ���㾭γ��,ͼ��Ҫ��Ϊ:I49E023012��ʽ
//��������Ӧͼ������γ������๫������dL,dB��
//ͼ�����½Ǿ�γ�������õ��õĲ���L��B����
/***************************************************************************/
void CMapFrameInfo::NeweMapCode2LB(CString NewMapCode,int MapCodeScale)
{
	int   TH100,TL100;
	int   TH,TL;
	double L100 ,B100;
	char ScaleCode;

	NewMapCode.MakeUpper();//��ͼ���е���ĸתΪ��д
	
	//��ȡ100���кš��кţ�����100��ͼ�����½Ǿ�γ��
	TH100=NewMapCode[0]-'A'+1;
	TL100=atoi(NewMapCode.Mid(1,2));
	L100=6*(TL100-31);
	B100=4*(TH100-1);

	ScaleCode=NewMapCode.GetBuffer(10)[3];
	NewMapCode.ReleaseBuffer();

	TH=atoi(NewMapCode.Mid(4,3));
	TL=atoi(NewMapCode.Mid(7,3));
	
	switch(ScaleCode)
	{
		case 'B'://50��
			dL=dL50;
			dB=dB50;
			mL1=L100+dL50*(TL-1);
			mB1=B100+dB50*(2-TH);
			break;
		case 'C'://25��
			dL=dL25;
			dB=dB25;
			mL1=L100+dL25*(TL-1);
			mB1=B100+dB25*(4-TH);
			break;
		case 'D'://10��
			dL=dL10;
			dB=dB10;
			mL1=L100+dL10*(TL-1);
			mB1=B100+dB10*(12-TH);
			break;
		case 'E'://5��
			dL=dL5;
			dB=dB5;
			mL1=L100+dL5*(TL-1);
			mB1=B100+dB5*(24-TH);
			break;
		case 'F'://2.5��
			dL=dL2d5;
			dB=dB2d5;
			mL1=L100+dL2d5*(TL-1);
			mB1=B100+dB2d5*(48-TH);
			break;
		case 'G'://1��
			dL=dL1;
			dB=dB1;
			mL1=L100+dL1*(TL-1);
			mB1=B100+dB1*(96-TH);
			break;
	}
	mL2=mL1 + dL;
	mB2=mB1;
	mL3=mL1 + dL;
	mB3=mB1 + dB;
	mL4=mL1;
	mB4=mB1 + dB;
}

CString CMapFrameInfo::LB2MapCode(double L, double B, int MapCodeScale , BOOL isNewMapCode,BOOL isFirstLetter)
{
	int N,Row,Col;
	CString MapCode100,MapCode50,MapCode25,MapCode10;
	CString MapCode5,MapCode2d5,MapCode1,cstrTemp;
	double dL,dB;

	//����1:100��ͼ��ͼ��
	N=int(B/4)+1;
	if(isFirstLetter)//1:100��I49����
	{
		char a_char='A' + N - 1;
		MapCode100=a_char;
	}
	else//1��100��0949����
	{
		MapCode100.Format(_T("%.2d"),N);
	}
	N=int(L/6)+31;
	cstrTemp.Format(_T("%.2d"),N);
	MapCode100=MapCode100+cstrTemp;
	NewMapCode=MapCode100;

	//���������γ����1:100��ͼ���еľ���γ��
	dB=B-int(B/4)*4.0;
	dL=L-int(L/6)*6.0;

	//����1:50��ͼ��ͼ��
	Row=2-int(dB/2.0);
	Col=int(dL/3.0)+1;
	N=(Row-1)*2+Col;
	cstrTemp.Format(_T("%d"),N);  
	MapCode50=MapCode100+cstrTemp;

	//����1:25��ͼ��ͼ��
	Row=4-int(B-int(B/4)*4);
	Col=int((L-int(L/6)*6)/1.5)+1;
	N=(Row-1)*4+Col;
	cstrTemp.Format(_T("%.2d"),N); 
	MapCode25=MapCode100+cstrTemp;


	
	//����1:10��ͼ��ͼ��
	Row=12-int(dB*3);
	Col=int(dL*2)+1;
	N=(Row-1)*12+Col;
	cstrTemp.Format(_T("%.3d"),N);
	MapCode10=MapCode100+cstrTemp;

	//1:1��ͼ��ͼ��,��10������Ϸֵģ������Ƶ�2.5��֮��
	{
	Row=8-int((dB-int(dB*3)/3.0)*24);
	Col=int((dL-int(dL*2)/2.0)*16)+1;
	N=(Row-1)*8+Col;
	cstrTemp.Format(_T("%.2d"),N);
	MapCode1=MapCode10+cstrTemp;
	}
	
	//���������γ����1:10��ͼ���еľ���γ��
	dB=dB-int(dB*3)/3.0;
	dL=dL-int(dL*2)/2.0;

	//1:5��ͼ��ͼ��
	if(dB>1.0/6. && dL<1.0/4.)      MapCode5=MapCode10+"1";
	else if(dB>1.0/6. && dL>1.0/4.) MapCode5=MapCode10+"2";
	else if(dB<1.0/6. && dL<1.0/4.) MapCode5=MapCode10+"3";
	else if(dB<1.0/6. && dL>1.0/4.) MapCode5=MapCode10+"4";

	//1:2.5��ͼ��ͼ��
	if (dB>1.0/6.)dB=dB-1.0/6.;
	if (dL>1.0/4.)dL=dL-1.0/4.;
	if (dB>1.0/12 && dL<1.0/8.)       MapCode2d5=MapCode5+"1";
	else if (dB>1.0/12. && dL>1.0/8.) MapCode2d5=MapCode5+"2";
	else if (dB<1.0/12. && dL<1.0/8.) MapCode2d5=MapCode5+"3";
	else if (dB<1.0/12. && dL>1.0/8.) MapCode2d5=MapCode5+"4";


	//���¼��������γ����1:100��ͼ���еľ���γ��
	dB=B-int(B/4)*4.0;
	dL=L-int(L/6)*6.0;

	switch(MapCodeScale)
	{
		case 100:
			MapCode=MapCode100;
			NewMapCode=MapCode;
			break;
		case 50:
			MapCode=MapCode50;

			Row=2-int(dB/dB50);
			Col=int(dL/dL50)+1;
			cstrTemp.Format("%03d%03d",Row,Col);
			NewMapCode=MapCode100+"B"+cstrTemp;
			break;
		case 25:
			MapCode=MapCode25;

			Row=4-int(dB/dB25);
			Col=int(dL/dL25)+1;
			cstrTemp.Format("%03d%03d",Row,Col);
			NewMapCode=MapCode100+"C"+cstrTemp;
			break;
		case 10:
			MapCode=MapCode10;

			Row=12-int(dB/dB10);
			Col=int(dL/dL10)+1;
			cstrTemp.Format("%03d%03d",Row,Col);
			NewMapCode=MapCode100+"D"+cstrTemp;
			break;
		case 5:
			MapCode=MapCode5;

			Row=int(dB/dB5);
			Row=24-int(dB/dB5);
			Col=int(dL/dL5)+1;
			cstrTemp.Format("%03d%03d",Row,Col);
			NewMapCode=MapCode100+"E"+cstrTemp;
			break;
		case 25000:
			MapCode=MapCode2d5;

			Row=48-int(dB/dB2d5);
			Col=int(dL/dL2d5)+1;
			cstrTemp.Format("%03d%03d",Row,Col);
			NewMapCode=MapCode100+"F"+cstrTemp;
			break;
		case 1:
			MapCode=MapCode1;

			Row=96-int(dB/dB1);
			Col=int(dL/dL1)+1;
			cstrTemp.Format("%03d%03d",Row,Col);
			NewMapCode=MapCode100+"G"+cstrTemp;
			break;
		default:
			MsgBox("������ѡ�����");
			MapCode="";
			NewMapCode="";

	}

	
	if(isNewMapCode)
		return NewMapCode;
	else
		return MapCode;
}



/***************************************************************************/
//�Ƕȵ�λת������ת��Ϊ�ȷ���    
/***************************************************************************/
double CMapFrameInfo::DEG_DMS(double T0)
{
	  
	//�ӽ�0�ĽǶȣ�ֱ�ӷ���0.000000
	if(T0<1e-8)
	{
		return 1e-10;
	}

	double T,dAngleDms;
	int X0,X1,sign;
	double X2;
	
	T=fabs(T0);
	sign=int(T/T0);//ȡ�Ƕȷ���

	X0=(int)T;
	X1=(int)(60*(T-X0));
	X2=3600*(T-X0-X1/60.);
	
	if(X2 < 1e-10)
	{
		dAngleDms=((X0+X1/100.)*sign)+0.0000000002;
	}
	else if(X2 > 59.9999999999)
	{
		X2=0.0;
		X1++;
		
		if(X1==60)
		{
			X1=0;
			X0++;
		}
		dAngleDms=((X0+X1/100.+X2/10000.)*sign);
	}
	else
	{
		dAngleDms=((X0+X1/100.+X2/10000.)*sign);
	}
	
	return dAngleDms;
}


/***************************************************************************/
//�Ƕȵ�λת�����ȷ���ת��Ϊ��
/***************************************************************************/
double CMapFrameInfo::DMS_DEG(double T0)
{
	  double T;
	  int X0,X1;
	  double X2;

	  T=fabs(T0);
	  T=(T*100000000. + 2.)/100000000.;
	  X0=(int)T;
	  X1=(int)((T-X0)*100);
	  X2=((T-X0)*100-X1)*10000;
	  return((X0+X1/60.+X2/360000.)*fabs(T0)/T0);
}



void CMapFrameInfo::MsgBox(char* msg,char* title,UINT uType,HWND hWnd)
{
    if(hWnd==NULL)
  	   hWnd=GetActiveWindow();
	MessageBox(hWnd,msg,title,uType);
}

#endif
