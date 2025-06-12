namespace NetSeal_Controller
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("NetSeal.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern bool InjectIntoProcess(uint processId, string dllPath);

        [System.Runtime.InteropServices.DllImport("NetSeal.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern bool AddFirewallBlockRule(string exePath);

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
                    var proc = System.Diagnostics.Process.GetProcessById((int)pid);
                    firewall = AddFirewallBlockRule(proc.MainModule.FileName);
                }
                catch
                {
                    firewall = false;
                }
            }

            MessageBox.Show(result && firewall ? "Injected and blocked" : "Injection or firewall failed");
        }
    }
}
