#pragma once
#include "afxwin.h"


// CPictureDlg �Ի���

class CPictureDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CPictureDlg)

public:
	CPictureDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CPictureDlg();

// �Ի�������
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_PICTUREDIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	DECLARE_MESSAGE_MAP()
public:
    CStatic m_Picture;
    virtual BOOL OnInitDialog();
    afx_msg void OnPaint();
};

extern CPictureDlg *pPicture;