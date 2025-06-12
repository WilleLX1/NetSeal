namespace Test_App
{
    public partial class Form1 : Form
    {
        private readonly System.Net.NetworkInformation.Ping _ping = new();
        private int _sent;
        private int _success;
        private long _totalTime;

        public Form1()
        {
            InitializeComponent();
            timerPing.Start();
        }

        private async void timerPing_Tick(object sender, EventArgs e)
        {
            try
            {
                var reply = await _ping.SendPingAsync("google.com", 1000);
                _sent++;
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    _success++;
                    _totalTime += reply.RoundtripTime;
                    listBoxLog.Items.Insert(0, $"Reply from {reply.Address}: {reply.RoundtripTime}ms");
                }
                else
                {
                    listBoxLog.Items.Insert(0, $"Ping failed: {reply.Status}");
                }

                labelStats.Text = $"Sent: {_sent}, Success: {_success}, Avg: {( _success > 0 ? _totalTime / _success : 0)}ms";
            }
            catch (Exception ex)
            {
                listBoxLog.Items.Insert(0, $"Error: {ex.Message}");
            }
        }
    }
}
