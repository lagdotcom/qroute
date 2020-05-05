namespace QuakeDemoFun
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.Timeline = new System.Windows.Forms.TrackBar();
            this.TimeLabel = new System.Windows.Forms.TextBox();
            this.PlayButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBSPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenDem = new System.Windows.Forms.OpenFileDialog();
            this.Display = new QuakeDemoFun.DemoDisplay();
            this.OpenBsp = new System.Windows.Forms.OpenFileDialog();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timeline)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.Timeline);
            this.BottomPanel.Controls.Add(this.TimeLabel);
            this.BottomPanel.Controls.Add(this.PlayButton);
            this.BottomPanel.Controls.Add(this.StopButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 405);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(800, 45);
            this.BottomPanel.TabIndex = 1;
            // 
            // Timeline
            // 
            this.Timeline.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Timeline.Enabled = false;
            this.Timeline.Location = new System.Drawing.Point(100, 0);
            this.Timeline.Name = "Timeline";
            this.Timeline.Size = new System.Drawing.Size(610, 45);
            this.Timeline.TabIndex = 3;
            this.Timeline.ValueChanged += new System.EventHandler(this.Timeline_ValueChanged);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TimeLabel.Enabled = false;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(0, 0);
            this.TimeLabel.Multiline = true;
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(100, 45);
            this.TimeLabel.TabIndex = 2;
            this.TimeLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TimeLabel.TextChanged += new System.EventHandler(this.TimeLabel_TextChanged);
            // 
            // PlayButton
            // 
            this.PlayButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.PlayButton.Enabled = false;
            this.PlayButton.Location = new System.Drawing.Point(710, 0);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(45, 45);
            this.PlayButton.TabIndex = 4;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(755, 0);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(45, 45);
            this.StopButton.TabIndex = 5;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Clock
            // 
            this.Clock.Interval = 50;
            this.Clock.Tick += new System.EventHandler(this.Clock_Tick);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(800, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.overlayToolStripMenuItem,
            this.loadBSPToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // overlayToolStripMenuItem
            // 
            this.overlayToolStripMenuItem.Name = "overlayToolStripMenuItem";
            this.overlayToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.overlayToolStripMenuItem.Text = "O&verlay...";
            this.overlayToolStripMenuItem.Click += new System.EventHandler(this.overlayToolStripMenuItem_Click);
            // 
            // loadBSPToolStripMenuItem
            // 
            this.loadBSPToolStripMenuItem.Name = "loadBSPToolStripMenuItem";
            this.loadBSPToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.loadBSPToolStripMenuItem.Text = "Load &BSP...";
            this.loadBSPToolStripMenuItem.Click += new System.EventHandler(this.loadBSPToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // OpenDem
            // 
            this.OpenDem.Filter = "Quake Demo files|*.dem";
            // 
            // Display
            // 
            this.Display.Bsp = null;
            this.Display.Demo = null;
            this.Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display.Location = new System.Drawing.Point(0, 24);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(800, 381);
            this.Display.TabIndex = 2;
            // 
            // OpenBsp
            // 
            this.OpenBsp.Filter = "Quake BSP files|*.bsp";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Display);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "QRoute3 v1.2";
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timeline)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.TextBox TimeLabel;
        private System.Windows.Forms.TrackBar Timeline;
        private DemoDisplay Display;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Timer Clock;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenDem;
        private System.Windows.Forms.ToolStripMenuItem overlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenBsp;
        private System.Windows.Forms.ToolStripMenuItem loadBSPToolStripMenuItem;
    }
}

