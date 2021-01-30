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

namespace NetSatis.BackOffice.Kasa
{
    public partial class FrmKasaSec : DevExpress.XtraEditors.XtraForm
    {
        KasaDal kasaDal=new KasaDal();
        NetSatisContext context=new  NetSatisContext();
        public Entities.Tables.Kasa entity=new Entities.Tables.Kasa();
        public bool secildi = false;
        public FrmKasaSec()
        {
            InitializeComponent();
        }

        private void FrmKasaSec_Load(object sender, EventArgs e)
        {
            gridcontSecim.DataSource = kasaDal.KasaListele(context);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (gridSecim.GetSelectedRows().Length!=0)
            {
                string kasaKodu = gridSecim.GetFocusedRowCellValue(colKasaKodu).ToString();
                entity = context.Kasalar.SingleOrDefault(c => c.KasaKodu == kasaKodu);
                secildi = true;
                this.Close();
            }
           
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}