# NetSeal
An application that injects a custom DLL into selected processes and it blocks all internet access for that application.

## DLL Injection
The C++ project exposes a function `InjectIntoProcess` that uses
`CreateRemoteThread` and `LoadLibraryA` to load the NetSeal DLL into a
specified process ID.
