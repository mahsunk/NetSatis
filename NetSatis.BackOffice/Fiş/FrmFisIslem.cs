using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using NetSatis.BackOffice.Cari;
using NetSatis.BackOffice.Depo;
using NetSatis.BackOffice.Kasa;
using NetSatis.BackOffice.Stok;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;
using NetSatis.Entities.Tables;
using NetSatis.Entities.Tables.Other_Tables;

namespace NetSatis.BackOffice.Fiş
{
    public partial class FrmFisIslem : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context = new NetSatisContext();
        FisDal fisDal = new FisDal();
        StokHareketDal stokHareketDal = new StokHareketDal();
        KasaHareketDal kasaHareketDal = new KasaHareketDal();
        CariDal cariDal = new CariDal();
        FisAyarlari ayarlar = new FisAyarlari();

        Fis _fisentity = new Fis();
        private CariBakiye entityBakiye = new CariBakiye();
        public FrmFisIslem(string fisKodu = null, string fisTuru = null)
        {
            InitializeComponent();
            if (fisKodu != null)
            {
                _fisentity = context.Fisler.Where(c => c.FisKodu == fisKodu).SingleOrDefault();
                context.StokHareketleri.Where(c => c.FisKodu == fisKodu).Load();
                context.KasaHareketleri.Where(c => c.FisKodu == fisKodu).Load();

                toggleBakiyeTuru.IsOn =
                    context.KasaHareketleri.Count(c => c.FisKodu == fisKodu && c.Hareket == "Kasa Giriş") == 0;

                if (_fisentity.CariKodu != null)
                {
                    entityBakiye = cariDal.CariBakiyesi(context, _fisentity.CariKodu);
                    lblAlacak.Text = entityBakiye.Alacak.ToString("C2");
                    lblBorc.Text = entityBakiye.Borc.ToString("C2");
                    lblBakiye.Text = entityBakiye.Bakiye.ToString("C2");
                }



            }
            else
            {
                _fisentity.FisTuru = fisTuru;
                _fisentity.Tarih = DateTime.Now;
            }
            txtFisKodu.DataBindings.Add("Text", _fisentity, "FisKodu", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFisTuru.DataBindings.Add("Text", _fisentity, "FisTuru", false, DataSourceUpdateMode.OnPropertyChanged);
            cbmTarih.DataBindings.Add("EditValue", _fisentity, "Tarih", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBelgeNo.DataBindings.Add("Text", _fisentity, "BelgeNo", false, DataSourceUpdateMode.OnPropertyChanged);
            txtAciklama.DataBindings.Add("Text", _fisentity, "Aciklama", false, DataSourceUpdateMode.OnPropertyChanged);
            lblCariKodu.DataBindings.Add("Text", _fisentity, "CariKodu", false, DataSourceUpdateMode.OnPropertyChanged);
            lblCariAdi.DataBindings.Add("Text", _fisentity, "CariAdi", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFaturaUnvani.DataBindings.Add("Text", _fisentity, "FaturaUnvani", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCepTelefon.DataBindings.Add("Text", _fisentity, "CepTelefonu", false, DataSourceUpdateMode.OnPropertyChanged);
            txtIl.DataBindings.Add("Text", _fisentity, "Il", false, DataSourceUpdateMode.OnPropertyChanged);
            txtIlce.DataBindings.Add("Text", _fisentity, "Ilce", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSemt.DataBindings.Add("Text", _fisentity, "Semt", false, DataSourceUpdateMode.OnPropertyChanged);
            txtAdres.DataBindings.Add("Text", _fisentity, "Adres", false, DataSourceUpdateMode.OnPropertyChanged);
            txtVergiDairesi.DataBindings.Add("Text", _fisentity, "VergiDairesi", false, DataSourceUpdateMode.OnPropertyChanged);
            txtVergiNo.DataBindings.Add("Text", _fisentity, "VergiNo", false, DataSourceUpdateMode.OnPropertyChanged);


            gridcontStokHareket.DataSource = context.StokHareketleri.Local.ToBindingList();
            gridcontKasaHareket.DataSource = context.KasaHareketleri.Local.ToBindingList();

            FisAyar();
            Toplamlar();
            OdenenTutarGuncelle();

            ButonlariYukle();
        }

        private void ButonlariYukle()
        {
            foreach (var item in context.OdemeTurleri.ToList())
            {
                var buton = new SimpleButton
                {
                    Name = item.OdemeTuruKodu,
                    Text = item.OdemeTuruAdi,
                    Height = 41,
                    Width = 94
                };
                buton.Click += OdemeEkleClick;
                flowOdemeTurleri.Controls.Add(buton);
            }

            var PersonelSecimIptal = new CheckButton()
            {
                Name = "Yok",
                Text = "Yok",
                GroupIndex = 1,
                Height = 41,
                Width = 94,
                Checked = _fisentity.PlasiyerKodu==null
            };
            PersonelSecimIptal.Click += PersonelYukle_Click;
            flowPersonel.Controls.Add(PersonelSecimIptal);

            foreach (var item in context.Personeller.ToList())
            {
                var buton = new CheckButton()
                {
                    Name = item.PersonelKodu,
                    Text = item.PersonelAdi,
                    GroupIndex = 1,
                    Height = 41,
                    Width = 94,
                    Checked = item.PersonelKodu==_fisentity.PlasiyerKodu
                };
                buton.Click += PersonelYukle_Click;
                flowPersonel.Controls.Add(buton);
            }
        }

        private void PersonelYukle_Click(object sender, EventArgs e)
        {
            var button = sender as CheckButton;
            if (button.Name=="Yok")
            {
                _fisentity.PlasiyerKodu = null;
                _fisentity.PlasiyerAdi = null;
            }
            else
            {
                _fisentity.PlasiyerKodu = button.Name;
                _fisentity.PlasiyerAdi = button.Text;
            }
           
        }



        private void OdemeEkleClick(object sender, EventArgs e)
        {
            var buton = (sender as SimpleButton);

            if (ayarlar.SatisEkrani == false)
            {
                FrmOdemeEkrani form = new FrmOdemeEkrani(buton.Text, buton.Name);
                form.ShowDialog();
                if (form.entity != null)
                {
                    kasaHareketDal.AddorUpdate(context, form.entity);
                    OdenenTutarGuncelle();
                }

            }
            else
            {
                if (calcOdemesiGereken.Value <= 0)
                {
                    MessageBox.Show("Ödenmesi gereken tutar zaten ödenmiş durumdadır.");
                }
                else
                {


                    KasaHareket entityKasaHareket = new KasaHareket
                    {
                        OdemeTuruKodu = buton.Name,
                        OdemeTuruAdi = buton.Text,
                        Tutar = calcOdemesiGereken.Value
                    };
                    kasaHareketDal.AddorUpdate(context, entityKasaHareket);
                    OdenenTutarGuncelle();
                }
            }






        }

        private void OdenenTutarGuncelle()
        {
            gridKasaHareket.UpdateSummary();
            if (ayarlar.SatisEkrani)
            {
                calcOdenenTutar.Value = Convert.ToDecimal(colTutar.SummaryItem.SummaryValue);
                calcOdemesiGereken.Value = calcGenelToplam.Value - calcOdenenTutar.Value;
            }
            else
            {
                calcGenelToplam.Value= Convert.ToDecimal(colTutar.SummaryItem.SummaryValue);
            }

        }

        private void FisAyar()
        {

            switch (_fisentity.FisTuru)
            {
                case "Alış Faturası":
                    ayarlar.StokHareketi = "Stok Giriş";
                    ayarlar.KasaHareketi = "Kasa Çıkış";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = true;
                    lblBaslik.ImageOptions.ImageIndex = 0;

                    break;
                case "Perakende Satış Faturası":
                    ayarlar.StokHareketi = "Stok Çıkış";
                    ayarlar.KasaHareketi = "Kasa Giriş";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = true;
                    lblBaslik.ImageOptions.ImageIndex = 1;

                    break;
                case "Toptan Satış Faturası":
                    ayarlar.StokHareketi = "Stok Çıkış";
                    ayarlar.KasaHareketi = "Kasa Giriş";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = true;
                    lblBaslik.ImageOptions.ImageIndex = 2;

                    break;

                case "Alış İade Faturası":
                    ayarlar.StokHareketi = "Stok Çıkış";
                    ayarlar.KasaHareketi = "Kasa Giriş";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = true;
                    lblBaslik.ImageOptions.ImageIndex = 3;

                    break;
                case "Satış İade Faturası":
                    ayarlar.StokHareketi = "Stok Giriş";
                    ayarlar.KasaHareketi = "Kasa Çıkış";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = true;
                    lblBaslik.ImageOptions.ImageIndex = 4;
                    break;

                case "Sayım Fazlası Fişi":
                    ayarlar.StokHareketi = "Stok Çıkış";
                    lblBaslik.ImageOptions.ImageIndex = 5;
                    ayarlar.OdemeEkrani = false;
                    ayarlar.SatisEkrani = true;
                    panelOdeme.Visible = false;
                    navOdemeEkrani.Dispose();
                    navCariBilgi.Dispose();

                    break;
                case "Sayım Eksiği Fişi":
                    ayarlar.StokHareketi = "Stok Çıkış";
                    lblBaslik.ImageOptions.ImageIndex = 6;
                    ayarlar.OdemeEkrani = false;
                    ayarlar.SatisEkrani = true;
                    panelOdeme.Visible = false;
                    navOdemeEkrani.Dispose();
                    navCariBilgi.Dispose();
                    break;
                case "Stok Devir Fişi":
                    ayarlar.StokHareketi = "Stok Giriş";
                    lblBaslik.ImageOptions.ImageIndex = 7;
                    ayarlar.OdemeEkrani = false;
                    ayarlar.SatisEkrani = true;
                    panelOdeme.Visible = false;
                    navOdemeEkrani.Dispose();
                    navCariBilgi.Dispose();
                    break;

                case "Tahsilat Fişi":
                    ayarlar.KasaHareketi = "Kasa Giriş";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = false;
                    navSatisEkrani.Dispose();
                    panelOdeme.Visible = false;
                    panelIskonto.Visible = false;
                    panelKDV.Visible = false;
                    grpToplamlar.Height = 150;
                    panelToplam.Top = 30;
                    navigationPane2.SelectedPage = navOdemeEkrani;
                    break;

                case "Ödeme Fişi":
                    ayarlar.KasaHareketi = "Kasa Çıkış";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = false;
                    navSatisEkrani.Dispose();
                    panelOdeme.Visible = false;
                    panelIskonto.Visible = false;
                    panelKDV.Visible = false;
                    grpToplamlar.Height = 150;
                    panelToplam.Top = 30;
                    navigationPane2.SelectedPage = navOdemeEkrani;
                    break;

                case "Cari Devir Fişi":
                    ayarlar.KasaHareketi = "Kasa Giriş";
                    ayarlar.OdemeEkrani = true;
                    ayarlar.SatisEkrani = false;
                    navSatisEkrani.Dispose();
                    panelOdeme.Visible = false;
                    panelIskonto.Visible = false;
                    panelKDV.Visible = false;
                    panelCariDevir.Visible = true;
                    txtFisKodu.Width = 311;
                    grpToplamlar.Height = 150;
                    panelToplam.Top = 30; ;
                    navigationPane2.SelectedPage = navOdemeEkrani;
                    break;
            }
        }

        private void FrmFisIslem_Load(object sender, EventArgs e)
        {

        }

        private StokHareket StokSec(Entities.Tables.Stok entity)
        {
            IndirimDal indirimDal=new IndirimDal();;
            StokHareket stokHareket = new StokHareket();
            stokHareket.StokKodu = entity.StokKodu;
            stokHareket.StokAdi = entity.StokAdi;
            stokHareket.Barkod = entity.Barkod;
            stokHareket.BarkodTuru = entity.BarkodTuru;
            stokHareket.BirimFiyati = txtFisTuru.Text == "Alış Faturası" ? entity.AlisFiyati1 : entity.SatisFiyati1;
            stokHareket.Birimi = entity.Birimi;
            stokHareket.Miktar = txtMiktar.Value;
            stokHareket.Tarih=DateTime.Now;
            stokHareket.IndirimOrani = indirimDal.StokIndirimi(context, entity.StokKodu);
            stokHareket.Kdv = entity.SatisKdv;
            
            return stokHareket;
        }

        private void btnStokSec_Click(object sender, EventArgs e)
        {
            FrmStokSec form = new FrmStokSec();
            form.ShowDialog();
            if (form.secildi)
            {
                stokHareketDal.AddorUpdate(context, StokSec(form.secilen.First()));
                Toplamlar();
            }
        }

        private void txtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Entities.Tables.Stok entity;
                entity = context.Stoklar.Where(c => c.Barkod == txtBarkod.Text).SingleOrDefault();
                if (entity != null)
                {
                    stokHareketDal.AddorUpdate(context, StokSec(entity));
                    Toplamlar();
                }
                else
                {
                    MessageBox.Show("Aradığınız barkod numarası ürünler arasında bulunamadı.");
                }

                txtBarkod.Text = null;
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            frmCariSec form = new frmCariSec();

            form.ShowDialog();
            if (form.secildi)
            {

                Entities.Tables.Cari entity = form.secilen.FirstOrDefault();
                entityBakiye = cariDal.CariBakiyesi(context, entity.CariKodu);

                lblCariKodu.Text = entity.CariKodu;
                lblCariAdi.Text = entity.CariAdi;
                txtFaturaUnvani.Text = entity.FaturaUnvani;
                txtVergiDairesi.Text = entity.VergiDairesi;
                txtVergiNo.Text = entity.VergiNo;
                txtCepTelefon.Text = entity.CepTelefonu;
                txtIl.Text = entity.Il;
                txtIlce.Text = entity.Ilce;
                txtSemt.Text = entity.Semt;
                txtAdres.Text = entity.Adres;
                lblAlacak.Text = entityBakiye.Alacak.ToString("C2");
                lblBorc.Text = entityBakiye.Borc.ToString("C2");
                lblBakiye.Text = entityBakiye.Bakiye.ToString("C2");

            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            if (gridStokHareket.RowCount != 0)
            {
                if (MessageBox.Show("Satış Ekranına eklenmiş ürünler var. Bu işlemi iptal edip formu kapatmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btnCariTemizle_Click(object sender, EventArgs e)
        {
            lblCariKodu.Text = null;
            lblCariAdi.Text = null;
            txtFaturaUnvani.Text = null;
            txtVergiDairesi.Text = null;
            txtVergiNo.Text = null;
            txtCepTelefon.Text = null;
            txtIl.Text = null;
            txtIlce.Text = null;
            txtSemt.Text = null;
            txtAdres.Text = null;
            lblAlacak.Text = "Görüntülenemiyor";
            lblBorc.Text = "Görüntülenemiyor";
            lblBakiye.Text = "Görüntülenemiyor";
        }

        private void gridStokHareket_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            Toplamlar();


        }

        private void Toplamlar()
        {
            gridStokHareket.UpdateSummary();
            calcIskontoTutar.Value = Convert.ToDecimal(colToplamTutar.SummaryItem.SummaryValue) / 100 * calcIskontoOrani.Value;
            calcGenelToplam.Value = Convert.ToDecimal(colToplamTutar.SummaryItem.SummaryValue) - calcIskontoTutar.Value;
            calcKdvToplam.Value = Convert.ToDecimal(colKdvToplam.SummaryItem.SummaryValue);
            calcIndirimToplam.Value = Convert.ToDecimal(colIndirimTutar.SummaryItem.SummaryValue);
            calcOdemesiGereken.Value = calcGenelToplam.Value - calcOdenenTutar.Value;

        }

        private void calcGenelToplam_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void calcIskontoOrani_Validated(object sender, EventArgs e)
        {
            Toplamlar();
        }

        private void repoDepo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmDepoSec form = new FrmDepoSec(gridStokHareket.GetFocusedRowCellValue(colStokKodu).ToString());
            form.ShowDialog();
            if (form.secildi)
            {
                gridStokHareket.SetFocusedRowCellValue(colDepoKodu, form.entity.DepoKodu);
                gridStokHareket.SetFocusedRowCellValue(colDepoAdi, form.entity.DepoAdi);
            }
        }

        private void repoBirimFiyat_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string fiyatSecilen = gridStokHareket.GetFocusedRowCellValue(colStokKodu).ToString();
            Entities.Tables.Stok fiyatEntity = context.Stoklar.Where(c => c.StokKodu == fiyatSecilen).SingleOrDefault();

            barFiyat1.Tag = txtFisTuru.Text == "Alış Faturası" ? fiyatEntity.AlisFiyati1 ?? 0 : fiyatEntity.SatisFiyati1 ?? 0;
            barFiyat2.Tag = txtFisTuru.Text == "Alış Faturası" ? fiyatEntity.AlisFiyati2 ?? 0 : fiyatEntity.SatisFiyati2 ?? 0;
            barFiyat3.Tag = txtFisTuru.Text == "Alış Faturası" ? fiyatEntity.AlisFiyati3 ?? 0 : fiyatEntity.SatisFiyati3 ?? 0;


            barFiyat1.Caption = string.Format("{0:C2}", barFiyat1.Tag);
            barFiyat2.Caption = string.Format("{0:C2}", barFiyat2.Tag);
            barFiyat3.Caption = string.Format("{0:C2}", barFiyat3.Tag);

            radialFiyat.ShowPopup(MousePosition);
        }

        private void FiyatSec(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridStokHareket.SetFocusedRowCellValue(colBirimFiyati, Convert.ToDecimal(e.Item.Tag));

        }

        private void repoSeriNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string veri = Convert.ToString(gridStokHareket.GetFocusedRowCellValue(colSeriNo));
            FrmSeriNoGir form = new FrmSeriNoGir(veri);
            form.ShowDialog();
            gridStokHareket.SetFocusedRowCellValue(colSeriNo, form.veriSeriNo);
        }

        private void repoSil_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gridStokHareket.DeleteSelectedRows();
                Toplamlar();
            }
        }

        private void repoKasaAdi_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmKasaSec form = new FrmKasaSec();
            form.ShowDialog();
            if (form.secildi)
            {
                gridKasaHareket.SetFocusedRowCellValue(colKasaAdi, form.entity.KasaKodu);
                {
                    gridKasaHareket.SetFocusedRowCellValue(colKasaAdi, form.entity.KasaAdi);
                    gridKasaHareket.SetFocusedRowCellValue(colKasaKodu, form.entity.KasaKodu);

                }
            }
        }

        private void repoKHSil_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("Seçili olan veriyi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gridKasaHareket.DeleteSelectedRows();
                OdenenTutarGuncelle();

            }

        }

        private void btnSatisBitir_Click(object sender, EventArgs e)
        {
            if (toggleBakiyeTuru.IsOn)
            {
                ayarlar.KasaHareketi = "Kasa Çıkış"; 
            }
            else
            {
                ayarlar.KasaHareketi = "Kasa Giriş";
            }
            int StokHata = context.StokHareketleri.Local.Where(c => c.DepoKodu == null).Count();
            int KasaHata = context.KasaHareketleri.Local.Where(c => c.KasaKodu == null).Count();

            string message = null;
            int hata = 0;

            if (gridStokHareket.RowCount == 0&& ayarlar.SatisEkrani==true)
            {
                message += "Satış Ekranında eklenmiş ürün bulunamadı." + System.Environment.NewLine;
                hata++;
            }

            if (_fisentity.CariKodu==null && ayarlar.SatisEkrani==false)
            {
                message +=txtFisTuru.Text +" türünde cari seçimi zorunludur."+ System.Environment.NewLine;
                hata++;

            }

            if (gridKasaHareket.RowCount == 0 && ayarlar.SatisEkrani == false)
            {
                message += "Herhangi bir ödeme bulunamadı." + System.Environment.NewLine;
                hata++;

            }
            if (txtFisKodu.Text == "")
            {
                message += "Fiş kodu alanı boş geçilemez." + System.Environment.NewLine;
                hata++;
            }
            if (calcOdemesiGereken.Value != 0 && ayarlar.OdemeEkrani == true)
            {
                message += "Ödenmesi gereken tutar ödenmemiş." + System.Environment.NewLine;
                hata++;

            }

            if (StokHata != 0)
            {
                message += "Satış ekranındaki ürünlerin Depo seçimlerinde eksiklikler var." + System.Environment.NewLine;
                hata++;
            }
            if (KasaHata != 0)
            {
                message += "Ödeme ekranındaki ödemelerin Kasa seçimlerinde eksiklikler var.";
                hata++;
            }

            if (hata != 0)
            {
                MessageBox.Show(message,"Uyarı");
                return;
            }


            foreach (var stokVeri in context.StokHareketleri.Local.ToList())
            {
                stokVeri.Tarih = stokVeri.Tarih == null
                    ? Convert.ToDateTime(cbmTarih.DateTime)
                    : Convert.ToDateTime(stokVeri.Tarih);
                stokVeri.FisKodu = txtFisKodu.Text;
                stokVeri.Hareket = ayarlar.StokHareketi;
            }

            if (ayarlar.OdemeEkrani)
            {
                foreach (var kasaVeri in context.KasaHareketleri.Local.ToList())
                {
                    kasaVeri.Tarih = kasaVeri.Tarih == null
                        ? Convert.ToDateTime(cbmTarih.DateTime)
                        : Convert.ToDateTime(kasaVeri.Tarih);
                    kasaVeri.FisKodu = txtFisKodu.Text;
                    kasaVeri.Hareket =  ayarlar.KasaHareketi;
                    kasaVeri.CariKodu = lblCariKodu.Text;
                    kasaVeri.CariAdi = lblCariAdi.Text;
                }
            }



            _fisentity.ToplamTutar = calcGenelToplam.Value;
            _fisentity.IskontoOrani = calcIskontoOrani.Value;
            _fisentity.IskontoTutar = calcIskontoTutar.Value;

            fisDal.AddorUpdate(context, _fisentity);
            context.SaveChanges();
            this.Close();



        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
           
        }
    }
}