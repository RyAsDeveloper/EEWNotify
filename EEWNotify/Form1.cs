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
                if (data["��������"] != null && data["�P���t���O"] == "false" || data["�P���t���O"] == null) //���񂪂Ȃ��ꍇ�́u�f�[�^������܂���v�ɂȂ�̂ŁA�����ł͂Ȃ��ꍇ�{�P���t���O�������Ă��Ȃ��ꍇ
                {
                    notify.DataNotOREEW = false;
                    notify.region_name = data["�k��"];
                    notify.depth = data["�k���[��"];
                    notify.shindo = data["�ő�\�z�k�x"];
                    notify.magunitude = data["�}�O�j�`���[�h"];

                    notify.Visible = true;
                }
                else if (data["��������"] == null)
                {
                    notify.DataNotOREEW = true;
                }
            } else if (data == null)
            {
                MessageBox.Show("�G���[���������܂����B�T�[�o�[�ɃA�N�Z�X�ł��܂���B\n�C���^�[�l�b�g�A�N�Z�X���Ȃ��Ȃǂ̖�肪�������Ă���\��������܂��B",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        bool Internet_Access;
        private Dictionary<string,string> EEW_get()
        {
            DateTime dt = new DateTime(); // �T���v������
            dt = DateTime.Now.AddSeconds(-5);

            string time = dt.ToString("yyyyMMddHHmmss"); // 2018/07/18 12:35:40 �������� 20180718123540 �ɂȂ�

            var url = $"http://www.kmoni.bosai.go.jp/webservice/hypo/eew/{time}.json";
            var client = new HttpClient(); // HttpClient ���C���X�^���X��

            string json = "";
            try
            {
                json = client.GetStringAsync(url).Result; // GET ���N�G�X�g������ JSON ���擾����
                Internet_Access = true;
            } catch (Exception ex)
            {
                Internet_Access=false;
                return null;
            }

            Dictionary<string, string?> map = new Dictionary<string, string?>();

            EEW eew = new EEW();

            
            
            JsonConvert.DeserializeObject<EEW>(json);




            //map.Add("�n�k�����t���O", eew.result.message);
            map.Add("�擾����", eew.report_time);
            map.Add("�k��", eew.region_name);
            map.Add("�ܓx", eew.latitude);
            map.Add("�o�x", eew.longitude);
            map.Add("�k���[��", eew.depth);
            map.Add("�ő�\�z�k�x", eew.calcintensity);
            map.Add("�P���t���O", eew.is_training.ToString());
            map.Add("��������", eew.origin_time);
            map.Add("�}�O�j�`���[�h", eew.magunitude);

            return map;

            /*
            var �擾���� = eew.report_time;
            var �k�� = eew.region_name;
            var �ܓx = eew.latitude;
            var �o�x = eew.longitude;
            var �L�����Z���t���O = eew.is_cancel;
            var �[�� = eew.depth;
            var �ő�\�z�k�x = eew.calcintensity;
            var �ŏI��t���O = eew.is_final;
            var �P���t���O = eew.is_training;
            var �������� = eew.origin_time;
            var �}�O�j�`���[�h = eew.magunitude;
            var �d���ԍ� = eew.report_num;
            var �d��ID = eew.report_id;
            var �x��t���O = eew.alertflg;
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
            icon.Text = "�ً}�n�k����ʒm�V�X�e��";
            ContextMenuStrip menu = new ContextMenuStrip();


            ToolStripMenuItem NotifyWindowItem = new ToolStripMenuItem();
            NotifyWindowItem.Text = "&�J��";
            NotifyWindowItem.Click += new EventHandler(NotifyWindow_Open);
            menu.Items.Add(NotifyWindowItem);

            ToolStripMenuItem CloseItem = new ToolStripMenuItem();
            CloseItem.Text = "&�I��";
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