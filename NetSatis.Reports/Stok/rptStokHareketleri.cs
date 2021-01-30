using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;

namespace NetSatis.Reports.Stok
{
    public partial class rptStokHareketleri : DevExpress.XtraReports.UI.XtraReport
    {
        public rptStokHareketleri()
        {
            InitializeComponent();
            NetSatisContext context=new NetSatisContext();
            StokHareketDal stokHareketDal=new StokHareketDal();

            ObjectDataSource dataSource = new ObjectDataSource {DataSource = stokHareketDal.GetAll(context)};
            this.DataSource = dataSource;
            colFisKodu.DataBindings.Add("Text", this.DataSource, "FisKodu");
            //colHareket.DataBindings.Add("Text", this.DataSource, "Hareket");
            //colStokKodu.DataBindings.Add("Text", this.DataSource, "StokKodu");
            //colBarkod.DataBindings.Add("Text", this.DataSource, "Barkod");
            //colStokAdi.DataBindings.Add("Text", this.DataSource, "StokAdi");
            colBirimi.DataBindings.Add("Text", this.DataSource, "Birimi");
            colKdv.DataBindings.Add("Text", this.DataSource, "Kdv","{0:'%'0}");
            colMiktar.DataBindings.Add("Text", this.DataSource, "Miktar");
            colBirimFiyat.DataBindings.Add("Text", this.DataSource, "BirimFiyati", "{0:C2}");
            colIndirimOrani.DataBindings.Add("Text", this.DataSource, "IndirimOrani", "{0:'%'0}");

            CalculatedField calcIndirimTutar = new CalculatedField();
            this.CalculatedFields.Add(calcIndirimTutar);
            calcIndirimTutar.Name = "IndirimTutari";
            calcIndirimTutar.Expression = "([BirimFiyati]*[Miktar]) / 100 * [IndirimOrani]";

            CalculatedField calcTutar = new CalculatedField();
            this.CalculatedFields.Add(calcTutar);
            calcTutar.Name = "Tutar";
            calcTutar.Expression = "([BirimFiyati]*[Miktar]) - [IndirimTutari] ";

            colIndirimTutar.DataBindings.Add("Text", null, "IndirimTutari","{0:C2}");
            colTutar.DataBindings.Add("Text", null, "Tutar", "{0:C2}");

            XRSummary sumIndirimTutar=new XRSummary();
            sumIndirimTutar.Func = SummaryFunc.Sum;
            sumIndirimTutar.Running = SummaryRunning.Group;
            sumIndirimTutar.FormatString = "{0:C2}";
            XRSummary sumToplamTutar=new XRSummary();
            sumToplamTutar.Func = SummaryFunc.Sum;
            sumToplamTutar.Running = SummaryRunning.Group;
            sumToplamTutar.FormatString = "{0:C2}";


            lblIndirimTutar.Summary = sumIndirimTutar;
            lblToplamTutar.Summary = sumToplamTutar;


            lblIndirimTutar.DataBindings.Add("Text", null, "IndirimTutari");
            lblToplamTutar.DataBindings.Add("Text", null, "Tutar");
        }

    }
}
