using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace alarm3 {
    public partial class Form1 : Form {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        private SoundPlayer Player = new SoundPlayer();

        public Form1() {
            InitializeComponent();
            
            System.DateTime.Now.ToString("yyyy");
            DateTime.Now.ToString("yyyy-MM-dd-HH-mm");


            //날짜 추가-1
            for (int m = 1; m < 13; m++) {
                if (m < 10) {
                    comboBox1.Items.Add("0" + m + "월");
                } else comboBox1.Items.Add(m + "월");
            }

            //날짜 추가-1
            for (int d = 1; d < 31; d++) {
                if (d < 10) {
                    comboBox2.Items.Add("0" + d + "일");
                } else comboBox2.Items.Add(d + "일");
            }

            //시간 추가-1
            for (int h = 0; h < 24; h++) {
                if (h < 10) {
                    comboBox3.Items.Add("0" + h + "시");
                } else comboBox3.Items.Add(h + "시");
            }

            //시간추가-2
            for (int m = 0; m < 60; m++) {
                if (m < 10) {
                    comboBox4.Items.Add("0" + m+ "분");
                } else comboBox4.Items.Add(m + "분");
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            timer1_Tick(sender, e);
            timer1.Interval = 1000; 
            timer1.Start(); 
        }
        private void timer1_Tick(object sender, EventArgs e) {
            string now = DateTime.Now.ToString("yyyy" + "년" + "MM" + "월" + "dd" + "일" + "HH" + "시" + "mm" + "분" + "ss" + "초");
            foreach (System.Diagnostics.Process procName in System.Diagnostics.Process.GetProcesses()) {
                label1.Text = "현재시간은 " + now + "입니다";
            }
            string b = "2016년" + comboBox1.Text + comboBox2.Text + comboBox3.Text + comboBox4.Text + "00초";
            string filename = label4.Text;
            if ( now == b) {

                Form2 Nyang = new Form2();
                Nyang.Show();
                mciSendString(("open \"" + filename + "\" type mpegvideo alias MediaFile"), null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);
                //try {
                //    this.Player.SoundLocation = @"filename";
                //    this.Player.PlayLooping();
                //} catch (Exception w) {
                //}

            }
            if (comboBox1.Text == "01월" || comboBox1.Text == "03월" || comboBox1.Text == "05월" || comboBox1.Text == "07월" || comboBox1.Text == "09월" || comboBox1.Text == "11월") {
                if (comboBox2.Items.Count == 30) {
                    comboBox2.Items.Add("31일");
                }
                if (comboBox2.Items.Count == 28) {
                    comboBox2.Items.Add("29일");
                    comboBox2.Items.Add("30일");
                    comboBox2.Items.Add("31일");
                }
            } else comboBox2.Items.Remove("31일");

            if (comboBox1.Text == "02월") {
                if (comboBox2.Items.Count == 30) {
                    comboBox2.Items.Remove("29일");
                    comboBox2.Items.Remove("30일");
                }
                if (comboBox2.Items.Count == 31) {
                    comboBox2.Items.Remove("29일");
                    comboBox2.Items.Remove("30일");
                    comboBox2.Items.Remove("31일");
                }
            }
            if (comboBox1.Text != "") {
                comboBox2.Visible = true;
            }
            if (comboBox2.Text != "") {
                comboBox3.Visible = true;
            }
            if (comboBox3.Text != "") {
                comboBox4.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) {

            string b = comboBox1.Text + comboBox2.Text + comboBox3.Text + comboBox4.Text;
            label2.Text = "알람시간은 " + b + "입니다."; 
        }

        private void button2_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
            label4.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e) {
            mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
            this.Player.Stop();


        }

        private void button4_Click(object sender, EventArgs e) {
            mciSendString("play MediaFile", null, 0, IntPtr.Zero);
        }

        private void button5_Click(object sender, EventArgs e) {
            string filename = label4.Text;
            mciSendString(("open \"" + filename + "\" type mpegvideo alias MediaFile"), null, 0, IntPtr.Zero);
            mciSendString("play MediaFile", null, 0, IntPtr.Zero);
        }

    }
}
