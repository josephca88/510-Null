
// VSExample.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CVSExampleApp:
// �йش����ʵ�֣������ VSExample.cpp
//

class CVSExampleApp : public CWinApp
{
public:
	CVSExampleApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CVSExampleApp theApp;