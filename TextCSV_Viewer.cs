/*
MIT License

Copyright (c) 2026 Sarayut Chaisuriya

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Note on dataset:
The included MalwareBazaar sample CSV has been modified:
- Limited to first 500 rows
- Header format adjusted for teaching purposes
See README.md for full details.
*/
using System;
using System.IO;
using System.Windows.Forms;

namespace FileProcessing
{
	public partial class frmTextView : Form
	{
		/// <summary>
		/// Initializes a new instance of the frmTextView class.
		/// </summary>
		public frmTextView()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Handles the Click event of the Read button by loading the contents of the specified file into the display area.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void btRead_Click(object sender, EventArgs e)
		{			
            string content = File.ReadAllText(tbFileName.Text);
            rtbShow.Text = content;
		}
        /// <summary>
        /// Handles the Click event of the btReadCSV button, reading CSV data from the specified file and populating the
        /// DataGridView with its contents.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btReadCSV_Click(object sender, EventArgs e)
        {
            // === 🎯 ตั้งค่า m, n และ Filter ตรงนี้สำหรับการทดสอบ (Part A) ===
            int m = 10;             // โหลดตั้งแต่บรรทัดที่
            int n = 20;            // ถึงบรรทัดที่
            string filter = ""; // นามสกุลไฟล์ที่ต้องการ (ใส่ "" ถ้าไม่ต้องการกรอง)
            // ========================================================

            // เคลียร์ข้อมูลเก่าออกตารางก่อนโหลดใหม่
            dgvData.Columns.Clear();
            dgvData.Rows.Clear();

            using (StreamReader srReader = new StreamReader(tbFileName.Text))
            {
                string strLine;
                bool bHeaderRead = false;
                int currentRow = 0; // ตัวนับบรรทัดข้อมูล (ไม่รวม Header)

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
                        currentRow++; // เริ่มนับแถวข้อมูล

                        // 1. ดัก Error Case: ถ้า n น้อยกว่า m ให้แจ้งเตือนและหยุดทำงาน
                        if (n < m)
                        {
                            MessageBox.Show("Error: ค่า n ต้องมากกว่าหรือเท่ากับ m", "Error Test Case", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        // 2. Partial Loading (ข้าม): ถ้ายูนิตข้อมูลยังไม่ถึงบรรทัดที่ m ให้ข้ามไป
                        if (currentRow < m)
                        {
                            continue;
                        }

                        // 3. Partial Loading (หยุด): ถ้าโหลดเกินบรรทัดที่ n ให้หยุดลูปทันที (โปรแกรมจะได้ไม่ค้าง)
                        if (currentRow > n)
                        {
                            break;
                        }

                        // 4. Filtering: คัดกรองประเภทไฟล์ (ตรวจสอบว่ามีคำที่หาในบรรทัดนั้นไหม)
                        if (!string.IsNullOrEmpty(filter))
                        {
                            // ถ้าบรรทัดนี้ ไม่มีคำที่ตั้งเป็น filter ไว้ ให้ข้ามไป
                            if (strLine.IndexOf(filter, StringComparison.OrdinalIgnoreCase) < 0)
                            {
                                continue;
                            }
                        }

                        // 5. ถ้าผ่านเงื่อนไขทั้งหมด ค่อยเพิ่มข้อมูลลงตาราง
                        dgvData.Rows.Add(strValues_arr);
                    }
                }
            }
        }
        /// <summary>
        /// Handles the Click event of the Browse button, allowing the user to select a file and displaying its path in the
        /// file name text box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
	}   // End of frmTextView class
}
