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

namespace NetSatis.BackOffice.Stok
{
    public partial class FrmStokHareket : DevExpress.XtraEditors.XtraForm
    {
        StokDal stokDal = new StokDal();
        StokHareketDal StokHareketDal=new StokHareketDal();
        NetSatisContext context=new NetSatisContext();
        private string _stokKodu;
        public FrmStokHareket(string stokKodu,string stokadi)
        {
            InitializeComponent();
            _stokKodu = stokKodu;
            lblBaslik.Text = _stokKodu + " - " + stokadi+"  "+ "Hareketleri";
        }

        private void FrmStokHareket_Load(object sender, EventArgs e)
        {
            Guncelle();
        }

        private void Guncelle()
        {
            gridcontStokHareket.DataSource = StokHareketDal.GetAll(context, c => c.StokKodu == _stokKodu);
            gridcontGenelStok.DataSource = StokHareketDal.GetGenelStok(context, _stokKodu);
            gridContDepoStok.DataSource = StokHareketDal.GetDepoStok(context, _stokKodu);
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Guncelle();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowAutoFilterRow == true)
            {
                gridView1.OptionsView.ShowAutoFilterRow = false;
            }
            else
            {
                gridView1.OptionsView.ShowAutoFilterRow = true;
            }
        }
    }
}