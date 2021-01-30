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

namespace NetSatis.BackOffice.Fiş
{
    public partial class FrmSeriNoGir : DevExpress.XtraEditors.XtraForm
    {
        public string veriSeriNo;
        public bool kilitli = true;
        public FrmSeriNoGir(string veri)
        {
            InitializeComponent();

            if (veri != null)
            {
                string[] veriListesi = veri.Split(new[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in veriListesi)
                {
                    listSeriNo.Items.Add(item);
                }
            }

            if (kilitli)
            {
                grpMenu.Enabled = false;
            }

        }



        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void KayitAc()
        {
            btnYeni.Enabled = false;
            btnSil.Enabled = false;
            btnKaydet.Enabled = true;
            btnVazgec.Enabled = true;
            grpBilgi.Enabled = true;
            txtSeriNo.Focus();
        }

        void KayitKapat()
        {
            btnYeni.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
            btnVazgec.Enabled = false;
            grpBilgi.Enabled = false;
            txtSeriNo.Text = null;
        }

        private void FrmSeriNoGir_Load(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            KayitAc();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listSeriNo.Items.Remove(listSeriNo.SelectedItem);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            listSeriNo.Items.Add(txtSeriNo.Text);
            KayitKapat();
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            KayitKapat();
        }

        private void FrmSeriNoGir_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (listSeriNo.Items.Count!=0)
            {
                foreach (var item in listSeriNo.Items)
                {
                    veriSeriNo += item + System.Environment.NewLine;
                }
            }
            
        }
    }
}