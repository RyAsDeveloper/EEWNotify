using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEWNotify
{
    public partial class EEW_Window : Form
    {
        private bool DataNotOREEW_s;
        private string region_name_s;
        private string depth_s;
        private string shindo_s;
        private string magunitude_s;

        delegate bool ChangeDelegate();

        public bool ENDButton
        {
            get; set;
        }

        public bool DataNotOREEW
        {
            get { return DataNotOREEW_s; }
            set { if (value) {
                    Invoke(new Action(() => { DataNotOREEW_c.Text = "緊急地震速報は発令されていません"; }));
                    Invoke(new Action(() => { DataNotOREEW_c.ForeColor = Color.Black; }));
                }
                else if (!value) {
                    Invoke(new Action(() => { DataNotOREEW_c.Text = "緊急地震速報発令中！"; }));
                    Invoke(new Action(() => { DataNotOREEW_c.ForeColor = Color.Red; }));
                }
            }
        }

        public string region_name
        {
            get { return region_name_s; }
            set { 
                region_name_s = value;
                Invoke(new Action(() => { region_name_c.Text = region_name_s; }));
            }
        }

        public string depth
        {
            get { return depth_s; }
            set { 
                depth_s = value;
                Invoke(new Action(() => { depth_c.Text = depth_s; }));
            }
        }

        public string shindo
        {
            get { return shindo_s; }
            set
            {
                shindo_s = value;
                Invoke(new Action(() => { shindo_c.Text = shindo_s; }));
            }
        }

        public string magunitude
        {
            get { return magunitude_s; }
            set
            {
                magunitude_s = value;
                Invoke(new Action(() => { magnitude_c.Text = magunitude_s; }));
            }
        }

        public EEW_Window()
        {
            InitializeComponent();
            this.TopMost = true;

        }

        private void Window_Closing(object sender, FormClosingEventArgs e)
        {
            // 閉じる処理をキャンセルして非表示にする
            if (!ENDButton)
            {
                e.Cancel = true;
                this.Visible = false;
            } else
            {
                e.Cancel = false;
            }
        }

        private void Window_Load(object sender, EventArgs e)
        {
            int left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.DesktopBounds = new Rectangle(left, top, this.Width, this.Height);

            //最小化、最大化の無効
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // MaximumSizeとMinimumSizeを同じにすることでサイズ固定にする
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        // 未処理例外をキャッチするイベント・ハンドラ
        // （Windowsアプリケーション用）
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("致命的なエラーが発生しました。\n" + e.Exception, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
