
// VSExampleDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "VSExample.h"
#include "VSExampleDlg.h"
#include "afxdialogex.h"
#include "jsoncpp\include\json\json.h"
#include <functional>
#include <sstream>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CVSExampleDlg �Ի���




CVSExampleDlg::CVSExampleDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CVSExampleDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CVSExampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT2, laneEdit);
	DDX_Control(pDX, IDC_EDIT3, userEdit);
	DDX_Control(pDX, IDC_EDIT1, textEdit);
}

BEGIN_MESSAGE_MAP(CVSExampleDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, &CVSExampleDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CVSExampleDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CVSExampleDlg::OnBnClickedButton3)
	ON_MESSAGE(WM_DO, &CVSExampleDlg::onMsg)
END_MESSAGE_MAP()


// CVSExampleDlg ��Ϣ�������

BOOL CVSExampleDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������

	try
	{
		m_client.reset(new ECClient);
	}
	catch(...)
	{
		MessageBoxA("Load EtcController.dll Error", "Error", 0);
		::exit(1);
	}

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CVSExampleDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
HCURSOR CVSExampleDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

DWORD __stdcall invokeAndDelete(void *param)
{
	auto func = reinterpret_cast<std::function<void()>*>(param);
	try
	{
		(*func)();
	}
	catch(...)
	{
		MessageBoxA(0, "Exception", "Error", 0);
	}
	delete func;
	return 0;
}

LRESULT CVSExampleDlg::onMsg(WPARAM wParam, LPARAM lParam)
{
	invokeAndDelete(reinterpret_cast<void*>(lParam));
	return 0;
}

template <typename T>
void runInParallel(T&& t)
{
	auto func = new std::function<void()>(std::forward<T>(t));
	HANDLE hThread = CreateThread(nullptr, 0, invokeAndDelete, reinterpret_cast<void*>(func), 0, nullptr);
	if(hThread == 0)
	{
		delete func;
	}
	else
	{
		CloseHandle(hThread);
	}
}


template <typename T>
void CVSExampleDlg::runInMain(T&& t)
{
	auto func = new std::function<void()>(std::forward<T>(t));
	PostMessageA(WM_DO, 0, reinterpret_cast<LPARAM>(func));
}

void CVSExampleDlg::OnBnClickedButton1()
{
	runInParallel([this]()
	{
		char conf[4096] = {0}, err[1024] = {0};
        int num = 0;
        int rc = m_client->Initialize(conf, &num, err);
		auto self = this;
		if(rc == 0)
		{
			std::string strConf(conf);
			Json::Value json;
			Json::Reader reader;

			if(!reader.parse(strConf, json))
			{
				return;
			}

			std::string username = json["UserName"].asString();
			std::string laneno = json["LaneNo"].asString();

			runInMain([=]()
			{
				CString str;
				self->textEdit.GetWindowTextA(str);

				CString resp(strConf.c_str());
				resp.Replace("\n", "\r\n");
				std::string all = std::string(str.GetBuffer(0)) + "\r\n" + resp.GetBuffer(0);

				self->userEdit.SetWindowTextA(username.c_str());
				self->laneEdit.SetWindowTextA(laneno.c_str());

				self->textEdit.SetWindowTextA(all.c_str());
				self->textEdit.LineScroll(self->textEdit.GetLineCount());

				self->userEdit.UpdateData(FALSE);
				self->laneEdit.UpdateData(FALSE);
				self->textEdit.UpdateData(FALSE);
			});
		}
		else
		{
			std::string strErr(err);
			runInMain([=]()
			{
				CString str;
				self->textEdit.GetWindowTextA(str);
				CString resp(strErr.c_str());
				resp.Replace("\n", "\r\n");
				std::string all = std::string(str.GetBuffer(0)) + "\r\n" + resp.GetBuffer(0);

				self->textEdit.SetWindowTextA(all.c_str());
				self->textEdit.LineScroll(self->textEdit.GetLineCount());
				self->textEdit.UpdateData(FALSE);
			});
		}
	});
	// TODO: �ڴ���ӿؼ�֪ͨ����������
}


void CVSExampleDlg::OnBnClickedButton2()
{
	runInParallel([this]()
	{
		m_client->Uninstall();
	});
	// TODO: �ڴ���ӿؼ�֪ͨ����������
}


void CVSExampleDlg::OnBnClickedButton3()
{
	CString str;

	userEdit.GetWindowTextA(str);
	Json::Value json;
	json["UserName"] = str.GetBuffer(0);

	laneEdit.GetWindowTextA(str);
	int laneno = strtol(str.GetBuffer(0), nullptr, 10);


	std::string strJson = json.toStyledString();

	runInParallel([=]()
	{
		char buf[4096] = {0};
		int rc = m_client->StatusQuery(laneno, strJson.c_str(), buf);

		CString strBuf(buf);
		strBuf.Replace("\n", "\r\n");
		std::string strResp = strBuf.GetBuffer(0);

		auto self = this;
		runInMain([=]()
		{
			CString str;
			self->textEdit.GetWindowTextA(str);
			std::stringstream ss;
			ss << rc;

			std::string all = std::string(str.GetBuffer(0)) + "\r\n" + ss.str() + "\r\n" + strResp;

			self->textEdit.SetWindowTextA(all.c_str());
			self->textEdit.LineScroll(self->textEdit.GetLineCount());
			self->textEdit.UpdateData(FALSE);
		});
	});
	// TODO: �ڴ���ӿؼ�֪ͨ����������
}
