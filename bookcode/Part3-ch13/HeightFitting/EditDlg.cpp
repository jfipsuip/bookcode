// EditDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "HeightFitting.h"
#include "EditDlg.h"
#include "afxdialogex.h"


// CEditDlg �Ի���

IMPLEMENT_DYNAMIC(CEditDlg, CDialogEx)

CEditDlg::CEditDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_EDITDIALOG, pParent)
{

}

CEditDlg::~CEditDlg()
{
}

void CEditDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_EDIT, m_Edit);
}


BEGIN_MESSAGE_MAP(CEditDlg, CDialogEx)
END_MESSAGE_MAP()


// CEditDlg ��Ϣ�������

CEditDlg *pEdit;
BOOL CEditDlg::OnInitDialog()
{
    CDialogEx::OnInitDialog();

    pEdit = this;

    return TRUE;  // return TRUE unless you set the focus to a control
                  // �쳣: OCX ����ҳӦ���� FALSE
}
