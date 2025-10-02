using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab03_02
{
    public partial class FormMain : Form
    {
        private string currentFile = "";
        private bool isSaved = false;

        public FormMain()
        {
            InitializeComponent();
            this.Text = "Soạn thảo văn bản";
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Load fonts vào combobox
            foreach (FontFamily font in FontFamily.Families)
            {
                cboFont.Items.Add(font.Name);
            }
            cboFont.SelectedItem = "Tahoma";

            // Load size vào combobox
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in sizes)
            {
                cboSize.Items.Add(size);
            }
            cboSize.SelectedItem = 14;

            rtbEditor.SelectionFont = new Font("Tahoma", 14);
        }

        // New
        private void btnNew_Click(object sender, EventArgs e)
        {
            // Xóa toàn bộ nội dung
            rtbEditor.Clear();

            // Reset lại font mặc định
            rtbEditor.Font = new Font("Tahoma", 14);
            rtbEditor.ForeColor = Color.Black;   // màu mặc định

            // Nếu bạn có ComboBox font và size thì reset lại luôn
            cboFont.Text = "Tahoma";
            cboSize.Text = "14";

            // Đặt cờ trạng thái file (nếu bạn có quản lý file đã lưu/chưa lưu)
            isSaved = false;
        }

        // Open
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.EndsWith(".rtf"))
                    rtbEditor.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                else
                    rtbEditor.Text = File.ReadAllText(ofd.FileName);

                currentFile = ofd.FileName;
                isSaved = true;
            }
        }

        // Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentFile == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Rich Text Format (*.rtf)|*.rtf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    rtbEditor.SaveFile(sfd.FileName);
                    currentFile = sfd.FileName;
                    isSaved = true;
                }
            }
            else
            {
                rtbEditor.SaveFile(currentFile);
                isSaved = true;
            }
        }

        // Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Bold
        private void btnBold_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Bold);
        }

        // Italic
        private void btnItalic_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Italic);
        }

        // Underline
        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Underline);
        }

        private void ChangeFontStyle(FontStyle style)
        {
            if (rtbEditor.SelectionFont != null)
            {
                FontStyle newStyle = rtbEditor.SelectionFont.Style ^ style; // toggle
                rtbEditor.SelectionFont = new Font(rtbEditor.SelectionFont, newStyle);
            }
        }

        // Chọn font từ combobox
        private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbEditor.SelectionFont != null)
            {
                rtbEditor.SelectionFont = new Font(cboFont.SelectedItem.ToString(),
                    rtbEditor.SelectionFont.Size,
                    rtbEditor.SelectionFont.Style);
            }
        }

        // Chọn size từ combobox
        private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbEditor.SelectionFont != null)
            {
                rtbEditor.SelectionFont = new Font(rtbEditor.SelectionFont.FontFamily,
                    float.Parse(cboSize.SelectedItem.ToString()),
                    rtbEditor.SelectionFont.Style);
            }
        }

       

        // FontDialog
        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowColor = true;
            fd.Color = rtbEditor.SelectionColor;
            fd.Font = rtbEditor.SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                rtbEditor.SelectionFont = fd.Font;
                rtbEditor.SelectionColor = fd.Color;
            }
        }

        
    }
}