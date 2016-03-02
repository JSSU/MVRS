using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVRS
{
    public partial class _default : System.Web.UI.Page
    {
        IQueryable<MVR> DListAll = null;
        DPW_OBC_PrequalMVRS dbcontext = new DPW_OBC_PrequalMVRS();
         
        protected void Page_Load(object sender, EventArgs e)
        {

            string s = accountsearch.Value;
            lblerrobox.Text = "";

            if (datetimepickerFrom.Value == "" && datetimepickerTo.Value == "" && s == "")
                lblerrobox.Text = "No input";
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
            if (!IsPostBack)
            {
                lblerrobox.Text = "";

            }
            //paging start
            GVResult.DataBound += GVResult_DataBound;
            GridViewRow row = GVResult.BottomPagerRow;
            if (row == null) return;
            DropDownList pages = (DropDownList)row.Cells[0].FindControl("pages");
            pages.SelectedIndexChanged += OnSelectedIndexChanged;
            //paging end

        }

        //this is for search btn
        //protected void BtnSearch(object sender, EventArgs e)
        //{
        //    inputfrom.Text = datetimepickerFrom.Value;
        //    inputto.Text = datetimepickerTo.Value;
        //}

        protected void BtnGrid(object sender, EventArgs e)
        {
            GVResult.AllowPaging = true;
            GVResult.PageSize = 15;
            //output
            if (DListAll != null)
            {
                var datalist = (from u in DListAll

                                select new
                                {
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
                                    TextPrompt = u.textPrompt,
                                    RF_ERT_ID = u.rfErtId,
                                    TamperStatus = u.tamperStatus,
                                    Radio_Read=u.radioRead
                                }).ToList();
                GVResult.DataSource = datalist;
                Session["GVTable"] = datalist; //store table view in Session
                GVResult.DataBind();
                TotalResult.Text = datalist.Count.ToString();
            }
            else { GVResult.DataBind(); }
           
        }

        protected void GVResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //if (DListAll != null)
            //{
            //    var datalist = (from u in DListAll

            //                    select new
            //                    {
            //                        AccountNumber = u.accountNumber,
            //                        MeterNumber = u.meterNumber,
            //                        Comment = u.comment,
            //                        Read = u.rdgRead,
            //                        ReadDate = u.readDate.ToString().Substring(0, 10),
            //                        ReadTime = u.readTime,
            //                        ReadCode = u.readCode,
            //                        SkipCode = u.skipCode,
            //                        TCode1 = u.tCode1,
            //                        TCode2 = u.tCode2,
            //                        MReaderID = u.mReaderId,
            //                        PrevRead = u.preReading,
            //                        ReadMethod = u.readMethod,
            //                        TextPrompt = u.textPrompt
            //                    }).ToList();
            //    GVResult.DataSource = datalist;
            //    GVResult.PageIndex = e.NewPageIndex;
            //    GVResult.DataBind();
            //    TotalResult.Text = datalist.Count.ToString();
            //}
            //else { }
            if (Session["GVTable"] != null)
            {
                GVResult.DataSource = Session["GVTable"];
                GVResult.PageIndex = e.NewPageIndex;
                GVResult.DataBind();
            }
        }

        protected void GVResult_DataBound(object sender, EventArgs e)
        {
            //for paging
            GridViewRow row = GVResult.BottomPagerRow;
            if (row == null) return;
            DropDownList pages = (DropDownList)row.Cells[0].FindControl("pages");
            pages.SelectedIndexChanged += OnSelectedIndexChanged;
            Label count = (Label)row.Cells[0].FindControl("count");
            if (pages != null)
            {
                // populate pager
                for (int i = 0; i < GVResult.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem pageItem = new ListItem(pageNumber.ToString());
                    if (i == GVResult.PageIndex) pageItem.Selected = true;
                    pages.Items.Add(pageItem);
                }
            }
            // populate page count
            if (count != null)
            {
                count.Text = string.Format("<b>{0}</b>", GVResult.PageCount);
            }
            LinkButton prev = (LinkButton)row.Cells[0].FindControl("prev");
            LinkButton next = (LinkButton)row.Cells[0].FindControl("next");
            LinkButton first = (LinkButton)row.Cells[0].FindControl("first");
            LinkButton last = (LinkButton)row.Cells[0].FindControl("last");
            // set the pager nav state based on the current page❺
            if (GVResult.PageIndex == 0)
            {
                prev.Enabled = false;
                first.Enabled = false;
            }
            else if (GVResult.PageIndex + 1 == GVResult.PageCount)
            {
                last.Enabled = false;
                next.Enabled = false;
            }
            else
            {
                last.Enabled = true;
                next.Enabled = true;
                prev.Enabled = true;
                first.Enabled = true;
            }
        }
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow pager = GVResult.BottomPagerRow;
            DropDownList pages = (DropDownList)pager.Cells[0].FindControl("pages");
            GVResult.PageIndex = pages.SelectedIndex;
            //// a method to populate your grid
            //if (DListAll != null)
            //{
            //    var datalist = (from u in DListAll

            //                    select new
            //                    {
            //                        AccountNumber = u.accountNumber,
            //                        MeterNumber = u.meterNumber,
            //                        Comment = u.comment,
            //                        Read = u.rdgRead,
            //                        ReadDate = u.readDate.ToString().Substring(0, 10),
            //                        ReadTime = u.readTime,
            //                        ReadCode = u.readCode,
            //                        SkipCode = u.skipCode,
            //                        TCode1 = u.tCode1,
            //                        TCode2 = u.tCode2,
            //                        MReaderID = u.mReaderId,
            //                        PrevRead = u.preReading,
            //                        ReadMethod = u.readMethod,
            //                        TextPrompt = u.textPrompt
            //                    }).ToList();
            //    GVResult.DataSource = datalist;
            //    GVResult.DataBind();
            //    TotalResult.Text = datalist.Count.ToString();
            //}
            //else { }
            GVResult.DataSource = Session["GVTable"];
            GVResult.DataBind();
        }

        protected void showall_btn(object sender, EventArgs e) //Show all the data on the list 
        {
            GVResult.AllowPaging = false;
            ////output
            //if (DListAll != null)
            //{
            //    var datalist = (from u in DListAll

            //                    select new
            //                    {
            //                        AccountNumber = u.accountNumber,
            //                        MeterNumber = u.meterNumber,
            //                        Comment = u.comment,
            //                        Read = u.rdgRead,
            //                        ReadDate = u.readDate.ToString().Substring(0, 10),
            //                        ReadTime = u.readTime,
            //                        ReadCode = u.readCode,
            //                        SkipCode = u.skipCode,
            //                        TCode1 = u.tCode1,
            //                        TCode2 = u.tCode2,
            //                        MReaderID = u.mReaderId,
            //                        PrevRead = u.preReading,
            //                        ReadMethod = u.readMethod,
            //                        TextPrompt = u.textPrompt
            //                    }).ToList();
            //    GVResult.DataSource = datalist;
            //    GVResult.DataBind();
            //    TotalResult.Text = datalist.Count.ToString();
            //}
            //else { GVResult.DataBind(); }
            GVResult.DataSource = Session["GVTable"];
            GVResult.DataBind();
        }

        protected void GVResult_Sorting(object sender, GridViewSortEventArgs e)
        {
            
            DataTable dt = Session["GVTable"] as DataTable;
            if (dt != null)
            {
                //sort the data
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                GVResult.DataSource = Session["GVTable"];
                GVResult.DataBind();
            }
        }

        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

    }
}