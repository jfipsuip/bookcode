
// HeightFittingView.cpp : CHeightFittingView ���ʵ��
//

#include "stdafx.h"
// SHARED_HANDLERS ������ʵ��Ԥ��������ͼ������ɸѡ�������
// ATL ��Ŀ�н��ж��壬�����������Ŀ�����ĵ����롣
#ifndef SHARED_HANDLERS
#include "HeightFitting.h"
#endif

#include "HeightFittingDoc.h"
#include "HeightFittingView.h"

#include "EditDlg.h"
#include "DataCenter.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CHeightFittingView

IMPLEMENT_DYNCREATE(CHeightFittingView, CFormView)

BEGIN_MESSAGE_MAP(CHeightFittingView, CFormView)
    ON_COMMAND(ID_FILE_NEW, &CHeightFittingView::OnFileNew)
    ON_COMMAND(ID_FILE_OPEN, &CHeightFittingView::OnFileOpen)
    ON_COMMAND(ID_FILE_SAVE, &CHeightFittingView::OnFileSave)
    ON_COMMAND(ID_FILE_SAVE_AS, &CHeightFittingView::OnFileSaveAs)
    ON_NOTIFY(TCN_SELCHANGE, IDC_TAB, &CHeightFittingView::OnTcnSelchangeTab)
    ON_COMMAND(ID_EDIT_UNDO, &CHeightFittingView::OnEditUndo)
    ON_COMMAND(ID_EDIT_CUT, &CHeightFittingView::OnEditCut)
    ON_COMMAND(ID_EDIT_COPY, &CHeightFittingView::OnEditCopy)
    ON_COMMAND(ID_EDIT_PASTE, &CHeightFittingView::OnEditPaste)
    ON_COMMAND(ID_RESULT_REPORT, &CHeightFittingView::OnResultReport)
    ON_COMMAND(ID_PICTURE_OPEN, &CHeightFittingView::OnPictureOpen)
    ON_COMMAND(ID_PICTURE_CLOSE, &CHeightFittingView::OnPictureClose)
    ON_COMMAND(ID_PICTURE_BIG, &CHeightFittingView::OnPictureBig)
    ON_COMMAND(ID_PICTURE_SMALL, &CHeightFittingView::OnPictureSmall)
END_MESSAGE_MAP()

// CHeightFittingView ����/����

CHeightFittingView::CHeightFittingView()
	: CFormView(IDD_HeightFitting_FORM)
{
	// TODO: �ڴ˴���ӹ������

}

CHeightFittingView::~CHeightFittingView()
{
}

void CHeightFittingView::DoDataExchange(CDataExchange* pDX)
{
    CFormView::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_TAB, m_Tab);
}

BOOL CHeightFittingView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: �ڴ˴�ͨ���޸�
	//  CREATESTRUCT cs ���޸Ĵ��������ʽ

	return CFormView::PreCreateWindow(cs);
}

void CHeightFittingView::OnInitialUpdate()
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
void CHeightFittingView::AssertValid() const
{
	CFormView::AssertValid();
}

void CHeightFittingView::Dump(CDumpContext& dc) const
{
	CFormView::Dump(dc);
}

CHeightFittingDoc* CHeightFittingView::GetDocument() const // �ǵ��԰汾��������
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CHeightFittingDoc)));
	return (CHeightFittingDoc*)m_pDocument;
}
#endif //_DEBUG


// CHeightFittingView ��Ϣ�������

/*----------------------------------------------------------------------
 * ��     �ܣ� �½�
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnFileNew()
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
void CHeightFittingView::OnFileSaveAs()
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
void CHeightFittingView::OnFileSave()
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
void CHeightFittingView::OnTcnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult)
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
void CHeightFittingView::OnEditUndo()
{
    pEdit->m_Edit.Undo();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditCut()
{
    pEdit->m_Edit.Cut();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ����
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditCopy()
{
    pEdit->m_Edit.Copy();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ճ��
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditPaste()
{
    pEdit->m_Edit.Paste();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� ���ļ�
 *----------------------------------------------------------------------*/
bool isDataReady = false;

void CHeightFittingView::OnFileOpen()
{
    if (isDataReady == true) 
    {
        delete pHfit;
        pHfit = NULL;
        isDataReady = false;
    }
        

    TCHAR fileFilter[] = "�ı��ļ�(*.txt)|*.txt|ȫ���ļ�(*.*)|*.*||";
    CFileDialog fileDlg(TRUE, "txt", 0, NULL, fileFilter, this);
    if (fileDlg.DoModal())
    {
        CString filePath = fileDlg.GetPathName();
    
        DataInOut D_IO;
        pHfit = new HeightFitting();

        if (D_IO.readData(*pHfit, filePath))
        {
            ViewCenter::viewList(pList->m_List, pHfit->pList);
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
void CHeightFittingView::OnResultReport()
{
    if (!isDataReady)
    {
        AfxMessageBox("���ݻ�δ��ȡ�����ȶ�ȡ���ݣ�");
        return;
    }
    if (isDataCal)     
        return;

    pHfit->adjument();
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
void CHeightFittingView::OnPictureOpen()
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
void CHeightFittingView::OnPictureClose()
{
    pPicture->OnPaint();
}

/*----------------------------------------------------------------------
 * ��     �ܣ� �Ŵ�
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnPictureBig()
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
void CHeightFittingView::OnPictureSmall()
{
    OnPictureClose();

    range.ratio -= 0.2;
    if (range.ratio == 0.2)
        range.ratio =  0.2;
    OnPictureOpen();
}
