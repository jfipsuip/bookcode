#pragma once
#include "afxcmn.h"


// CListDlg �Ի���

class CListDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CListDlg)

public:
	CListDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CListDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_LISTDIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    CListCtrl m_List;
    virtual BOOL OnInitDialog();
//    afx_msg void OnLvnItemchangedList(NMHDR *pNMHDR, LRESULT *pResult);
};

extern CListDlg *pList;