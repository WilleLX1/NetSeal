# NetSeal
An application that injects a custom DLL into selected processes and it blocks all internet access for that application.

## Building
The repository contains a Visual Studio solution (`NetSeal.sln`). Open it in
Visual Studio and build both projects using the **Release** configuration. The
controller is a WinForms application written in C#, while `NetSeal` produces a
DLL. You can also build from the command line with MSBuild or the `dotnet`
tools:

```bash
# Build the C# controller
dotnet build NetSeal_Controller/NetSeal_Controller.csproj -c Release

# Build the C++ DLL
msbuild NetSeal/NetSeal.vcxproj -p:Configuration=Release
```

Administrator privileges may be required when running the controller because
the injected DLL modifies the Windows Firewall.

## DLL Injection
The C++ project exposes a function `InjectIntoProcess` that uses
`CreateRemoteThread` and `LoadLibraryA` to load the NetSeal DLL into a
specified process ID.

## Controller UI
The `NetSeal_Controller` WinForms app enumerates running processes and displays
them in a list box.

1. Build and run `NetSeal_Controller`.
2. The window shows all processes that are accessible to the user.
3. Select a process and click **Inject**.
4. The application calls `InjectIntoProcess`, loading `NetSeal.dll` into the
   selected process. Once loaded, the DLL adds a firewall rule blocking that
   process's outbound network traffic.

## Internet Blocking
When the DLL is loaded into a target process, it calls `AddFirewallBlockRule`.
This function uses the Windows `netsh` command line tool to create a firewall
rule that blocks all outbound traffic for the current process.

## Test App

The repository also includes a simple WinForms application under `Test_App`.
When run, the window continuously pings `google.com` and displays the round-trip
time along with basic statistics. Injecting `NetSeal.dll` into this process will
demonstrate that the network requests are blocked.

Build the test app with:

```bash
dotnet build Test_App/Test_App.csproj -c Release
```
