using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using NetSatis.Entities.Context;
using NetSatis.Entities.Data_Access;
using NetSatis.Entities.Tables;

namespace NetSatis.Reports.Fatura_ve_Fis
{
    public partial class rptBilgiFisi : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBilgiFisi(string fiskodu)
        {
            InitializeComponent();
            NetSatisContext context = new NetSatisContext();
            StokHareketDal stokHareketDal = new StokHareketDal();
            FisDal fisDal = new FisDal();

            Fis fisBilgi = fisDal.GetByFilter(context, c => c.FisKodu == fiskodu);


            ObjectDataSource stokHareketDataSource = new ObjectDataSource { DataSource = stokHareketDal.GetAll(context, c => c.FisKodu == fiskodu) };
            lblFisKodu.Text = "Fiş Kodu :" + fisBilgi.FisKodu;
            lblTarih.Text = "Tarih    :" + fisBilgi.Tarih.ToString();
            
            
            this.DataSource = stokHareketDataSource;

           




            CalculatedField calcTutar = new CalculatedField();
            this.CalculatedFields.Add(calcTutar);
            calcTutar.Name = "Tutar";
            calcTutar.Expression = "([BirimFiyati]*[Miktar]) ";


            CalculatedField calcIndirimTutar = new CalculatedField();
            this.CalculatedFields.Add(calcIndirimTutar);
            calcIndirimTutar.Name = "IndirimTutari";
            calcIndirimTutar.Expression = "([BirimFiyati]*[Miktar]) / 100 * [IndirimOrani]";


            CalculatedField calcKdvToplam = new CalculatedField();
            this.CalculatedFields.Add(calcKdvToplam);
            calcKdvToplam.Name = "KdvTutari";
            calcKdvToplam.Expression = "([BirimFiyati]*[Miktar]-[IndirimTutari]) / 100 * [Kdv]";


            CalculatedField calcKdvsizToplam = new CalculatedField();
            this.CalculatedFields.Add(calcKdvsizToplam);
            calcKdvsizToplam.Name = "AraToplam";
            calcKdvsizToplam.Expression = "([BirimFiyati]*[Miktar]) - [KdvTutari] - [IndirimTutari]";

            CalculatedField calcKdvliTutar = new CalculatedField();
            this.CalculatedFields.Add(calcKdvliTutar);
            calcKdvliTutar.Name = "KdvliTutar";
            calcKdvliTutar.Expression = "([BirimFiyati]*[Miktar]) - [IndirimTutari]";

            XRSummary sumAraToplam = new XRSummary();
            sumAraToplam.Func = SummaryFunc.Sum;
            sumAraToplam.Running = SummaryRunning.Page;
            sumAraToplam.FormatString = "{0:C2}";

             

            XRSummary sumKdvToplam = new XRSummary();
            sumKdvToplam.Func = SummaryFunc.Sum;
            sumKdvToplam.Running = SummaryRunning.Page;
            sumKdvToplam.FormatString = "{0:C2}";


            XRSummary sumGenelToplam = new XRSummary();
            sumGenelToplam.Func = SummaryFunc.Sum;
            sumGenelToplam.Running = SummaryRunning.Page;
            sumGenelToplam.FormatString = "{0:C2}";
            
            XRSummary sumIndirimToplam = new XRSummary();
            sumIndirimToplam.Func = SummaryFunc.Sum;
            sumIndirimToplam.Running = SummaryRunning.Page;
            sumIndirimToplam.FormatString = "{0:C2}";

            //colToplamTutar.DataBindings.Add("Text", null, "Tutar", "{0:C2}");

            lblAraToplam.DataBindings.Add("Text", null, "AraToplam");
            lblIndirimTutari.DataBindings.Add("Text", null, "IndirimTutari");
            lblKdvToplam.DataBindings.Add("Text", null, "KdvTutari");
            lblGenelToplam.DataBindings.Add("Text", null, "KdvliTutar");
             

            lblAraToplam.Summary = sumAraToplam;
            lblKdvToplam.Summary = sumKdvToplam;
            lblGenelToplam.Summary = sumGenelToplam;
            lblIndirimTutari.Summary = sumIndirimToplam;
        }

    }
}
