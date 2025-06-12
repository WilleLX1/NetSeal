#include "pch.h"
#include <string>
#include <windows.h>
#include <wchar.h>

// Ensure the exported symbol uses C linkage to match the header
extern "C" {

// Adds a Windows Firewall rule to block outbound traffic for the
// specified executable path. Returns TRUE on success.
BOOL AddFirewallBlockRule(const wchar_t* exePath)
{
    if (!exePath || wcslen(exePath) == 0)
        return FALSE;

    std::wstring command = L"netsh advfirewall firewall add rule name=\"NetSealBlock\" program=\"";
    command += exePath;
    command += L"\" dir=out action=block";

    // Execute the command silently
    int result = _wsystem(command.c_str());
    return result == 0;
}
}
