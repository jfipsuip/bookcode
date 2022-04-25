// ListDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "NetAdjustment.h"
#include "ListDlg.h"
#include "afxdialogex.h"


// CListDlg �Ի���

IMPLEMENT_DYNAMIC(CListDlg, CDialogEx)

CListDlg::CListDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_LISTDIALOG, pParent)
{

}

CListDlg::~CListDlg()
{
}

void CListDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_LIST, m_List);
}


BEGIN_MESSAGE_MAP(CListDlg, CDialogEx)
//    ON_NOTIFY(LVN_ITEMCHANGED, IDC_LIST, &CListDlg::OnLvnItemchangedList)
END_MESSAGE_MAP()


// CListDlg ��Ϣ�������

CListDlg *pList;
BOOL CListDlg::OnInitDialog()
{
    CDialogEx::OnInitDialog();

    pList = this;

    CRect rect;
    m_List.GetClientRect(&rect);
    m_List.SetExtendedStyle(m_List.GetExtendedStyle() | LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT);
    m_List.InsertColumn(0, "�� ��"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(1, "�� ��"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(2, "DX(m)"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(3, "DY(m)"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(4, "DZ(m)"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(5, "Norm "   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(6, "�����x" , LVCFMT_CENTER, 100);
    m_List.InsertColumn(7, "�����y" , LVCFMT_CENTER, 100);
    m_List.InsertColumn(8, "�����z" , LVCFMT_CENTER, 100);

    return TRUE;  // return TRUE unless you set the focus to a control
                  // �쳣: OCX ����ҳӦ���� FALSE
}


//void CListDlg::OnLvnItemchangedList(NMHDR *pNMHDR, LRESULT *pResult)
//{
//    LPNMLISTVIEW pNMLV = reinterpret_cast<LPNMLISTVIEW>(pNMHDR);
//    // TODO: �ڴ���ӿؼ�֪ͨ����������
//    *pResult = 0;
//}
