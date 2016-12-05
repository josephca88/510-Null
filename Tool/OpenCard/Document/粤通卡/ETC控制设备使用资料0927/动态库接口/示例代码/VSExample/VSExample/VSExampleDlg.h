
// VSExampleDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"
#include <memory>
#include "ecclient.h"

#define WM_DO (WM_USER + 100)

// CVSExampleDlg �Ի���
class CVSExampleDlg : public CDialogEx
{
// ����
public:
	CVSExampleDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_VSEXAMPLE_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButton1();
	CEdit laneEdit;
	CEdit userEdit;
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton3();

	LRESULT onMsg(WPARAM wParam, LPARAM lParam);

	template <typename T>
	void runInMain(T&& t);

	CEdit textEdit;

	std::unique_ptr<ECClient> m_client;
};
