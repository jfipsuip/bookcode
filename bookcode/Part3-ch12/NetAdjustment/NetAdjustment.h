
// NetAdjustment.h : NetAdjustment Ӧ�ó������ͷ�ļ�
//
#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"       // ������


// CNetAdjustmentApp:
// �йش����ʵ�֣������ NetAdjustment.cpp
//

class CNetAdjustmentApp : public CWinApp
{
public:
	CNetAdjustmentApp();


// ��д
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// ʵ��
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CNetAdjustmentApp theApp;
