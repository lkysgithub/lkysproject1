using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 音乐播放器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //取消播放器自动播放功能
            musicPlayer.settings.autoStart = false;
            musicPlayer.URL = @"C:\Users\Administrator\Desktop\music\双笙、封茗囧菌 - 霜雪千年.mp3";
            label1.Image = Image.FromFile(@"C:\Users\Administrator\Desktop\图片\放音.png");
        
        }

        //存储音乐文件的全路径
        List<string> ListPath = new List<string>();

        bool b = true;
        private void btnPlayorPause_Click(object sender, EventArgs e)
        {
            if (btnPlayorPause.Text == "播放")
            {
                if(b)
                {
                    musicPlayer.URL = ListPath[listBox1.SelectedIndex];
                }
                musicPlayer.Ctlcontrols.play();
                btnPlayorPause.Text = "暂停";

            }
            else if(btnPlayorPause.Text == "暂停")
            {
                musicPlayer.Ctlcontrols.pause();
                btnPlayorPause.Text = "播放";
                b = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\Administrator\Desktop\music";
            ofd.Filter = "音乐文件|*.wav|Mp3文件|*.mp3|所有文件|*.*";
            ofd.Multiselect = true;
            ofd.Title = "请选择音乐文件！";
            ofd.ShowDialog();

            //获得在文本框中选择文件的全路径
            string[] path = ofd.FileNames;

            for (int i = 0; i < path.Length; i++)
            {
                ListPath.Add(path[i]);

                listBox1.Items.Add(Path.GetFileName(path[i]));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("请首先选择音乐文件！");
                return;
            }
            try
            {
                musicPlayer.URL = ListPath[listBox1.SelectedIndex];
                musicPlayer.Ctlcontrols.play();
                btnPlayorPause.Text = "暂停";
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //获得当前索引
            int index = listBox1.SelectedIndex;
            //清空所有选中项的索引
            listBox1.SelectedIndices.Clear();
            index++;
            if (index == listBox1.Items.Count)
            {
                index = 0;
            }
            //将改变后的索引重新赋给蓝条
            listBox1.SelectedIndex = index;
            musicPlayer.URL = ListPath[index];
            musicPlayer.Ctlcontrols.play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //获得当前索引
            int index = listBox1.SelectedIndex;
            //清空所有选中项的索引
            listBox1.SelectedIndices.Clear();
            index--;
            if (index < 0)
            {
                index = listBox1.Items.Count-1;
            }
            //将改变后的索引重新赋给蓝条
            listBox1.SelectedIndex = index;
            musicPlayer.URL = ListPath[index];
            musicPlayer.Ctlcontrols.play();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = listBox1.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                //先删集合
                ListPath.RemoveAt(listBox1.SelectedIndex);
                //再删列表
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (label1.Tag.ToString() == "1")
            {
                //tag作一个标记作用，默认值为1
                //目的：让你静音
                musicPlayer.settings.mute = true;
                //显示静音的图片
                label1.Image = Image.FromFile(@"C:\Users\Administrator\Desktop\图片\静音.png");
                label1.Tag = "2";

            }
            else if (label1.Tag.ToString() == "2")
            {
                musicPlayer.settings.mute = false;
                //显示放音的图片
                label1.Image = Image.FromFile(@"C:\Users\Administrator\Desktop\图片\放音.png");
                label1.Tag = "1";

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            musicPlayer.settings.volume -= 5;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            musicPlayer.settings.volume += 5;
        }
    }
}
