using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.BackOffice.Personel
{
    public partial class FrmPersonel : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context=new NetSatisContext();
        PersonelDal personelDal=new PersonelDal();
        public FrmPersonel()
        {
            InitializeComponent();
        }

        private void Listele()
        {
            gridcontPersonel.DataSource=personelDal.PersonelListesi(context);
        }
        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmPersonelIslem frm = new FrmPersonelIslem(new Entities.Tables.Personel());
            frm.ShowDialog();
            if (frm.saved)
            {
                Listele();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Listele();
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            string secilen = gridPersonel.GetFocusedRowCellValue(colPersonelKodu).ToString();
            if (MessageBox.Show("Seçili olan kaydı silmek istediğinize emin misiniz?","Uyarı",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                personelDal.Delete(context,c=>c.PersonelKodu== secilen);
                personelDal.Save(context);
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            string secilen = gridPersonel.GetFocusedRowCellValue(colPersonelKodu).ToString();
            FrmPersonelIslem frm = new FrmPersonelIslem(personelDal.GetByFilter(context, c => c.PersonelKodu == secilen));
            frm.ShowDialog();
            if (frm.saved)
            {
                Listele();
            }
        }

        private void btnPersonelHareket_Click(object sender, EventArgs e)
        {
            string secilen = gridPersonel.GetFocusedRowCellValue(colPersonelKodu).ToString();
            string secilenpersonelAdi = gridPersonel.GetFocusedRowCellValue(colPersonelAdi).ToString();
            FrmPersonelHareket form=new FrmPersonelHareket(secilen, secilenpersonelAdi);
        }
    }
}