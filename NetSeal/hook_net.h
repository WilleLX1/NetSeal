#pragma once
#include <windows.h>
#ifdef __cplusplus
extern "C" {
#endif

BOOL WINAPI InitializeHooks();
void WINAPI UninitializeHooks();

#ifdef __cplusplus
}
#endif
