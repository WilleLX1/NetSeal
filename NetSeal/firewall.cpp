#include "pch.h"
#include <string>
#include <windows.h>
#include <wchar.h>
#include <cstdlib>

// Ensure the exported symbol uses C linkage to match the header
extern "C" {

// Adds a Windows Firewall rule to block outbound traffic for the
// specified executable path. Returns TRUE on success.
BOOL WINAPI AddFirewallBlockRule(const wchar_t* exePath)
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

// Removes the firewall rule for the specified executable.
BOOL WINAPI RemoveFirewallBlockRule(const wchar_t* exePath)
{
    if (!exePath || wcslen(exePath) == 0)
        return FALSE;

    std::wstring command = L"netsh advfirewall firewall delete rule name=\"NetSealBlock\" program=\"";
    command += exePath;
    command += L"\"";

    int result = _wsystem(command.c_str());
    return result == 0;
}
}
