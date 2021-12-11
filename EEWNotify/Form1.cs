using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Reflection;

namespace EEWNotify
{
    public partial class Form1 : Form
    {
        EEW_Window notify = new EEW_Window();

        public Form1()
        {
            InitializeComponent();

            this.ShowInTaskbar = false;
            this.setComponents();

            IntPtr dummy = notify.Handle;
            notify.ENDButton = false;

            Task task = Task.Run(() => {
                while (true)
                {
                    Thread.Sleep(1000);
                    EEW_Analyze();
                }

            });
        }

        private void EEW_Analyze()
        {
            Dictionary<string, string> data = EEW_get();

            if (data != null)
            {
                if (data["発生時刻"] != null && data["訓練フラグ"] == "false" || data["訓練フラグ"] == null) //速報がない場合は「データがありません」になるので、そうではない場合＋訓練フラグが立っていない場合
                {
                    notify.DataNotOREEW = false;
                    notify.region_name = data["震源"];
                    notify.depth = data["震源深さ"];
                    notify.shindo = data["最大予想震度"];
                    notify.magunitude = data["マグニチュード"];

                    notify.Visible = true;
                }
                else if (data["発生時刻"] == null)
                {
                    notify.DataNotOREEW = true;
                }
            } else if (data == null)
            {
                MessageBox.Show("エラーが発生しました。サーバーにアクセスできません。\nインターネットアクセスがないなどの問題が発生している可能性があります。",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        bool Internet_Access;
        private Dictionary<string,string> EEW_get()
        {
            DateTime dt = new DateTime(); // サンプル時刻
            dt = DateTime.Now.AddSeconds(-5);

            string time = dt.ToString("yyyyMMddHHmmss"); // 2018/07/18 12:35:40 だったら 20180718123540 になる

            var url = $"http://www.kmoni.bosai.go.jp/webservice/hypo/eew/{time}.json";
            var client = new HttpClient(); // HttpClient をインスタンス化

            string json = "";
            try
            {
                json = client.GetStringAsync(url).Result; // GET リクエストをして JSON を取得する
                Internet_Access = true;
            } catch (Exception ex)
            {
                Internet_Access=false;
                return null;
            }

            Dictionary<string, string?> map = new Dictionary<string, string?>();

            EEW eew = new EEW();

            
            
            JsonConvert.DeserializeObject<EEW>(json);




            //map.Add("地震発生フラグ", eew.result.message);
            map.Add("取得時刻", eew.report_time);
            map.Add("震源", eew.region_name);
            map.Add("緯度", eew.latitude);
            map.Add("経度", eew.longitude);
            map.Add("震源深さ", eew.depth);
            map.Add("最大予想震度", eew.calcintensity);
            map.Add("訓練フラグ", eew.is_training.ToString());
            map.Add("発生時刻", eew.origin_time);
            map.Add("マグニチュード", eew.magunitude);

            return map;

            /*
            var 取得時刻 = eew.report_time;
            var 震源 = eew.region_name;
            var 緯度 = eew.latitude;
            var 経度 = eew.longitude;
            var キャンセルフラグ = eew.is_cancel;
            var 深さ = eew.depth;
            var 最大予想震度 = eew.calcintensity;
            var 最終報フラグ = eew.is_final;
            var 訓練フラグ = eew.is_training;
            var 発生時刻 = eew.origin_time;
            var マグニチュード = eew.magunitude;
            var 電文番号 = eew.report_num;
            var 電文ID = eew.report_id;
            var 警報フラグ = eew.alertflg;
            */
        }

        private void NotifyWindow_Open(object sender, EventArgs e)
        {
            notify.Visible = true;


        }

        private void Close_Click(object sender, EventArgs e)
        {
            notify.ENDButton = true;
            Application.Exit();
        }

        private void setComponents()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly().ManifestModule.Assembly;
            NotifyIcon icon = new NotifyIcon();
            using (Stream s = myAssembly.GetManifestResourceStream("EEWNotify.TasktrayIcon.ico"))
            {
                icon.Icon = new Icon(s);

                s.Close();
            }
            icon.Visible = true;
            icon.Text = "緊急地震速報通知システム";
            ContextMenuStrip menu = new ContextMenuStrip();


            ToolStripMenuItem NotifyWindowItem = new ToolStripMenuItem();
            NotifyWindowItem.Text = "&開く";
            NotifyWindowItem.Click += new EventHandler(NotifyWindow_Open);
            menu.Items.Add(NotifyWindowItem);

            ToolStripMenuItem CloseItem = new ToolStripMenuItem();
            CloseItem.Text = "&終了";
            CloseItem.Click += new EventHandler(Close_Click);
            menu.Items.Add(CloseItem);


            icon.ContextMenuStrip = menu;

            icon.MouseDoubleClick += Icon_MouseDoubleClick;
        }

        private void Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notify.Visible = true;
        }
    }



    public class EEW
    {
        public Result result { get; set; }
        public string report_time { get; set; }
        public string region_code { get; set; }
        public string request_time { get; set; }
        public string region_name { get; set; }
        public string longitude { get; set; }
        public bool? is_cancel { get; set; }
        public string depth { get; set; }
        public string calcintensity { get; set; }
        public bool? is_final { get; set; }
        public bool? is_training { get; set; }
        public string latitude { get; set; }
        public string origin_time { get; set; }
        public Security security { get; set; }
        public string magunitude { get; set; }
        public string report_num { get; set; }
        public string request_hypo_type { get; set; }
        public string report_id { get; set; }
        public string alertflg { get; set; }
    }

    public class Result
    {
        public string status { get; set; }
        public string message { get; set; }
        public bool is_auth { get; set; }
    }

    public class Security
    {
        public string realm { get; set; }
        public string hash { get; set; }
    }
}