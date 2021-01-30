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

namespace NetSatis.BackOffice.İndirim
{
    public partial class FrmIndirim : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context = new NetSatisContext();
        IndirimDal indirimDal = new IndirimDal();
        public FrmIndirim()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            
            gridcontIndirim.DataSource = indirimDal.IndirimListele(context);
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmIndirimIslem form = new FrmIndirimIslem();
            form.ShowDialog();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnFiyatDegistir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var secilen = gridIndirim.GetFocusedRowCellValue(colStokKodu).ToString();
                indirimDal.Delete(context, c => c.StokKodu == secilen);
                indirimDal.Save(context);
            }
        }

        private void btnDurumu_Click(object sender, EventArgs e)
        {
            var secilenStokKodu = gridIndirim.GetFocusedRowCellValue(colStokKodu).ToString();
            var secilen = indirimDal.GetByFilter(context, c => c.StokKodu == secilenStokKodu);
            if (Convert.ToBoolean(gridIndirim.GetFocusedRowCellValue(colDurumu)))
            {

                secilen.Durumu = false;
                gridIndirim.SetFocusedRowCellValue(colDurumu, false);
                btnDurumu.Text = "Pasif Yap";
                btnDurumu.ImageOptions.ImageIndex = 16;
                indirimDal.Save(context);
            }
            else
            {
                secilen.Durumu = true;
                gridIndirim.SetFocusedRowCellValue(colDurumu, true);
                btnDurumu.Text = "Aktif Yap";
                btnDurumu.ImageOptions.ImageIndex = 15;
                indirimDal.Save(context);
            }
        }

        private void gridIndirim_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Convert.ToBoolean(gridIndirim.GetFocusedRowCellValue(colDurumu)))
            {
                btnDurumu.Text = "Pasif Yap";
                btnDurumu.ImageOptions.ImageIndex = 16;
            }
            else
            {
                btnDurumu.Text = "Aktif Yap";
                btnDurumu.ImageOptions.ImageIndex = 15;
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            gridIndirim.OptionsView.ShowAutoFilterRow = true
                ? gridIndirim.OptionsView.ShowAutoFilterRow == false
                : gridIndirim.OptionsView.ShowAutoFilterRow == true;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}