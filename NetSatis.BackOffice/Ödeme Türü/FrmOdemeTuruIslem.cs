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
using NetSatis.Entities.Tables;

namespace NetSatis.BackOffice.Ödeme_Türü
{
    public partial class FrmOdemeTuruIslem : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context = new NetSatisContext();
        OdemeTuruDal odemeTuruDal=new OdemeTuruDal();
        private OdemeTuru _entity;
        public bool saved = false;
        public FrmOdemeTuruIslem(OdemeTuru entity)
        {
            InitializeComponent();
            _entity = entity;
            txtOdemeKodu.DataBindings.Add("Text", _entity, "OdemeKodu");
            txtOdemeAdi.DataBindings.Add("Text", _entity, "OdemeAdi");
            txtAciklama.DataBindings.Add("Text", _entity, "Aciklama");

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (odemeTuruDal.AddorUpdate(context,_entity))
            {
                odemeTuruDal.Save(context);
                saved = true;
                this.Close();
            }
        }

        private void FrmOdemeTuruIslem_Load(object sender, EventArgs e)
        {

        }
    }
}