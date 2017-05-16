using PictureCompare.Script;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PictureCompare {
    public partial class Form1 : Form {
        public Form1 () {
            InitializeComponent();
        }

        public List<string> dupPic;

        private void button1_Click (object sender, EventArgs e) {

            string root = textBox1.Text != "" ? textBox1.Text : @"C:\Users\ChiaDe-Lin\Google 雲端硬碟\CV";
            dupPic = PictureScanner.ins.Scan(root);
            
            listBox1.Items.Clear();
            if (dupPic.Count > 0)
                listBox1.Items.Add("全部");

            for (int i = 0; i < dupPic.Count; i++) {
                listBox1.Items.Add(dupPic[i]);
            }
        }

        private void button2_Click (object sender, EventArgs e) {
            bool isAll = false;

            List<string> selectPaths = new List<string>();
            for (int i = 0; i < listBox1.SelectedItems.Count; i++) {
                string selectPath = listBox1.SelectedItems[i].ToString();
                if (selectPath == "全部") {
                    isAll = true;
                    break;
                }
                selectPaths.Add(selectPath);
            }

            if (isAll) {
                PictureScanner.ins.Remove(dupPic);
                listBox1.Items.Clear();
            }
            else {
                PictureScanner.ins.Remove(selectPaths);
                for (int i = 0; i < listBox1.SelectedItems.Count; i++) {
                    listBox1.Items.Remove(listBox1.SelectedItems[i]);
                }
            }
        }
    }
}
