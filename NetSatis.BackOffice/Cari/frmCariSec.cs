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

namespace NetSatis.BackOffice.Cari
{
    public partial class frmCariSec : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context = new NetSatisContext();
        CariDal cariDal = new CariDal();
        public List<Entities.Tables.Cari> secilen = new List<Entities.Tables.Cari>();
       
        public bool secildi = false;
        public frmCariSec(bool cokluSecim = false)
        {
            InitializeComponent();
            if (cokluSecim)
            {
                lblUyari.Visible = true;
                gridView1.OptionsSelection.MultiSelect = true;
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCariSec_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = cariDal.GetCariler(context);
        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            if (gridView1.GetSelectedRows().Length!=0)
            {
                foreach (var row in gridView1.GetSelectedRows())
                {
                    string carikodu = gridView1.GetRowCellValue(row, colCariKodu).ToString();
                    secilen.Add(context.Cariler.SingleOrDefault(c => c.CariKodu == carikodu));
                    
                    //CariBakiye Caribaki =new CariBakiye();
                    //var sa = gridView1.GetRowCellValue(row, carikodu).ToString();
                    ////Caribaki.Alacak = Convert.ToDecimal(gridView1.GetRowCellValue(row, colAlacak));
                    ////Caribaki.Borc = Convert.ToDecimal(gridView1.GetRowCellValue(row, colBorc));
                    ////Caribaki.Bakiye = Convert.ToDecimal(gridView1.GetRowCellValue(row, colBakiye));

                }

                secildi = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seçilen bir cari bulunamadı.");
            }


            
        }
    }
}