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
using NetSatis.BackOffice.Tanim;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.BackOffice.Cari
{
    public partial class FrmCariIslemler : DevExpress.XtraEditors.XtraForm
    {
        private Entities.Tables.Cari _entity;
        CariDal caridal = new CariDal();
        NetSatisContext context = new NetSatisContext();
        public bool saved = false;
        public FrmCariIslemler(Entities.Tables.Cari entity)
        {
            InitializeComponent();
            _entity = entity;
            toggleDurum.DataBindings.Add("EditValue", _entity, "Durumu");
            txtCariKodu.DataBindings.Add("Text", _entity, "CariKodu");
            txtCariAdi.DataBindings.Add("Text", _entity, "CariAdi");
            cmbCariTuru.DataBindings.Add("Text", _entity, "CariTuru");
            txtYetkiliKisi.DataBindings.Add("Text", _entity, "YetkiliKisi");
            txtFaturaUnvan.DataBindings.Add("Text", _entity, "FaturaUnvani");
            txtVergiDairesi.DataBindings.Add("Text", _entity, "VergiDairesi");
            txtVergiNo.DataBindings.Add("Text", _entity, "VergiNo");

            txtAciklama.DataBindings.Add("Text", _entity, "Aciklama");
            txtCepTelefon.DataBindings.Add("Text", _entity, "CepTelefonu");
            txtSabitTel.DataBindings.Add("Text", _entity, "Telefon");
            txtFax.DataBindings.Add("Text", _entity, "Fax");
            txtEmail.DataBindings.Add("Text", _entity, "Email");
            txtWeb.DataBindings.Add("Text", _entity, "Web");
            txtIl.DataBindings.Add("Text", _entity, "Il");
            txtIlce.DataBindings.Add("Text", _entity, "Ilce");
            txtSemt.DataBindings.Add("Text", _entity, "Semt");
            txtAdres.DataBindings.Add("Text", _entity, "Adres");
            btnCariGrubu.DataBindings.Add("Text", _entity, "CariGrubu");
            btnCariAltGrubu.DataBindings.Add("Text", _entity, "CariAltGrubu");
            btnOzelKod1.DataBindings.Add("Text", _entity, "OzelKod1");
            btnOzelKod2.DataBindings.Add("Text", _entity, "OzelKod2");
            btnOzelKod3.DataBindings.Add("Text", _entity, "OzelKod3");
            btnOzelKod4.DataBindings.Add("Text", _entity, "OzelKod4");
            cbmAlisOzelFiyat.DataBindings.Add("Text", _entity, "AlisOzelFiyat");
            cbmSatisOzelFiyat.DataBindings.Add("Text", _entity, "SatisOzelFiyat");



            calcIskontoOrani.DataBindings.Add("Text", _entity, "IskontoOrani");
            calcIskontoOrani.DataBindings[0].FormattingEnabled = true;
            calcIskontoOrani.DataBindings[0].FormatString = "'%'0";
            calcIskontoOrani.DataBindings[0].DataSourceNullValue = "0";


            calcRiskLimiti.DataBindings.Add("Text", _entity, "RiskLimiti");
            calcRiskLimiti.DataBindings[0].FormattingEnabled = true;
            calcRiskLimiti.DataBindings[0].FormatString = "C2";
            calcRiskLimiti.DataBindings[0].DataSourceNullValue = "0";

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (caridal.AddorUpdate(context, _entity))
            {
                caridal.Save(context);
                saved = true;
                this.Close();
            }
        }

        private void FrmCariIslemler_Load(object sender, EventArgs e)
        {

        }

        private void btnCariGrubu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    FrmTanim form = new FrmTanim(FrmTanim.TanimTuru.CariGrubu);
                    form.ShowDialog();
                    if (form.secildi)
                    {
                        btnCariGrubu.Text = form._entity.Tanimi;
                    }

                    break;
                case 1:
                    btnCariGrubu.Text = null;
                    break;

            }
        }

        private void btnOzelKod1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    FrmTanim form = new FrmTanim(FrmTanim.TanimTuru.CariOzelKod1);
                    form.ShowDialog();
                    if (form.secildi)
                    {
                        btnOzelKod1.Text = form._entity.Tanimi;
                    }

                    break;
                case 1:
                    btnCariGrubu.Text = null;
                    break;

            }
        }

        private void btnOzelKod3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    FrmTanim form = new FrmTanim(FrmTanim.TanimTuru.CariOzelKod3);
                    form.ShowDialog();
                    if (form.secildi)
                    {
                        btnOzelKod3.Text = form._entity.Tanimi;
                    }

                    break;
                case 1:
                    btnCariGrubu.Text = null;
                    break;

            }
        }

        private void btnOzelKod2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    FrmTanim form = new FrmTanim(FrmTanim.TanimTuru.CariOzelKod2);
                    form.ShowDialog();
                    if (form.secildi)
                    {
                        btnOzelKod2.Text = form._entity.Tanimi;
                    }

                    break;
                case 1:
                    btnCariGrubu.Text = null;
                    break;

            }
        }

        private void btnOzelKod4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    FrmTanim form = new FrmTanim(FrmTanim.TanimTuru.CariOzelKod4);
                    form.ShowDialog();
                    if (form.secildi)
                    {
                        btnOzelKod4.Text = form._entity.Tanimi;
                    }

                    break;
                case 1:
                    btnCariGrubu.Text = null;
                    break;

            }
        }

        private void btnCariAltGrubu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    FrmTanim form = new FrmTanim(FrmTanim.TanimTuru.CariAltGrubu);
                    form.ShowDialog();
                    if (form.secildi)
                    {
                        btnCariAltGrubu.Text = form._entity.Tanimi;
                    }

                    break;
                case 1:
                    btnCariGrubu.Text = null;
                    break;

            }
        }
    }
}