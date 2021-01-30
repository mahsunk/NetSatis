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

namespace NetSatis.BackOffice.Fiş
{
    public partial class FrmFis : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context=new NetSatisContext();
        FisDal fisDal=new FisDal();
        KasaHareketDal kasaHareketDal=new KasaHareketDal();
        StokHareketDal stokHareketDal=new StokHareketDal();
        public FrmFis()
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

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void FrmFis_Load(object sender, EventArgs e)
        {Listele();

        }

        public void Listele()
        {
            context=new NetSatisContext();
            gridcontFisler.DataSource=fisDal.GetAll(context);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string secilen = gridFisler.GetFocusedRowCellValue(colFisKodu).ToString();
                fisDal.Delete(context, c => c.FisKodu == secilen);
                kasaHareketDal.Delete(context, c => c.FisKodu == secilen);
                stokHareketDal.Delete(context, c => c.FisKodu == secilen);
                fisDal.Save(context);
                Listele();
            }
        }

        

        private void FisIslem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFisIslem form = new FrmFisIslem(null, e.Item.Caption);
            form.ShowDialog();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            string secilen = gridFisler.GetFocusedRowCellValue(colFisKodu).ToString();
            FrmFisIslem form=new FrmFisIslem(secilen,null);
            form.ShowDialog();
        }
    }
}