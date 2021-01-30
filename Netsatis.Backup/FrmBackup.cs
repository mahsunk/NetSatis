using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NetSatis.Entities.Context;
using NetSatis.Entities.Tools;

namespace Netsatis.Backup
{
    public partial class FrmBackup : DevExpress.XtraEditors.XtraForm
    {
        NetSatisContext context = new NetSatisContext();
        public FrmBackup()
        {
            InitializeComponent();
        }

        private void btnYedekle_Click(object sender, EventArgs e)
        {
            string sqlCumle =
                $"USE Netsatis;BACKUP DATABASE NetSatis TO DISK='{Application.StartupPath + "\\NetSatisYedek.nsy"}'";
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sqlCumle);
        }

        private void txtYedekKonum_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog form=new FolderBrowserDialog();
            //if (form.ShowDialog()==DialogResult.OK)
            //{
            //    txtYedekKonum.Text == form.SelectedPath;
            //    SettingsTool.ayarDegistir;
            //}
        }
    }
}