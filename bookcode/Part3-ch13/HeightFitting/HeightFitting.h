
// HeightFitting.h : HeightFitting Ӧ�ó������ͷ�ļ�
//
#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"       // ������


// CHeightFittingApp:
// �йش����ʵ�֣������ HeightFitting.cpp
//

class CHeightFittingApp : public CWinApp
{
public:
	CHeightFittingApp();


// ��д
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// ʵ��
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CHeightFittingApp theApp;
