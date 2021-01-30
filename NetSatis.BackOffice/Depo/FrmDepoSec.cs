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
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.BackOffice.Depo
{
    public partial class FrmDepoSec : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context = new NetSatisContext();
        DepoDal depoDal = new DepoDal();
        public Entities.Tables.Depo entity=new Entities.Tables.Depo();
        private string _stokKodu;
        public bool secildi = false;
        public FrmDepoSec(string stokKodu)
        {
            InitializeComponent();
            _stokKodu = stokKodu;
        }

        private void FrmDepoSec_Load(object sender, EventArgs e)
        {
            gridcontDepo.DataSource = depoDal.DepoBazindaStokListele(context, _stokKodu);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridDepo.SelectedRowsCount!=0)
            {
                string depoKodu = gridDepo.GetFocusedRowCellValue(colDepoKodu).ToString();
                entity = context.Depolar.SingleOrDefault(c => c.DepoKodu == depoKodu);
                secildi = true;
                this.Close();
            }
            
        }
    }
}