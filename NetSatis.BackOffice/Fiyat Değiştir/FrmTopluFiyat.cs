using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NetSatis.BackOffice.Stok;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.BackOffice.Fiyat_Değiştir
{
    public partial class FrmTopluFiyat : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context=new NetSatisContext();
        StokDal stokDal=new StokDal();
        public FrmTopluFiyat()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControl1.DataSource = context.Stoklar.Local.ToBindingList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmStokSec form=new FrmStokSec(true);
            form.ShowDialog();
            if (form.secildi)
            {
                foreach (var itemStok in form.secilen)
                {
                    stokDal.AddorUpdate(context, itemStok);
                }
            }
        }

        private void btnDegisiklikleriKaydet_Click(object sender, EventArgs e)
        {
            stokDal.Save(context);
        }

        private void btnCikar_Click(object sender, EventArgs e)
        {
            var secilen = gridView1.GetFocusedRowCellValue(colStokKodu).ToString();
            var result = stokDal.GetByFilter(context, c => c.StokKodu == secilen);
            context.Entry(result).State = EntityState.Detached;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowAutoFilterRow = true
                ? gridView1.OptionsView.ShowAutoFilterRow = false
                : gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFiyatDegistir_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount==0)
            {
                MessageBox.Show("Seçilen stok bulunamadı.");
                return;
            }
            FrmFiyatDegistir form=new FrmFiyatDegistir();
            form.ShowDialog();
            if (form.secildi)
            {
                foreach (var itemDegistir in form.list)
                {
                    if (itemDegistir.Degistir)
                    {
                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            decimal kolonDeger = 0;
                            decimal degisen = 0;
                            kolonDeger = Convert.ToDecimal(gridView1.GetRowCellValue(i, itemDegistir.KolonAdi));
                            if (itemDegistir.DegisimTuru=="Yüzde")
                            {
                                degisen = itemDegistir.DegisimYonu == "Arttır"
                                    ? kolonDeger + kolonDeger / 100 * itemDegistir.Degeri
                                    : kolonDeger - kolonDeger / 100 * itemDegistir.Degeri;
                            }
                            else
                            {
                                degisen = itemDegistir.DegisimYonu == "Arttır"
                                    ? kolonDeger +itemDegistir.Degeri
                                    : kolonDeger - itemDegistir.Degeri;
                            }
                            gridView1.SetRowCellValue(i,itemDegistir.KolonAdi,degisen);
                        }
                    }
                }
            }
        }
    }
}