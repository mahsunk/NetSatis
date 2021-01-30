using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UI;
using NetSatis.Reports.Stok;

namespace NetSatis.BackOffice.Raporlar
{
    public partial class FrmRaporListesi : DevExpress.XtraEditors.XtraForm
    {
        private XtraReport rapor; 
        public FrmRaporListesi()
        {
            InitializeComponent();
        }

       
        private void navBarLink_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            filterControl1.FilterString = null;
            var buton = sender as NavBarItem;

            Type tip = Assembly.Load("NetSatis.Reports").GetTypes()
                .SingleOrDefault(c => c.Name == buton.Name.Replace("link", ""));

             rapor = (XtraReport)Activator.CreateInstance(tip);

             txtRaporAdi.Text = e.Link.Caption;
             txtRaporGrubu.Text = e.Link.Group.Caption;
             txtAciklama.Text = buton.Tag == null ?txtAciklama.Text=null:buton.Tag.ToString();
             filterControl1.SourceControl = rapor.DataSource;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FrmRaporGoruntule form=new FrmRaporGoruntule(rapor);
            rapor.FilterString = filterControl1.FilterString;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}