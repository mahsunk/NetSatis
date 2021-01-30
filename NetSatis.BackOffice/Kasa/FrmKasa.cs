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

namespace NetSatis.BackOffice.Kasa
{
    public partial class FrmKasa : DevExpress.XtraEditors.XtraForm
    {
        KasaDal kasaDal = new KasaDal();
        NetSatisContext context = new NetSatisContext();

        public FrmKasa()
        {
            InitializeComponent();
        }

        public void Guncelle()
        {
            gridcontKasalar.DataSource=kasaDal.KasaListele(context);
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string secilen = layoutView1.GetFocusedRowCellValue(colKasaKodu).ToString();
                kasaDal.Delete(context, c => c.YetkiliKodu == secilen);
                kasaDal.Save(context);
                Guncelle();
            }
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

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmKasaIslem frm = new FrmKasaIslem(new Entities.Tables.Kasa());
            frm.ShowDialog();
            if (frm.saved)
            {
                Guncelle();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Guncelle();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            string secilen = layoutView1.GetFocusedRowCellValue(colKasaKodu).ToString();
            FrmKasaIslem frm = new FrmKasaIslem(kasaDal.GetByFilter(context, c => c.YetkiliKodu == secilen));
            frm.ShowDialog();
            if (frm.saved)
            {
                Guncelle();
            }
        }
    }
}