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
using DevExpress.XtraEditors.Frames;
using NetSatis.BackOffice.Tanim;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.BackOffice.Personel
{
    public partial class FrmPersonelIslem : DevExpress.XtraEditors.XtraForm
    {
        private Entities.Tables.Personel _entity;
        PersonelDal personelDal=new PersonelDal();
        NetSatisContext contex=new NetSatisContext();
        public bool saved = false;
        public FrmPersonelIslem(Entities.Tables.Personel entity)
        {
            InitializeComponent();
            _entity = entity;
            toggleCalisiyor.DataBindings.Add("EditValue", _entity, "Calisiyor", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtPersonelKodu.DataBindings.Add("Text", _entity, "PersonelKodu", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtPersonelAdi.DataBindings.Add("Text", _entity, "PersonelAdi",false,DataSourceUpdateMode.OnPropertyChanged);
            btnUnvani.DataBindings.Add("Text", _entity, "Unvani",false,DataSourceUpdateMode.OnPropertyChanged); 
            txtTcKimlikNo.DataBindings.Add("Text", _entity, "TcKimlikNo",false,DataSourceUpdateMode.OnPropertyChanged);
             
            cbmIseGirisTarihi.DataBindings.Add("EditValue", _entity, "IseGirisTarihi", true,
                DataSourceUpdateMode.OnPropertyChanged, null, "F");
            cbmIsenCikisTarihi.DataBindings.Add("EditValue", _entity, "IstenCikisTarihi", true,
                DataSourceUpdateMode.OnPropertyChanged,null,"F");
            txtCepTelefon.DataBindings.Add("Text", _entity, "CepTelefonu", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtVergiDairesi.DataBindings.Add("Text", _entity, "VergiDairesi", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtVergiNo.DataBindings.Add("Text", _entity, "VergiNo", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtAciklama.DataBindings.Add("Text", _entity, "Aciklama", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtSabitTel.DataBindings.Add("Text", _entity, "Telefon", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtFax.DataBindings.Add("Text", _entity, "Fax", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtEmail.DataBindings.Add("Text", _entity, "Email", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtWeb.DataBindings.Add("Text", _entity, "Web", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtIl.DataBindings.Add("Text", _entity, "Il", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtIlce.DataBindings.Add("Text", _entity, "Ilce", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtSemt.DataBindings.Add("Text", _entity, "Semt", false,
                DataSourceUpdateMode.OnPropertyChanged);
            txtAdres.DataBindings.Add("Text", _entity, "Adres", false,
                DataSourceUpdateMode.OnPropertyChanged);
            calcMaasi.DataBindings.Add("EditValue", _entity, "AylikMaasi", true, DataSourceUpdateMode.OnPropertyChanged, 0,"C2");
            calcPrimOrani.DataBindings.Add("EditValue", _entity, "PrimOrani", true, DataSourceUpdateMode.OnPropertyChanged, 0,"'%'0");

        }

        private void FrmPersonelIslem_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (personelDal.AddorUpdate(contex,_entity))
            {
                personelDal.Save(contex);
                saved = true;
                this.Close();
            }
        }

        private void btnUnvani_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmTanim form=new FrmTanim(FrmTanim.TanimTuru.PersonelUnvan);
            form.ShowDialog();
            if (form.secildi)
            {
                btnUnvani.Text = form._entity.Tanimi;

            }
        }
    }
}