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
using NetSatis.Entities.Tables;

namespace NetSatis.BackOffice.Stok
{
    public partial class FrmStok : DevExpress.XtraEditors.XtraForm
    {
        public FrmStok()
        {
            InitializeComponent();
        }

        private string secilen;

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmStok_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        public void GetAll()
        {
            NetSatisContext context=new NetSatisContext();
            StokDal stokDal=new StokDal();
           gridControl1.DataSource= stokDal.StokListele(context);
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
            
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

        private void btnAra_Click(object sender, EventArgs e)
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            GetAll();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            NetSatisContext context=new NetSatisContext();
            StokDal stokDal=new StokDal();
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                  secilen = gridView1.GetFocusedRowCellValue(colStokKodu).ToString();
                stokDal.Delete(context,c=>c.StokKodu==secilen);
                stokDal.Save(context);
                GetAll();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmStokIslem    form=new FrmStokIslem(new Entities.Tables.Stok());
            form.ShowDialog();
            if (form.saved)
            {
                GetAll();
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {NetSatisContext context= new  NetSatisContext();
            StokDal stokDal=new StokDal();
             secilen = gridView1.GetFocusedRowCellValue(colStokKodu).ToString();
            FrmStokIslem form = new FrmStokIslem(stokDal.GetByFilter(context,c=>c.StokKodu==secilen));
            form.ShowDialog();
            if (form.saved)
            {
                GetAll();
            }
        }

        private void btnKopyala_Click(object sender, EventArgs e)
        {
            NetSatisContext context= new  NetSatisContext();
            StokDal stokDal=new StokDal();
              secilen = gridView1.GetFocusedRowCellValue(colStokKodu).ToString();
            Entities.Tables.Stok stokEntity=new Entities.Tables.Stok();
            stokEntity = stokDal.GetByFilter(context, c => c.StokKodu == secilen);
            stokEntity.Id = -1;
            stokEntity.StokKodu = null;
            FrmStokIslem form = new FrmStokIslem(stokEntity);
            form.ShowDialog();
            if (form.saved)
            {
                GetAll();
            }

        }

        private void btnStokHareket_Click(object sender, EventArgs e)
        {
              secilen = gridView1.GetFocusedRowCellValue(colStokKodu).ToString();
             string secilenAd = gridView1.GetFocusedRowCellValue(colStokAdi).ToString();

            FrmStokHareket form =new FrmStokHareket(secilen, secilenAd);
            form.ShowDialog();
        }
    }
}