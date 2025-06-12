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

Administrator privileges are only required if the target process runs with
elevated permissions.

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
   selected process. The injected DLL hooks network APIs to block outbound
   connections.

## Internet Blocking
The DLL hooks the Winsock `connect` function using the bundled **MinHook**
library. Any connection attempts from the target process fail, preventing
outbound traffic without modifying firewall rules.

## Test App

The repository also includes a simple WinForms application under `Test_App`.
When started, the app automatically begins pinging `google.com` once per
second and displays each response in a log along with running statistics. This
application is useful to demonstrate that injecting `NetSeal.dll` blocks the
network requests.

Build the test app with:

```bash
dotnet build Test_App/Test_App.csproj -c Release
```
