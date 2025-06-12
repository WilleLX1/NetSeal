#pragma once
#include <windows.h>

#ifdef __cplusplus
extern "C" {
#endif

__declspec(dllexport) BOOL WINAPI AddFirewallBlockRule(const wchar_t* exePath);
__declspec(dllexport) BOOL WINAPI RemoveFirewallBlockRule(const wchar_t* exePath);

#ifdef __cplusplus
}
#endif
