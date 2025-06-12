#pragma once
#include <windows.h>

#ifdef __cplusplus
extern "C" {
#endif

__declspec(dllexport) BOOL WINAPI InjectIntoProcess(DWORD processId, const char* dllPath);

#ifdef __cplusplus
}
#endif
