
// NetAdjustmentView.cpp : CNetAdjustmentView ���ʵ��
//

#include "stdafx.h"
// SHARED_HANDLERS ������ʵ��Ԥ��������ͼ������ɸѡ�������
// ATL ��Ŀ�н��ж��壬�����������Ŀ�����ĵ����롣
#ifndef SHARED_HANDLERS
#include "NetAdjustment.h"
#endif

#include "NetAdjustmentDoc.h"
#include "NetAdjustmentView.h"

#include "EditDlg.h"
#include "DataCenter.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CNetAdjustmentView

IMPLEMENT_DYNCREATE(CNetAdjustmentView, CFormView)

BEGIN_MESSAGE_MAP(CNetAdjustmentView, CFormView)
    ON_COMMAND(ID_FILE_NEW, &CNetAdjustmentView::OnFileNew)
    ON_COMMAND(ID_FILE_OPEN, &CNetAdjustmentView::OnFileOpen)
    ON_COMMAND(ID_FILE_SAVE, &CNetAdjustmentView::OnFileSave)
    ON_COMMAND(ID_FILE_SAVE_AS, &CNetAdjustmentView::OnFileSaveAs)
    ON_NOTIFY(TCN_SELCHANGE, IDC_TAB, &CNetAdjustmentView::OnTcnSelchangeTab)
    ON_COMMAND(ID_EDIT_UNDO, &CNetAdjustmentView::OnEditUndo)
    ON_COMMAND(ID_EDIT_CUT, &CNetAdjustmentView::OnEditCut)
    ON_COMMAND(ID_EDIT_COPY, &CNetAdjustmentView::OnEditCopy)
    ON_COMMAND(ID_EDIT_PASTE, &CNetAdjustmentView::OnEditPaste)
    ON_COMMAND(ID_RESULT_REPORT, &CNetAdjustmentView::OnResultReport)
    ON_COMMAND(ID_PICTURE_OPEN, &CNetAdjustmentView::OnPictureOpen)
    ON_COMMAND(ID_PICTURE_CLOSE, &CNetAdjustmentView::OnPictureClose)
    ON_COMMAND(ID_PICTURE_BIG, &CNetAdjustmentView::OnPictureBig)
    ON_COMMAND(ID_PICTURE_SMALL, &CNetAdjustmentView::OnPictureSmall)
END_MESSAGE_MAP()

// CNetAdjustmentView ����/����

CNetAdjustmentView::CNetAdjustmentView()
	: CFormView(IDD_NETADJUSTMENT_FORM)
{
	// TODO: �ڴ˴���ӹ������

}

CNetAdjustmentView::~CNetAdjustmentView()
{
}

void CNetAdjustmentView::DoDataExchange(CDataExchange* pDX)
{
    CFormView::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_TAB, m_Tab);
}

BOOL CNetAdjustmentView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: �ڴ˴�ͨ���޸�
	//  CREATESTRUCT cs ���޸Ĵ��������ʽ

	return CFormView::PreCreateWindow(cs);
}

void CNetAdjustmentView::OnInitialUpdate()
{
	CFormView::OnInitialUpdate();
	GetParentFrame()->RecalcLayout();
	ResizeParentToFit();

    CRect rect;
    m_Tab.GetClientRect(&rect);

    m_Tab.InsertItem(0, "���");
    m_Tab.InsertItem(1, "����");
    m_Tab.InsertItem(2, "ͼ��");

    rect.top    +=  4;
    rect.bottom -= 20;
    rect.left   +=  4;
    rect.right  -=  4;

    LD.Create(IDD_LISTDIALOG,    &m_Tab);
    ED.Create(IDD_EDITDIALOG,    &m_Tab);
    PD.Create(IDD_PICTUREDIALOG, &m_Tab);

    LD.MoveWindow(rect);
    ED.MoveWindow(rect);
    PD.MoveWindow(rect);


    LD.ShowWindow(TRUE);
    m_Tab.SetCurSel(0);
}


#ifdef _DEBUG
void CNetAdjustmentView::AssertValid() const
{
	CFormView::AssertValid();
}

void CNetAdjustmentView::Dump(CDumpContext& dc) const
{
	CFormView::Dump(dc);
}

CNetAdjustmentDoc* CNetAdjustmentView::GetDocument() const // �ǵ��԰汾��������
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CNetAdjustmentDoc)));
	return (CNetAdjustmentDoc*)m_pDocument;
}
#endif //_DEBUG


// CNetAdjustmentView ��Ϣ�������

/*----------------------------------------------------------------------
 * ��     �ܣ� �½�
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnFileNew()
{
    if (pEdit->m_Edit.GetModify())
    {
        int res = MessageBox("�ļ������Ѹı䣬�Ƿ񱣴�", "��ʾ��Ϣ", MB_YESNOCANCEL);
        if (res == IDYES)
            OnFileSave();
        else
            return;
    }
}
/*----------------------------------------------------------------------
 * ��     �ܣ� ���Ϊ
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnFileSaveAs()
{
    TCHAR fileFilter[] = "�ı��ļ�(*.txt)|*.txt|ȫ���ļ�(*.*)|*.*||";
    CFileDialog fileDlg(FALSE, "txt", 0, NULL, fileFilter, this);
    if (fileDlg.DoModal())
    {
        CString filePath = fileDlg.GetPathName();
        szCurrentPath    = filePath;
    
        CStdioFile file;
        if (!file.Open(filePath, CFile::modeCreate | CFile::modeWrite))
        {
            AfxMessageBox("����ʧ�ܣ�");
            return;
        }
        
        CString text;
        pEdit->m_Edit.GetWindowText(text);
        file.Write(text, text.GetLength());
        file.Flush();
        file.Close();

        pEdit->m_Edit.SetModify(FALSE);
    }
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnFileSave()
{
    if (szCurrentPath == "")
        OnFileSaveAs();
    else
    {
        CStdioFile file;
        if (!file.Open(szCurrentPath, CFile::modeCreate | CFile::modeWrite))
        {
            AfxMessageBox("���Ϊʧ�ܣ�");
            return;
        }

        CString text;
        pEdit->m_Edit.GetWindowText(text);
        file.Write(text, text.GetLength());
        file.Flush();
        file.Close();

        pEdit->m_Edit.SetModify(FALSE);
    }
}


/*----------------------------------------------------------------------
 * ��     �ܣ� ѡ��л�
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnTcnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult)
{
    *pResult = 0;

    int res  = m_Tab.GetCurSel();
    switch (res)
    {
    case 0: LD.ShowWindow(TRUE );
            ED.ShowWindow(FALSE);
            PD.ShowWindow(FALSE);
            break;
    case 1: LD.ShowWindow(FALSE);
            ED.ShowWindow(TRUE );
            PD.ShowWindow(FALSE);
            break;
    case 2: LD.ShowWindow(FALSE);
            ED.ShowWindow(FALSE);
            PD.ShowWindow(TRUE);
            break;
    default:LD.ShowWindow(TRUE );
            ED.ShowWindow(FALSE);
            PD.ShowWindow(FALSE);
            break;
    }
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnEditUndo()
{
    pEdit->m_Edit.Undo();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnEditCut()
{
    pEdit->m_Edit.Cut();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnEditCopy()
{
    pEdit->m_Edit.Copy();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ճ��
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnEditPaste()
{
    pEdit->m_Edit.Paste();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ���ļ�
 *----------------------------------------------------------------------*/
bool isDataReady = false;

void CNetAdjustmentView::OnFileOpen()
{
    if (isDataReady == true) 
    {
        delete pN_Aj;
        pN_Aj = NULL;
        isDataReady = false;
    }
        

    TCHAR fileFilter[] = "�ı��ļ�(*.txt)|*.txt|ȫ���ļ�(*.*)|*.*||";
    CFileDialog fileDlg(TRUE, "txt", 0, NULL, fileFilter, this);
    if (fileDlg.DoModal())
    {
        CString filePath = fileDlg.GetPathName();
        inFilePath = filePath;

        int pos  = inFilePath.ReverseFind(_T('\\'));
        if (pos >= 0)
            filePath = inFilePath.Left(pos);

        DataInOut D_IO;
        netName.clear();
        pN_Aj = new NetAdjustment();


        if (D_IO.readData(*pN_Aj, filePath))
        {
            ViewCenter::viewList(pList->m_List, netName);
            isDataReady = true;
        }
        else
            AfxMessageBox("�ļ���ȡʧ�ܣ�");
    }

}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����ƽ���
 *----------------------------------------------------------------------*/
bool isDataCal = false;
void CNetAdjustmentView::OnResultReport()
{
    if (!isDataReady)
    {
        AfxMessageBox("���ݻ�δ��ȡ�����ȶ�ȡ���ݣ�");
        return;
    }
    if (isDataCal)     
        return;

    pN_Aj->adjument(netName);
    isDataCal = true;
    ViewCenter::getMax();
    CString text = ViewCenter::viewReport();
    if (text != "")
    {
        pEdit->m_Edit.SetWindowTextA(text);
        pEdit->m_Edit.SetModify(FALSE);
    }
    else
        AfxMessageBox("��������ʧ��");
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ��ͼƬ
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnPictureOpen()
{
    if (!isDataReady)
    {
        AfxMessageBox("���ݻ�δ��ȡ�����ȶ�ȡ���ݣ�");
        return;
    }
    if (!isDataCal)
    {
        AfxMessageBox("���ݻ�δ���㣬���ȶ�ȡ���ݣ�");
        return;
    }
    CRect rect;
    pPicture->m_Picture.GetClientRect(&rect);
    ViewCenter VT;
    VT.viewPicture(pPicture->m_Picture.GetWindowDC(), rect);
}

/*----------------------------------------------------------------------
 * ��     �ܣ� �ر�ͼƬ
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnPictureClose()
{
    pPicture->OnPaint();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� �Ŵ�
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnPictureBig()
{
    OnPictureClose();

    range.ratio += 0.2;
    if (range.ratio == 4)
        range.ratio =  4;
    OnPictureOpen();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ��С
 *----------------------------------------------------------------------*/
void CNetAdjustmentView::OnPictureSmall()
{
    OnPictureClose();

    range.ratio -= 0.2;
    if (range.ratio == 0.2)
        range.ratio =  0.2;
    OnPictureOpen();
}
