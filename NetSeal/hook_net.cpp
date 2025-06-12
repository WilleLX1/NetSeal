#include "pch.h"
#include "hook_net.h"
#include "minhook/MinHook.h"
#include <winsock2.h>

#pragma comment(lib, "ws2_32.lib")

static decltype(&connect) fpConnect = nullptr;

static int WSAAPI MyConnect(SOCKET s, const struct sockaddr* name, int namelen)
{
    WSASetLastError(WSAEACCES);
    return SOCKET_ERROR; // Block the connection
}

BOOL WINAPI InitializeHooks()
{
    if (MH_Initialize() != MH_OK)
        return FALSE;

    HMODULE hWs2 = GetModuleHandleW(L"ws2_32.dll");
    if (!hWs2)
        hWs2 = LoadLibraryW(L"ws2_32.dll");
    if (!hWs2)
        return FALSE;

    FARPROC target = GetProcAddress(hWs2, "connect");
    if (!target)
        return FALSE;

    if (MH_CreateHook(target, &MyConnect, reinterpret_cast<LPVOID*>(&fpConnect)) != MH_OK)
        return FALSE;

    if (MH_EnableHook(target) != MH_OK)
        return FALSE;

    return TRUE;
}

void WINAPI UninitializeHooks()
{
    MH_DisableHook(MH_ALL_HOOKS);
    MH_Uninitialize();
}

