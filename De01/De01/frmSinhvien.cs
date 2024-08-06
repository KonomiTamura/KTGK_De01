using De01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De01
{
    public partial class frmSinhvien : Form
    {
        public frmSinhvien()
        {
            InitializeComponent();
        }

        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            try
            {
                QLSVDBContext context = new QLSVDBContext();
                List<Lop> listLop = context.Lops.ToList();
                List<Sinhvien> listSinhvient = context.Sinhviens.ToList();
                LopComboBox(listLop);
                SinhVien(listSinhvient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnLuu.Enabled = false;
            btnKhong.Enabled = false;
        }

        private void LopComboBox(List<Lop> listLop)
        {
            this.cboLop.DataSource = listLop;
            this.cboLop.DisplayMember = "TenLop";
            this.cboLop.ValueMember = "MaLop";
        }

        private void SinhVien(List<Sinhvien> listSinhvien)
        {
            lvSinhvien.Items.Clear();
            foreach (var sinhvien in listSinhvien)
            {
                ListViewItem item = new ListViewItem(sinhvien.MaSV.ToString());
                item.SubItems.Add(sinhvien.HotenSV.ToString());
                item.SubItems.Add(string.Empty);
                item.SubItems.Add(sinhvien.Lop.TenLop.ToString());
                lvSinhvien.Items.Add(item);
            }
        }

        private void lvSinhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (lvSinhvien.SelectedItems.Count > 0)
                {
                  
                    ListViewItem selectedItem = lvSinhvien.SelectedItems[0];
                    txtMaSV.Text = selectedItem.SubItems[0].Text; 
                    txtHotenSV.Text = selectedItem.SubItems[1].Text;
                    DateTime Ngaysinh;
                        if (DateTime.TryParse(selectedItem.SubItems[2].Text, out Ngaysinh))
                        {
                            dtNgaysinh.Value = Ngaysinh;
                        }
                        else
                        {
                            dtNgaysinh.Value = DateTime.Today; 
                        }
                    string lop = selectedItem.SubItems[3].Text;
                    cboLop.SelectedItem = Ngaysinh;
                }
                else
                {
                    txtMaSV.Text = string.Empty;
                    txtHotenSV.Text = string.Empty;
                    cboLop.SelectedIndex = 0;
                    dtNgaysinh.Value = DateTime.Today;
                }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dong = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận đóng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dong == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvSinhvien.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo,MessageBoxIcon.Warning ); 
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem selectedItem in lvSinhvien.SelectedItems)
                    {
                        lvSinhvien.Items.Remove(selectedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHotenSV.Text) || string.IsNullOrWhiteSpace(cboLop.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ListViewItem item = new ListViewItem(txtMaSV.Text);
            item.SubItems.Add(txtHotenSV.Text); 
            item.SubItems.Add(dtNgaysinh.Value.ToShortDateString());
            item.SubItems.Add(cboLop.SelectedItem.ToString());
            lvSinhvien.Items.Add(item);

            txtMaSV.Clear();
            txtHotenSV.Clear();         
            dtNgaysinh.Value = DateTime.Now;
            cboLop.SelectedIndex = -1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvSinhvien.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvSinhvien.SelectedItems[0];
                selectedItem.SubItems[0].Text = txtMaSV.Text;
                selectedItem.SubItems[1].Text = txtHotenSV.Text;
                selectedItem.SubItems[2].Text = dtNgaysinh.Value.ToShortDateString();
                selectedItem.SubItems[3].Text = cboLop.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string searchQuery = txtTim.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                MessageBox.Show("Vui lòng nhập tên sinh viên cần tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<ListViewItem> matchedItems = new List<ListViewItem>();
            foreach (ListViewItem item in lvSinhvien.Items)
            {
                string tenSV = item.SubItems[1].Text.ToLower();

                if (tenSV.Contains(searchQuery))
                {
                    matchedItems.Add((ListViewItem)item.Clone()); 
                }
            }
            lvSinhvien.Items.Clear();
            lvSinhvien.Items.AddRange(matchedItems.ToArray());
        }

    }
}
