namespace Test_App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Label labelStats;
        private System.Windows.Forms.Timer timerPing;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.labelStats = new System.Windows.Forms.Label();
            this.timerPing = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();

            // listBoxLog
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(12, 12);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(360, 364);

            // labelStats
            this.labelStats.AutoSize = true;
            this.labelStats.Location = new System.Drawing.Point(12, 382);
            this.labelStats.Name = "labelStats";
            this.labelStats.Size = new System.Drawing.Size(33, 15);
            this.labelStats.Text = "Stats";

            // timerPing
            this.timerPing.Interval = 1000;
            this.timerPing.Tick += new System.EventHandler(this.OnTimerPingTick);

            // Form1
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 417);
            this.Controls.Add(this.labelStats);
            this.Controls.Add(this.listBoxLog);
            this.Name = "Form1";
            this.Text = "Ping Test";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
