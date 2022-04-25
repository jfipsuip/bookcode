
// HeightFittingView.h : CHeightFittingView ��Ľӿ�
//

#pragma once
#include "afxcmn.h"
#include "EditDlg.h"
#include "ListDlg.h"
#include "PictureDlg.h"


class CHeightFittingView : public CFormView
{
protected: // �������л�����
	CHeightFittingView();
	DECLARE_DYNCREATE(CHeightFittingView)

public:
#ifdef AFX_DESIGN_TIME
	enum{ IDD = IDD_HeightFitting_FORM };
#endif

// ����
public:
	CHeightFittingDoc* GetDocument() const;

// ����
public:

// ��д
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
	virtual void OnInitialUpdate(); // ������һ�ε���

// ʵ��
public:
	virtual ~CHeightFittingView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// ���ɵ���Ϣӳ�亯��
protected:
	DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnFileNew();
    afx_msg void OnFileOpen();
    afx_msg void OnFileSave();
    afx_msg void OnFileSaveAs();

    afx_msg void OnEditUndo();
    afx_msg void OnEditCut();
    afx_msg void OnEditCopy();
    afx_msg void OnEditPaste();

public:
    CTabCtrl     m_Tab; 
    CEditDlg     ED;
    CListDlg     LD;
    CPictureDlg  PD;
    afx_msg void OnTcnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult);

    afx_msg void OnResultReport();
    afx_msg void OnPictureOpen();
    afx_msg void OnPictureClose();
    afx_msg void OnPictureBig();
    afx_msg void OnPictureSmall();
};

#ifndef _DEBUG  // HeightFittingView.cpp �еĵ��԰汾
inline CHeightFittingDoc* CHeightFittingView::GetDocument() const
   { return reinterpret_cast<CHeightFittingDoc*>(m_pDocument); }
#endif

