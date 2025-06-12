#include "pch.h"
#include "hook_net.h"

#include "minhook/MinHook.h"
#include <winsock2.h>
#include <iphlpapi.h>

#pragma comment(lib, "iphlpapi.lib")

#pragma comment(lib, "ws2_32.lib")

static decltype(&connect) fpConnect = nullptr;
static decltype(&IcmpSendEcho) fpIcmpSendEcho = nullptr;
static decltype(&IcmpSendEcho2) fpIcmpSendEcho2 = nullptr;

static int WSAAPI MyConnect(SOCKET s, const struct sockaddr* name, int namelen)
{
    WSASetLastError(WSAEACCES);
    return SOCKET_ERROR; // Block the connection
}

static DWORD WINAPI MyIcmpSendEcho(
    HANDLE IcmpHandle,
    IPAddr DestinationAddress,
    LPVOID RequestData,
    WORD RequestSize,
    PIP_OPTION_INFORMATION RequestOptions,
    LPVOID ReplyBuffer,
    DWORD ReplySize,
    DWORD Timeout)
{
    SetLastError(ERROR_ACCESS_DENIED);
    return 0; // Fail the request
}

static DWORD WINAPI MyIcmpSendEcho2(
    HANDLE IcmpHandle,
    HANDLE Event,
    PIO_APC_ROUTINE ApcRoutine,
    PVOID ApcContext,
    IPAddr DestinationAddress,
    LPVOID RequestData,
    WORD RequestSize,
    PIP_OPTION_INFORMATION RequestOptions,
    LPVOID ReplyBuffer,
    DWORD ReplySize,
    DWORD Timeout)
{
    SetLastError(ERROR_ACCESS_DENIED);
    return 0; // Fail the request
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

    HMODULE hIphlp = GetModuleHandleW(L"iphlpapi.dll");
    if (!hIphlp)
        hIphlp = LoadLibraryW(L"iphlpapi.dll");
    if (hIphlp)
    {
        FARPROC tIcmp2 = GetProcAddress(hIphlp, "IcmpSendEcho2");
        if (tIcmp2)
        {
            if (MH_CreateHook(tIcmp2, &MyIcmpSendEcho2, reinterpret_cast<LPVOID*>(&fpIcmpSendEcho2)) == MH_OK)
                MH_EnableHook(tIcmp2);
        }

        FARPROC tIcmp = GetProcAddress(hIphlp, "IcmpSendEcho");
        if (tIcmp)
        {
            if (MH_CreateHook(tIcmp, &MyIcmpSendEcho, reinterpret_cast<LPVOID*>(&fpIcmpSendEcho)) == MH_OK)
                MH_EnableHook(tIcmp);
        }
    }

    return TRUE;
}

void WINAPI UninitializeHooks()
{
    MH_DisableHook(MH_ALL_HOOKS);
    MH_Uninitialize();
}

