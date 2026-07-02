/*
MIT License
Copyright (c) 2026 Sarayut Chaisuriya
... (License คงไว้ตามเดิม) ...
*/
using System;
using System.IO;
using System.Windows.Forms;

namespace FileProcessing
{
    public partial class frmTextView : Form
    {
        public frmTextView()
        {
            InitializeComponent();

            // หมายเหตุ: ลบโค้ด cbFileType.Items.Add ออกไปแล้ว 
            // เพราะเราใช้ช่อง textBox3 พิมพ์เอาเองแทนการใช้ Dropdown ครับ โปรแกรมจะได้ไม่ Error
        }

        private void btRead_Click(object sender, EventArgs e)
        {
            string content = File.ReadAllText(tbFileName.Text);
            rtbShow.Text = content;
        }

        private void btReadCSV_Click(object sender, EventArgs e)
        {
            // === เปลี่ยนมารับค่าจาก textBox1, textBox2, textBox3 ===
            int start = 1;
            int end = 500;
            string selectedType = "All";

            // รับค่าจากกล่อง Start
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                int.TryParse(textBox1.Text, out start);
            }

            // รับค่าจากกล่อง End
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                int.TryParse(textBox2.Text, out end);
            }

            // รับค่าจากกล่อง File
            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                selectedType = textBox3.Text.Trim();
            }
            // ========================================================

            // Validate range
            if (start < 1 || end < start)
            {
                MessageBox.Show("Invalid Range");
                return;
            }

            dgvData.Rows.Clear();
            dgvData.Columns.Clear();

            using (StreamReader srReader = new StreamReader(tbFileName.Text))
            {
                string strLine;
                bool bHeaderRead = false;
                int currentRow = 0;

                while ((strLine = srReader.ReadLine()) != null)
                {
                    string[] strHeaders_arr = null;

                    if (strLine.StartsWith("#"))
                    {
                        if (strLine.Length > 8 && strLine.Substring(0, 8).Equals("#HEADER"))
                        {
                            strHeaders_arr = strLine.Substring(8).Split(',');
                        }
                        continue;
                    }

                    string[] strValues_arr = strLine.Split(',');

                    if (!bHeaderRead)
                    {
                        foreach (string strHeader in strValues_arr)
                        {
                            if (strHeaders_arr == null)
                                dgvData.Columns.Add(strHeader.Trim(), strHeader.Trim());
                            else
                                dgvData.Columns.Add(strHeader.Trim(), strHeaders_arr[dgvData.Columns.Count].Trim());
                        }
                        bHeaderRead = true;
                    }
                    else
                    {
                        currentRow++;

                        // Skip rows before Start
                        if (currentRow < start)
                            continue;

                        // Stop reading after End
                        if (currentRow > end)
                            break;

                        // ---------- File Type Filter (ลอจิกเดิมเป๊ะ) ----------
                        if (selectedType.Equals("All", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(selectedType))
                        {
                            dgvData.Rows.Add(strValues_arr);
                        }
                        else
                        {
                            // file_type_guess อยู่คอลัมน์ที่ 7 (index 6) ป้องกันกรณีคอลัมน์ไม่ครบ
                            if (strValues_arr.Length > 6)
                            {
                                string fileType = strValues_arr[6].Trim().Trim('"');

                                if (fileType.Equals(selectedType, StringComparison.OrdinalIgnoreCase))
                                {
                                    dgvData.Rows.Add(strValues_arr);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbFileName.Text = ofd.FileName;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void FileType_Click(object sender, EventArgs e)
        {
        }
    }
}