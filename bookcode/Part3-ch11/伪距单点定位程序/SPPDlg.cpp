
// SPPDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "SPP.h"
#include "SPPDlg.h"
#include "afxdialogex.h"
#include "Resource.h"
#include "Matrix.h"
#include "DataFile.h"
#include "CalCulate.h"
#include "utils.h"

#include <fstream>
#include <cstdio>
#include <stdlib.h>
#include <math.h>
#include <vector>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// �Ի�������
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

// ʵ��
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CSPPDlg �Ի���

BEGIN_DHTML_EVENT_MAP(CSPPDlg)
	DHTML_EVENT_ONCLICK(_T("ButtonOK"), OnButtonOK)
	DHTML_EVENT_ONCLICK(_T("ButtonCancel"), OnButtonCancel)
END_DHTML_EVENT_MAP()



CSPPDlg::CSPPDlg(CWnd* pParent /*=NULL*/)
	: CDHtmlDialog(CSPPDlg::IDD, CSPPDlg::IDH, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSPPDlg::DoDataExchange(CDataExchange* pDX)
{
	CDHtmlDialog::DoDataExchange(pDX);

	DDX_Control(pDX, IDC_EDIT_path,m_path);
	DDX_Control(pDX, IDC_resultlist,m_Grid);

}

BEGIN_MESSAGE_MAP(CSPPDlg, CDHtmlDialog)
	ON_WM_SYSCOMMAND()
	ON_COMMAND(ID_FileOpen, &CSPPDlg::OnFileopen)
	ON_COMMAND(ID_SAVE, &CSPPDlg::OnSave)
	ON_COMMAND(ID_ANOTHER, &CSPPDlg::OnAnother)
	ON_COMMAND(ID_CLOSE, &CSPPDlg::OnClose)
	ON_COMMAND(ID_CALCULATE, &CSPPDlg::OnCalculate)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CSPPDlg ��Ϣ�������

BOOL CSPPDlg::OnInitDialog()
{
	CDHtmlDialog::OnInitDialog();

	// ��������...���˵�����ӵ�ϵͳ�˵��С�

	// IDM_ABOUTBOX ������ϵͳ���Χ�ڡ�
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������

	/*CRect rect;
	GetClientRect(&rect);
	old.x = rect.right - rect.left;
	old.y = rect.bottom - rect.top;*/

	CRect rect;
	GetWindowRect(&rect);
	m_listRect.AddTail(rect);//�Ի��������

	CWnd* pWnd = GetWindow(GW_CHILD);//��ȡ�Ӵ���
	while(pWnd)
	{
		pWnd->GetWindowRect(rect);//�Ӵ��������
		m_listRect.AddTail(rect); //CList<CRect,CRect> m_listRect��Ա����
		pWnd = pWnd->GetNextWindow();//ȡ��һ���Ӵ���
	}

	m_Grid.SetExtendedStyle(LVS_EX_FLATSB	//��ƽ�����ʾ������
		| LVS_EX_FULLROWSELECT				//��������ѡ��
		| LVS_EX_HEADERDRAGDROP				//���������϶�
		| LVS_EX_ONECLICKACTIVATE			//����ѡ����
		| LVS_EX_GRIDLINES);					//����������
												//���ñ�ͷ
	m_Grid.InsertColumn(0, "�۲���Ԫ", LVCFMT_LEFT, 80, 0); //����������
	m_Grid.InsertColumn(1, "X/m", LVCFMT_LEFT, 100, 1); //���ôº���
	m_Grid.InsertColumn(2, "��x/m", LVCFMT_LEFT, 80, 2); //����������
	m_Grid.InsertColumn(3, "Y/m", LVCFMT_LEFT, 100, 3); //���ôº���
	m_Grid.InsertColumn(4, "��y/m", LVCFMT_LEFT, 80, 4); //����������
	m_Grid.InsertColumn(5, "Z/m", LVCFMT_LEFT, 100, 5); //���ôº���
	m_Grid.InsertColumn(6, "��z/m", LVCFMT_LEFT, 80, 6); //����������
	m_Grid.InsertColumn(7, "PDOP", LVCFMT_LEFT, 80, 6); //����������

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

void CSPPDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDHtmlDialog::OnSysCommand(nID, lParam);
	}
}

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CSPPDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDHtmlDialog::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
HCURSOR CSPPDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

HRESULT CSPPDlg::OnButtonOK(IHTMLElement* /*pElement*/)
{
	OnOK();
	return S_OK;
}

HRESULT CSPPDlg::OnButtonCancel(IHTMLElement* /*pElement*/)
{
	OnCancel();
	return S_OK;
}


void CSPPDlg::OnFileopen()
{
	// TODO: �ڴ���������������
	CFileDialog dlg(TRUE,NULL,NULL,OFN_HIDEREADONLY| OFN_HIDEREADONLY,
		"All Files (*.txt*)|*.TXT*||",AfxGetMainWnd());
	if(dlg.DoModal()==IDOK)
	{
		CString StrPath=dlg.GetPathName();
		m_path.SetWindowText(StrPath);
	}
	vector<CSPP> dataList;   //����һ�����������飩datalist
	CString StrPath;
	m_path.GetWindowText(StrPath);
	CString  tips;
	if(DataRead(_T(StrPath),dataList))
		tips="�����ļ���ȡ�ɹ�!  ";
	else tips="Error:�ļ���дʧ��!  ";
	MessageBox(tips,"��ʾ��Ϣ");
}


void CSPPDlg::OnSave()
{
	// TODO: �ڴ���������������
	int item_count=m_Grid.GetItemCount();
	if (item_count==0)
	{
		MessageBox(_T("�б�Ϊ��ʱ���ܵ�����"),_T("����"),MB_OK|MB_ICONEXCLAMATION);
		return;
	}
	CStdioFile RecFile;
	CFileException fileException;
	if (RecFile.Open("GPSα�������.txt",CFile::typeText|CFile::modeCreate|CFile::modeWrite|CFile::shareExclusive),&fileException)
	{
		/*m_HandleProgress.SetPos(0);*/
		char* old_locale = _strdup( setlocale(LC_CTYPE,NULL) ); 
		setlocale( LC_CTYPE, "chs" );//�趨����
		RecFile.WriteString(_T("�۲���Ԫ	X/m	  ��x/m  	      Y/m	��y/m 	             Z/m	 ��z/m          PDOP\r\n"));
		RecFile.WriteString(_T("===============================================================================================================\r\n"));
		for (int i=0;i<item_count;i++)
		{
			RecFile.WriteString(m_Grid.GetItemText(i,0));
			RecFile.WriteString(_T("\t"));
			RecFile.WriteString(m_Grid.GetItemText(i,1));
			RecFile.WriteString(_T("\t  "));
			RecFile.WriteString(m_Grid.GetItemText(i,2));
			RecFile.WriteString(_T("  \t"));
			RecFile.WriteString(m_Grid.GetItemText(i,3));
			RecFile.WriteString(_T("\t"));
			RecFile.WriteString(m_Grid.GetItemText(i,4));
			RecFile.WriteString(_T("  \t"));
			RecFile.WriteString(m_Grid.GetItemText(i,5));
			RecFile.WriteString(_T(" \t "));
			RecFile.WriteString(m_Grid.GetItemText(i,6));
			RecFile.WriteString(_T(" \t"));
			RecFile.WriteString(m_Grid.GetItemText(i,7));
			RecFile.WriteString(_T("\r\n"));
		}
		setlocale( LC_CTYPE, old_locale ); 
		free( old_locale );//��ԭ�����趨
		RecFile.Close();
	}

}


void CSPPDlg::OnAnother()
{
	// TODO: �ڴ���������������
	int item_count=m_Grid.GetItemCount();
	if (item_count==0)
	{
		MessageBox(_T("�б�Ϊ��ʱ���ܵ�����"),_T("����"),MB_OK|MB_ICONEXCLAMATION);
		return;
	}
	OPENFILENAME *ofn=new OPENFILENAME;
	TCHAR szFile[MAX_PATH];
	ZeroMemory(szFile,sizeof(szFile)/sizeof(TCHAR));
	ZeroMemory(ofn,sizeof(OPENFILENAME));
	ofn->lStructSize=sizeof(OPENFILENAME);
	ofn->hwndOwner = m_hWnd;  
	ofn->lpstrFile = szFile; 
	ofn->lpstrFile[0] = _T('\0');  
	ofn->nMaxFile = sizeof(szFile);
	ofn->lpstrFilter = _T("�ı��ļ�\0*.txt\0");
	ofn->nFilterIndex = 1;  
	ofn->lpstrFileTitle = NULL;  
	ofn->nMaxFileTitle = 0;  
	ofn->lpstrInitialDir = _T("Record//");  
	ofn->Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;  
	CString strFile;
	// ��ʾ��ѡ���ļ��Ի���  
	if ( GetSaveFileName(ofn))  
	{  
		//��ʾѡ����ļ���  
		strFile.Format(_T("%s"),szFile);
	}
	else
	{
	  return;
	}
	delete ofn;
	ofn=NULL;
	CString tmp;
	int len=strFile.GetLength();
	int lastdir=0;
	for (int i=len-1;i>=0;i--)
	{
		if (((int)strFile.GetAt(i))!=-1)
		{
			lastdir=i;//�������
			break;
		}
	}
	tmp=strFile.Right(4);
	int pos=tmp.Find(_T(".txt"));//�Һ�׺��
	if (pos==-1)
	{
		strFile+=_T(".txt");
	}
	CStdioFile RecFile;
	CFileException fileException;
	if (RecFile.Open(strFile,CFile::typeText|CFile::modeCreate|CFile::modeWrite|CFile::shareExclusive),&fileException)
	{
		/*m_HandleProgress.SetPos(0);*/
		char* old_locale = _strdup( setlocale(LC_CTYPE,NULL) ); 
		setlocale( LC_CTYPE, "chs" );//�趨����
		RecFile.WriteString(_T("�۲���Ԫ	X/m	  ��x/m  	      Y/m	��y/m 	             Z/m	 ��z/m          PDOP\r\n"));
		RecFile.WriteString(_T("===============================================================================================================\r\n"));
		for (int i=0;i<item_count;i++)
		{
			RecFile.WriteString(m_Grid.GetItemText(i,0));
			RecFile.WriteString(_T("\t"));
			RecFile.WriteString(m_Grid.GetItemText(i,1));
			RecFile.WriteString(_T("\t  "));
			RecFile.WriteString(m_Grid.GetItemText(i,2));
			RecFile.WriteString(_T("  \t"));
			RecFile.WriteString(m_Grid.GetItemText(i,3));
			RecFile.WriteString(_T("\t"));
			RecFile.WriteString(m_Grid.GetItemText(i,4));
			RecFile.WriteString(_T("  \t"));
			RecFile.WriteString(m_Grid.GetItemText(i,5));
			RecFile.WriteString(_T(" \t "));
			RecFile.WriteString(m_Grid.GetItemText(i,6));
			RecFile.WriteString(_T(" \t"));
			RecFile.WriteString(m_Grid.GetItemText(i,7));
			RecFile.WriteString(_T("\r\n"));
		}
		setlocale( LC_CTYPE, old_locale ); 
		free( old_locale );//��ԭ�����趨
		RecFile.Close();
	}
}


void CSPPDlg::OnClose()
{
	// TODO: �ڴ���������������
}


void CSPPDlg::OnCalculate()
{
	// TODO: �ڴ���������������
	vector<CSPP> dataList;   //����һ�����������飩datalist
	CString StrPath;
	m_path.GetWindowText(StrPath);
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
				dataList.push_back(info);
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
		dataList.push_back(info);

	double X,Y,Z,mx,my,mz,PDOP;
	/*m_Grid.InsertItem(0, "");
	m_Grid.SetItemText(0, 0, "�۲���Ԫ");
	m_Grid.SetItemText(0, 1,"X/m");
	m_Grid.SetItemText(0, 2,"��x");
	m_Grid.SetItemText(0, 3, "Y/m");
	m_Grid.SetItemText(0, 4,"��y");
	m_Grid.SetItemText(0, 5, "Z/m");
	m_Grid.SetItemText(0, 6,"��z");
	m_Grid.SetItemText(0, 7,"PDOP");*/
	const int N = dataList.size();
	vector<vector<double>> res6(4,vector<double>(1));
	vector<vector<double>> res3(4,vector<double>(4));
	for (int pos = 0; pos < N; pos++)
	{
		int sum=dataList[pos].m_SymbolSN;
		vector<double> R0(sum);
		vector<double> l(sum);
		vector<double> m(sum);
		vector<double> n(sum);
		for (int i = 0; i < sum; i++)
		{
			R0[i] = sqrtl(pow((dataList[pos].m_X[i] - head1.X), 2) + pow((dataList[pos].m_Y[i] - head1.Y), 2) + pow((dataList[pos].m_Z[i] - head1.Z), 2));
			l[i] = (dataList[pos].m_X[i] - head1.X) / R0[i];
			m[i] = (dataList[pos].m_Y[i] - head1.Y) / R0[i];
			n[i] = (dataList[pos].m_Z[i] - head1.Z) / R0[i];
		}
		vector<vector<double>> B(sum);
		for(int i=0;i<sum;i++)
		{
			B[i].resize(4);
		}
		for (int i = 0; i < sum; i++)
		{
			B[i][0] = l[i];
			B[i][1] = m[i];
			B[i][2] = n[i];
			B[i][3] = -1;

		}
			
		vector<vector<double>> L(1);
		for(int i=0;i<1;i++)
		{
			L[i].resize(sum);
		}
		for (int i = 0; i < sum; i++)
		{
			L[0][i] = dataList[pos].m_Cl[i] - R0[i] + dataList[pos].m_Clock[i] - dataList[pos].m_TtopDelay[i];
		}
		vector<double> m_CL(sum);
		for (int i = 0; i < sum; i++)
		{
			m_CL[i] = dataList[pos].m_Cl[i];
		}

		vector<double> p(sum);
		vector<vector<double>> P(sum);
		for(int i=0;i<sum;i++)
		{
			P[i].resize(sum);
		}
		for (int i = 0; i < sum; i++)
		{
			p[i] = sin(dataList[pos].m_Elevation[i] * 3.1415926 / 180) / 0.04;
		}
		for (int i = 0; i < sum; i++)
		{
			for (int j = 0; j < sum; j++)
			{
				if (i == j)
					P[i][j] = p[i];
				else
					P[i][j] = 0;
					
			}
		}

		vector<vector<double>> TL(sum,vector<double>(1));
		TL=operator_converse(L);
			
		vector<vector<double>> TB(4,vector<double>(sum));
		TB=operator_converse(B);
			
		vector<vector<double>> res1(4,vector<double>(sum));
		res1=operator_multiply(TB,P);
			
		vector<vector<double>> res2(4,vector<double>(4));
		res2=operator_multiply(res1,B);

		//Э�������
		res3=operator_inv(res2);

		vector<vector<double>> res4(4,vector<double>(sum));
		res4=operator_multiply(res3,TB);

		vector<vector<double>> res5(4,vector<double>(sum));
		res5=operator_multiply(res4,P);

		//ƫ����
		res6=operator_multiply(res5,TL);

		vector<vector<double>> D(4,vector<double>(1));
		D=operator_number(res6,-1.0);

		vector<vector<double>> V(sum,vector<double>(1));

		V=operator_add(operator_multiply(B,D),TL);

		vector<vector<double>> res7(1,vector<double>(sum));
		res7=operator_multiply(operator_converse(V),P);

		vector<vector<double>> res8(1,vector<double>(1));
		res8=operator_multiply(res7,V);

		X=head1.X+D[0][0];
		Y=head1.Y+D[1][0];
		Z=head1.Z+D[3][0];

		double m0=sqrtl(res8[0][0]/(sum-4));

		mx=m0*res3[0][0];
		my=m0*res3[1][1];
		mz=m0*res3[2][2];

		PDOP=sqrtl(res3[0][0]*res3[0][0]+res3[1][1]*res3[1][1]+res3[2][2]*res3[2][2]);

		char buf0[20];
		char buf1[20];
		char buf11[20];
		char buf2[20];
		char buf22[20];
		char buf3[20];
		char buf33[20];
		char buf4[20];
		m_Grid.InsertItem(pos, "");
		_itoa_s(dataList[pos].m_GPSTIME, buf0, 10);
		m_Grid.SetItemText(pos, 0, buf0);
		sprintf_s(buf1, "%0.4f", X);
		m_Grid.SetItemText(pos, 1, buf1);
		sprintf_s(buf11, "%0.4f", mx);
		m_Grid.SetItemText(pos, 2, buf11);
		sprintf_s(buf2, "%0.4f", Y);
		m_Grid.SetItemText(pos, 3, buf2);
		sprintf_s(buf22, "%0.4f", my);
		m_Grid.SetItemText(pos, 4, buf22);
		sprintf_s(buf3, "%0.4f", Z);
		m_Grid.SetItemText(pos, 5, buf3);
		sprintf_s(buf33, "%0.4f", mz);
		m_Grid.SetItemText(pos, 6, buf33);
		sprintf_s(buf4, "%0.4f", PDOP);
		m_Grid.SetItemText(pos, 7, buf4);		
    }

}



void CSPPDlg::OnSize(UINT nType, int cx, int cy)
{
	CDHtmlDialog::OnSize(nType, cx, cy);
	// TODO: �ڴ˴������Ϣ����������
	/*if (nType == SIZE_RESTORED || nType == SIZE_MAXIMIZED)
	{
		resize();
	}*/
	if (m_listRect.GetCount() > 0)  
    {  
		CRect dlgNow;  
		GetWindowRect(&dlgNow);  
		POSITION pos = m_listRect.GetHeadPosition();//��һ��������ǶԻ����Rect  
      
		CRect dlgSaved;  
		dlgSaved = m_listRect.GetNext(pos);  
		ScreenToClient(dlgNow);  
      
		float x = dlgNow.Width() * 1.0 / dlgSaved.Width();//���ݵ�ǰ��֮ǰ����ĶԻ���Ŀ�������  
		float y = dlgNow.Height()  *1.0/ dlgSaved.Height();  
		ClientToScreen(dlgNow);  
      
		CRect childSaved;  
      
		CWnd* pWnd = GetWindow(GW_CHILD);  
		while(pWnd)  
		{  
			childSaved = m_listRect.GetNext(pos);//���λ�ȡ�Ӵ����Rect  
			childSaved.left = dlgNow.left + (childSaved.left - dlgSaved.left)*x;//���ݱ��������ؼ��������Ҿ���Ի���ľ���  
			childSaved.right = dlgNow.right + (childSaved.right - dlgSaved.right)*x;  
			childSaved.top = dlgNow.top + (childSaved.top - dlgSaved.top)*y;  
			childSaved.bottom = dlgNow.bottom + (childSaved.bottom - dlgSaved.bottom)*y;  
			ScreenToClient(childSaved);  
			pWnd->MoveWindow(childSaved);  
			pWnd = pWnd->GetNextWindow();  
		}  
	}

}


//void CSPPDlg::resize(void)
//{
//	float fsp[2];
//	POINT Newp;//��ȡ���ڶԻ���Ĵ�С
//	CRect recta;
//	GetClientRect(&recta);//ȡ�ͻ����Ĵ�С
//	Newp.x = recta.right - recta.left;
//	Newp.y = recta.bottom - recta.top;
//	fsp[0] = (float)Newp.x / old.x;
//	fsp[1] = (float)Newp.y / old.y;
//
//
//	CRect Rect;
//	int woc;
//	CPoint OldTLPoint,TLPoint;//���Ͻ�
//	CPoint OldBRPoint,BRPoint;//���½�
//	HWND hwndChild = ::GetWindow(m_hWnd,GW_CHILD);//�г����пؼ�
//
//
//	while(hwndChild)
//	{
//		woc = ::GetDlgCtrlID(hwndChild);//ȡ��ID
//		GetDlgItem(woc)->GetWindowRect(Rect);
//		ScreenToClient(Rect);
//		OldTLPoint = Rect.TopLeft();
//		TLPoint.x = long(OldTLPoint.x * fsp[0]);
//		TLPoint.y = long(OldTLPoint.y * fsp[1]);
//		OldBRPoint = Rect.BottomRight();
//		BRPoint.x = long(OldBRPoint.x * fsp[0]);
//		BRPoint.y = long(OldBRPoint.y * fsp[1]);
//		Rect.SetRect(TLPoint,BRPoint);
//		GetDlgItem(woc)->MoveWindow(Rect,TRUE);
//		hwndChild = ::GetWindow(hwndChild,GW_HWNDNEXT);
//	}
//	old = Newp;
//}
