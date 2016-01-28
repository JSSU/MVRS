using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVRS
{
    public partial class _default : System.Web.UI.Page
    {
        DPW_OBC_PrequalMVRS dbcontext = new DPW_OBC_PrequalMVRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        //this is for search btn
        //protected void BtnSearch(object sender, EventArgs e)
        //{
        //    inputfrom.Text = datetimepickerFrom.Value;
        //    inputto.Text = datetimepickerTo.Value;
        //}

        protected void BtnGrid(object sender, EventArgs e)
        {
            string s = accountsearch.Value;
            lblerrobox.Text = "";
            IQueryable<MVR> DListAll=null;
            if (datetimepickerFrom.Value == "" && datetimepickerTo.Value == "" && s == "")
                lblerrobox.Text = "No input...";
            else if (datetimepickerFrom.Value == "" && datetimepickerTo.Value == "" && s != "")
            {
                DListAll = (from u in dbcontext.MVRS
                            where u.accountNumber == s
                            || u.meterNumber == s
                            || u.mReaderId == s
                            //where u.ID<100
                            select u).Take(500);
            }
            else if (s == "" && datetimepickerFrom.Value != "" && datetimepickerTo.Value != "")
            {
                DateTime fromdate = DateTime.ParseExact(datetimepickerFrom.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime todate = DateTime.ParseExact(datetimepickerTo.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                if (fromdate > todate)
                {
                    lblerrobox.Text = "From date is larger, please check";
                }
                //f.Text = fromdate.ToString();
                //t.Text = todate.ToString();
                DListAll = (from u in dbcontext.MVRS
                            where u.readDate >= fromdate && u.readDate <= todate
                            select u).Take(500);
            }
            else if (datetimepickerFrom.Value != "" && datetimepickerTo.Value == "" && s != "")
            {
                DateTime fromdate = DateTime.ParseExact(datetimepickerFrom.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DListAll = (from u in dbcontext.MVRS
                            where u.readDate >= fromdate
                            && (u.accountNumber == s
                            || u.meterNumber == s
                            || u.mReaderId == s)
                            select u).Take(500);
            }
            else if (datetimepickerFrom.Value == "" && datetimepickerTo.Value != "" && s != "")
            {
                DateTime todate = DateTime.ParseExact(datetimepickerTo.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DListAll = (from u in dbcontext.MVRS
                            where u.readDate <= todate
                            && (u.accountNumber == s
                            || u.meterNumber == s
                            || u.mReaderId == s)
                            select u).Take(500);
            }
            else if (datetimepickerFrom.Value != "" && datetimepickerTo.Value == "" && s == "")
            {
                DateTime fromdate = DateTime.ParseExact(datetimepickerFrom.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DListAll = (from u in dbcontext.MVRS
                            where u.readDate >= fromdate
                            select u).Take(500);
            }
            else if (datetimepickerFrom.Value == "" && datetimepickerTo.Value != "" && s == "")
            {
                DateTime todate = DateTime.ParseExact(datetimepickerTo.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DListAll = (from u in dbcontext.MVRS
                            where u.readDate <= todate
                            select u).Take(500);
            }
            else if (datetimepickerFrom.Value != "" && datetimepickerTo.Value != "" && s != "")
            {
                DateTime fromdate = DateTime.ParseExact(datetimepickerFrom.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime todate = DateTime.ParseExact(datetimepickerTo.Value + " 00:00:00", "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                if (fromdate > todate)
                {
                    lblerrobox.Text = "From date is larger, please check";
                }
                DListAll = (from u in dbcontext.MVRS
                            where u.readDate >= fromdate && u.readDate <= todate
                             && (u.accountNumber == s
                            || u.meterNumber == s
                            || u.mReaderId == s)
                            select u).Take(500);
            }
            else { DListAll = null; }
            //TotalResult.Text = typeof(DataList).ToString(); //(System.Web.UI.WebControls.DataList)
            

            //output
            if (DListAll != null)
            {
                var datalist = (from u in DListAll

                                select new {
                                    AccountNumber = u.accountNumber,
                                    MeterNumber = u.meterNumber,
                                    Comment = u.comment,
                                    Read = u.rdgRead,
                                    ReadDate = u.readDate.ToString().Substring(0, 10),
                                    ReadTime = u.readTime,
                                    ReadCode = u.readCode,
                                    SkipCode = u.skipCode,
                                    TCode1 = u.tCode1,
                                    TCode2 = u.tCode2,
                                    MReaderID = u.mReaderId,
                                    PrevRead = u.preReading,
                                    ReadMethod = u.readMethod,
                                    TextPrompt = u.textPrompt
                                }).ToList();
                GVResult.DataSource = datalist;
                GVResult.DataBind();
                TotalResult.Text = datalist.Count.ToString();
            }
            else { }

        }
    }
}