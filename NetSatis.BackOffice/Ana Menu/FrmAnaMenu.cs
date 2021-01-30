using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSatis.BackOffice.Ajanda;
using NetSatis.BackOffice.Ana_Menu;
using NetSatis.BackOffice.Cari;
using NetSatis.BackOffice.Fiş;
using NetSatis.BackOffice.Fiyat_Değiştir;
using NetSatis.BackOffice.İndirim;
using NetSatis.BackOffice.KasaHareketleri;
using NetSatis.BackOffice.Personel;
using NetSatis.BackOffice.Raporlar;
using NetSatis.BackOffice.Stok;
using NetSatis.BackOffice.Stok_Hareketleri;
using NetSatis.BackOffice.Tanim;
using Netsatis.Backup;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;
using NetSatis.Entities.Tables;
using NetSatis.Reports.Fatura_ve_Fis;
using NetSatis.Reports.Stok;

namespace NetSatis.BackOffice
{
    public partial class Form : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form()
        {
            InitializeComponent();
            using (var context = new NetSatisContext())
            {
                context.Database.CreateIfNotExists();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmStok frm = new FrmStok();
            frm.MdiParent = this;
            frm.Show();
        }



        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCari frm = new FrmCari();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFis frm = new FrmFis();
            frm.MdiParent = this;
            frm.Show();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FrmAnaMenuBilgi frm = new FrmAnaMenuBilgi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmStokHareketleri frm = new FrmStokHareketleri();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmKasaHareketleri frm = new FrmKasaHareketleri();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPersonel frm = new FrmPersonel();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rptBilgiFisi report = new rptBilgiFisi("3");
            FrmRaporGoruntule form = new FrmRaporGoruntule(report);
            form.WindowState = FormWindowState.Maximized;
            form.ShowDialog();
        }
        private void FisIslem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFisIslem form = new FrmFisIslem(null, e.Item.Caption);
            form.ShowDialog();

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTopluFiyat form = new FrmTopluFiyat();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmIndirim form = new FrmIndirim();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRaporListesi form = new FrmRaporListesi();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRaporDuzenle form = new FrmRaporDuzenle();
            form.WindowState = FormWindowState.Maximized;
            form.ShowDialog();
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAjanda form=new FrmAjanda();
            form.WindowState = FormWindowState.Maximized;
            form.ShowDialog();
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBackup form=new FrmBackup();
            form.ShowDialog();
        }
    }
}
