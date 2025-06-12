namespace NetSeal_Controller
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("NetSeal.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern bool InjectIntoProcess(uint processId, string dllPath);

        [System.Runtime.InteropServices.DllImport("NetSeal.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern bool AddFirewallBlockRule(string exePath);

        [System.Runtime.InteropServices.DllImport("NetSeal.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern bool RemoveFirewallBlockRule(string exePath);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern bool QueryFullProcessImageName(IntPtr hProcess, int flags, System.Text.StringBuilder text, ref int size);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(int access, bool inherit, uint pid);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        private const int PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

        private static string? GetProcessPath(uint pid)
        {
            IntPtr handle = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, pid);
            if (handle == IntPtr.Zero)
                return null;

            try
            {
                var sb = new System.Text.StringBuilder(1024);
                int size = sb.Capacity;
                if (QueryFullProcessImageName(handle, 0, sb, ref size))
                    return sb.ToString(0, size);
            }
            finally
            {
                CloseHandle(handle);
            }

            return null;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBoxProcesses.Items.Clear();
            foreach (var p in System.Diagnostics.Process.GetProcesses())
            {
                try
                {
                    listBoxProcesses.Items.Add($"{p.ProcessName} ({p.Id})");
                }
                catch
                {
                    // ignore processes that may not allow access
                }
            }
        }

        private void buttonInject_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.SelectedItem == null)
                return;

            var text = listBoxProcesses.SelectedItem.ToString();
            var pidStart = text.LastIndexOf('(');
            var pidEnd = text.LastIndexOf(')');
            if (pidStart == -1 || pidEnd == -1)
                return;

            if (!uint.TryParse(text.Substring(pidStart + 1, pidEnd - pidStart - 1), out uint pid))
                return;

            var dllPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NetSeal.dll");
            bool result = InjectIntoProcess(pid, dllPath);

            bool firewall = false;
            if (result)
            {
                try
                {
                    var path = GetProcessPath(pid);
                    if (!string.IsNullOrEmpty(path))
                    {
                        firewall = AddFirewallBlockRule(path);
                    }
                }
                catch
                {
                    firewall = false;
                }
            }

            MessageBox.Show(result && firewall ? "Injected and blocked" : "Injection or firewall failed");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.SelectedItem == null)
                return;

            var text = listBoxProcesses.SelectedItem.ToString();
            var pidStart = text.LastIndexOf('(');
            var pidEnd = text.LastIndexOf(')');
            if (pidStart == -1 || pidEnd == -1)
                return;

            if (!uint.TryParse(text.Substring(pidStart + 1, pidEnd - pidStart - 1), out uint pid))
                return;

            var path = GetProcessPath(pid);
            if (!string.IsNullOrEmpty(path))
            {
                bool removed = RemoveFirewallBlockRule(path);
                MessageBox.Show(removed ? "Firewall rule removed" : "Failed to remove rule");
            }
        }
    }
}
