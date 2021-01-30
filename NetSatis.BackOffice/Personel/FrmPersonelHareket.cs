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

namespace NetSatis.BackOffice.Personel
{
    public partial class FrmPersonelHareket : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context=new NetSatisContext();
        PersonelDal personelDal=new PersonelDal();
        FisDal fisDal=new FisDal();
        private string _personelKodu;
        public FrmPersonelHareket(string personelKodu,string personelAdi)
        {
            InitializeComponent();
            _personelKodu = personelKodu;
            lblBaslik.Text = personelAdi + " - " + personelAdi;

        }

        private void FrmPersonelHareket_Load(object sender, EventArgs e)
        {

        }

        private void Listele()
        {

            gridcontPersonelHareket.DataSource = fisDal.GetAll(context, c => c.PlasiyerKodu == _personelKodu);
            gridcontFisToplam.DataSource = personelDal.PersonelFisToplam(context, _personelKodu);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            gridPersonelHareket.OptionsView.ShowAutoFilterRow = true ? gridPersonelHareket.OptionsView.ShowAutoFilterRow ==false : gridPersonelHareket.OptionsView.ShowAutoFilterRow ==true;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}