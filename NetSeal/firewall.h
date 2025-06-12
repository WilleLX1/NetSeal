#pragma once
#include <windows.h>

#ifdef __cplusplus
extern "C" {
#endif

__declspec(dllexport) BOOL AddFirewallBlockRule(const wchar_t* exePath);

#ifdef __cplusplus
}
#endif
