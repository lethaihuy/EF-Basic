using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFGUI
{
    public partial class formMain : Form
    {
        SinhVienEntities db = new SinhVienEntities();
        List<Khoa> listKhoa = new List<Khoa>();
        string isCheck = "";
        public formMain()
        {
            InitializeComponent();
            Load();       
            //AddBinding();
            LoadListKhoa();
            Khoa();
        }

        //fun
        void LoadListKhoa()
        {
            foreach (var item in db.Khoas.Where(k => k.MaKhoa != null).ToList())
            {
                listKhoa.Add(item);
            }
            cbMaKhoa.DataSource = listKhoa;
            cbMaKhoa.DisplayMember = "MaKhoa";
        }

        void Load()
        {
            var data = from sv in db.DMSinhViens select new { MaSV = sv.MaSV, HoTen = sv.HoTenSV, GioiTinh = sv.Phai, NgaySinh = sv.NgaySinh, NoiSinh = sv.NoiSinh, MaKhoa = sv.Khoa.MaKhoa };
            dgvData.DataSource = data.ToList();

            this.dgvData.Columns[0].HeaderText = "Mã SV";
            this.dgvData.Columns[1].HeaderText = "Họ và tên";
            this.dgvData.Columns[2].HeaderText = "Giới tính ";
            this.dgvData.Columns[3].HeaderText = "Ngày Sinh";
            this.dgvData.Columns[4].HeaderText = "Nơi sinh";
            this.dgvData.Columns[5].HeaderText = "Mã khoa";
        }

        void AddBinding()
        {
            txtMaSV.DataBindings.Add(new Binding("Text", dgvData.DataSource, "MaSV"));
            txtHoTen.DataBindings.Add(new Binding("Text", dgvData.DataSource, "HoTenSV"));
            cbGioiTinh.DataBindings.Add(new Binding("text", dgvData.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never));
            dtpNgaySinh.DataBindings.Add(new Binding("Text", dgvData.DataSource, "NgaySinh"));
            txtNoiSinh.DataBindings.Add(new Binding("Text", dgvData.DataSource, "NoiSinh"));
            cbMaKhoa.DataBindings.Add(new Binding("Text", dgvData.DataSource, "MaKhoa"));

        }

        void AddSV()
        {
            try
            {
                DMSinhVien sv = new DMSinhVien();
                sv.MaSV = txtMaSV.Text;
                sv.HoTenSV = txtHoTen.Text;
                sv.Phai = cbGioiTinh.Text;
                sv.NgaySinh = dtpNgaySinh.Value;
                sv.NoiSinh = txtNoiSinh.Text;
                sv.MaKhoa = cbMaKhoa.Text;


                db.DMSinhViens.Add(sv);
                db.SaveChanges();
                Load();
            }
            catch (Exception)
            {
                throw;
            }

        }
        void DeleteSV()
        {
            string maSV = this.dgvData.CurrentRow.Cells[0].Value.ToString();
            DMSinhVien sv = db.DMSinhViens.Single(v => v.MaSV.Equals(maSV));
            db.DMSinhViens.Remove(sv);
            db.SaveChanges();
        }

        void EditSV()
        {          
            string maSV = this.dgvData.SelectedCells[0].OwningRow.Cells["MaSV"].Value.ToString();
            DMSinhVien sv = db.DMSinhViens.Find(maSV);
            sv.HoTenSV = txtHoTen.Text;
            sv.Phai = cbGioiTinh.Text;
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.NoiSinh = txtNoiSinh.Text;
            sv.MaKhoa = cbMaKhoa.Text;
            db.SaveChanges();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.isCheck = "save";
            MoKhoa();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maSV = this.dgvData.SelectedCells[0].OwningRow.Cells["MaSV"].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa "+maSV+" ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                DeleteSV();
                Load();
            }
          
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Khoa();
            string maSV = this.dgvData.CurrentRow.Cells[0].Value.ToString();
            DMSinhVien sv = db.DMSinhViens.Single(v => v.MaSV.Equals(maSV));
            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoTenSV;
            cbGioiTinh.Text = sv.Phai;
            dtpNgaySinh.Value = sv.NgaySinh;
            txtNoiSinh.Text = sv.NoiSinh;
            cbMaKhoa.Text = sv.MaKhoa;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.isCheck = "edit";
            MoKhoa();
        }

        void Khoa()
        {
            txtMaSV.ReadOnly = false;
            txtHoTen.ReadOnly = true;
            cbGioiTinh.Enabled = false;
            dtpNgaySinh.Enabled = false;
            txtNoiSinh.ReadOnly = true;
            cbMaKhoa.Enabled = false;
        }

        void MoKhoa()
        {
            btnSave.Enabled = true;

            txtMaSV.ReadOnly = true;
            txtHoTen.ReadOnly = false;
            cbGioiTinh.Enabled = true;
            dtpNgaySinh.Enabled = true;
            txtNoiSinh.ReadOnly = false;
            cbMaKhoa.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Khoa();
            txtMaSV.ResetText();
            txtHoTen.ResetText();
            cbGioiTinh.ResetText();
            dtpNgaySinh.ResetText();
            txtNoiSinh.ResetText();
            cbMaKhoa.ResetText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(isCheck == "save")
            {
                this.AddSV();
            }
            if(isCheck == "edit")
            {
                this.EditSV();
            }
            this.Load();
            Khoa();
        }
    }
}