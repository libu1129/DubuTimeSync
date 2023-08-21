namespace DubuTimeSync
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
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            보기ToolStripMenuItem = new ToolStripMenuItem();
            종료ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            파일ToolStripMenuItem = new ToolStripMenuItem();
            세팅리로드ToolStripMenuItem = new ToolStripMenuItem();
            종료ToolStripMenuItem1 = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(15, 36);
            textBox1.Margin = new Padding(4, 4, 4, 4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(871, 355);
            textBox1.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "DubuTimeSync";
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += 보기ToolStripMenuItem_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 보기ToolStripMenuItem, 종료ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(109, 52);
            // 
            // 보기ToolStripMenuItem
            // 
            보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
            보기ToolStripMenuItem.Size = new Size(108, 24);
            보기ToolStripMenuItem.Text = "보기";
            보기ToolStripMenuItem.Click += 보기ToolStripMenuItem_Click;
            // 
            // 종료ToolStripMenuItem
            // 
            종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            종료ToolStripMenuItem.Size = new Size(108, 24);
            종료ToolStripMenuItem.Text = "종료";
            종료ToolStripMenuItem.Click += 종료ToolStripMenuItem1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 파일ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 3, 0, 3);
            menuStrip1.Size = new Size(903, 30);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            파일ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 세팅리로드ToolStripMenuItem, 종료ToolStripMenuItem1 });
            파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            파일ToolStripMenuItem.Size = new Size(53, 24);
            파일ToolStripMenuItem.Text = "파일";
            // 
            // 세팅리로드ToolStripMenuItem
            // 
            세팅리로드ToolStripMenuItem.Name = "세팅리로드ToolStripMenuItem";
            세팅리로드ToolStripMenuItem.Size = new Size(172, 26);
            세팅리로드ToolStripMenuItem.Text = "세팅 리로드";
            세팅리로드ToolStripMenuItem.Click += 세팅리로드ToolStripMenuItem_Click;
            // 
            // 종료ToolStripMenuItem1
            // 
            종료ToolStripMenuItem1.Name = "종료ToolStripMenuItem1";
            종료ToolStripMenuItem1.Size = new Size(172, 26);
            종료ToolStripMenuItem1.Text = "종료";
            종료ToolStripMenuItem1.Click += 종료ToolStripMenuItem1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(903, 408);
            Controls.Add(menuStrip1);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DubuTimeSync";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 보기ToolStripMenuItem;
        private ToolStripMenuItem 종료ToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 파일ToolStripMenuItem;
        private ToolStripMenuItem 종료ToolStripMenuItem1;
        private ToolStripMenuItem 세팅리로드ToolStripMenuItem;
    }
}