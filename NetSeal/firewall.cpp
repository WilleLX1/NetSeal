#include "pch.h"
#include <string>
#include <windows.h>

// Ensure the exported symbol uses C linkage to match the header
extern "C" {

// Adds a simple Windows Firewall rule to block outbound traffic for the
// current process. Returns TRUE on success.
BOOL AddFirewallBlockRule()
{
    wchar_t exePath[MAX_PATH];
    if (!GetModuleFileNameW(NULL, exePath, MAX_PATH))
        return FALSE;

    std::wstring command = L"netsh advfirewall firewall add rule name=\"NetSealBlock\" program=\"";
    command += exePath;
    command += L"\" dir=out action=block";

    // Execute the command silently
    int result = _wsystem(command.c_str());
    return result == 0;
}
}
