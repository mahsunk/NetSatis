using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.BackOffice.Cari
{
    public partial class FrmCari : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context=new NetSatisContext();
        CariDal cariDal=new CariDal();
        public FrmCari()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            filterControl1.ApplyFilter();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            filterControl1.FilterString = null;
            filterControl1.ApplyFilter();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;

        }

        private void FrmCari_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        private void GetAll()
        {
            gridControl1.DataSource = cariDal.GetCariler(context);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            GetAll();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string secilen = gridView1.GetFocusedRowCellValue(colCariKodu).ToString();
                cariDal.Delete(context, c => c.CariKodu == secilen);
                cariDal.Save(context);
                GetAll();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmCariIslemler frm=new FrmCariIslemler(new Entities.Tables.Cari());
            frm.ShowDialog();
            if (frm.saved)
            {
            GetAll();
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            string secilen = gridView1.GetFocusedRowCellValue(colCariKodu).ToString();
            FrmCariIslemler frm = new FrmCariIslemler(cariDal.GetByFilter(context,c=>c.CariKodu==secilen));
            frm.ShowDialog();
            if (frm.saved)
            {
                GetAll();
            }

        }

        private void btnKopyala_Click(object sender, EventArgs e)
        {
            string secilen = gridView1.GetFocusedRowCellValue(colCariKodu).ToString();
            Entities.Tables.Cari cariEntity=new Entities.Tables.Cari();
            cariEntity = cariDal.GetByFilter(context, c => c.CariKodu == secilen);
            cariEntity.Id = -1;
            cariEntity.CariKodu = null;
            FrmCariIslemler frm=new FrmCariIslemler(cariEntity);
            frm.ShowDialog();
            if (frm.saved)
            {
                GetAll();
            }
        }

        private void btnCariHareket_Click(object sender, EventArgs e)
        {
            string secilen = gridView1.GetFocusedRowCellValue(colCariKodu).ToString();
            string secilenAd = gridView1.GetFocusedRowCellValue(colCariAdi).ToString();
            FrmCariHareket frm=new FrmCariHareket(secilen,secilenAd);
            frm.ShowDialog();
        }
    }
}