namespace NetSeal_Controller
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
        private System.Windows.Forms.ListBox listBoxProcesses;
        private System.Windows.Forms.Button buttonInject;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxProcesses = new System.Windows.Forms.ListBox();
            this.buttonInject = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // listBoxProcesses
            this.listBoxProcesses.FormattingEnabled = true;
            this.listBoxProcesses.ItemHeight = 15;
            this.listBoxProcesses.Location = new System.Drawing.Point(12, 12);
            this.listBoxProcesses.Name = "listBoxProcesses";
            this.listBoxProcesses.Size = new System.Drawing.Size(360, 364);

            // buttonInject
            this.buttonInject.Location = new System.Drawing.Point(12, 382);
            this.buttonInject.Name = "buttonInject";
            this.buttonInject.Size = new System.Drawing.Size(360, 23);
            this.buttonInject.Text = "Inject";
            this.buttonInject.UseVisualStyleBackColor = true;
            this.buttonInject.Click += new System.EventHandler(this.buttonInject_Click);

            // Form1
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 416);
            this.Controls.Add(this.buttonInject);
            this.Controls.Add(this.listBoxProcesses);
            this.Name = "Form1";
            this.Text = "NetSeal Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
