
// SPPDlg.h : ͷ�ļ�
//

#pragma once


// CSPPDlg �Ի���
class CSPPDlg : public CDHtmlDialog
{
// ����
public:
	CSPPDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_SPP_DIALOG, IDH = IDR_HTML_SPP_DIALOG };

	CEdit m_path;
	CListCtrl m_Grid;
	CStatusBar m_StatusBar;
	CList<CRect,CRect&> m_listRect; 

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��

	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
	afx_msg void OnFileopen();
	afx_msg void OnSave();
	afx_msg void OnAnother();
	afx_msg void OnClose();
	afx_msg void OnCalculate();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	void resize(void);

	POINT old;

};
