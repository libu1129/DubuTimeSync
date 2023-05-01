using GuerrillaNtp;

using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DubuTimeSync
{
    public partial class Form1 : Form
    {
        bool close = false;
        IniManager ini = null;
        Setting setting = new Setting();
        bool disposed = false;
        LinkedList<string> logs = new();
        NtpTool tool = new NtpTool();

        public Form1()
        {
            InitializeComponent();
            load_setting();
            if (setting.hide_on_startup) this.Visible = false;
            this.textBox1.Select(0, 0);
            Task.Run(loop);
            tool.on_log += message =>
            {
                this.add_log(message);
            };
        }

        private void load_setting()
        {
            var ini_file = Path.Combine(Application.StartupPath, "setting.json");
            this.setting = new Setting();
            this.setting.load_default();
            if (File.Exists(ini_file))
            {
                this.setting = JObject.Parse(File.ReadAllText(ini_file)).ToObject<Setting>();
            }
            else
            {
                File.WriteAllText(ini_file, JObject.FromObject(this.setting).ToString(Formatting.Indented));
            }
        }

        private async Task loop()
        {
            while (!disposed)
            {
                try
                {
                    load_setting();
                    if (setting.ntp_addresses.Count() == 0) throw new Exception("ntp 주소가 0개");
                    await tool.sync(setting.ntp_addresses);
                }
                catch (Exception ex)
                {
                    add_log("error : " + ex.Message);
                }
                await Task.Delay(1000 * 60);
            }
        }

        private void add_log(string message)
        {
            logs.AddFirst($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {message}");
            if (logs.Count > 300) logs.RemoveLast();
            this.textBox1.Invoke(() =>
            {
                this.textBox1.Text = string.Join(Environment.NewLine, this.logs);
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close == false)
            {
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                disposed = true;
            }
        }

        private void 종료ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.close = true;
            this.Close();
        }

        private void 세팅리로드ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load_setting();
        }

        private void 보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }
    }
}