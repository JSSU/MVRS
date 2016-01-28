<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnotherImplement.aspx.cs" Inherits="MVRS._AnotherImplement" %>
<html lang="en">
<head>
    <title></title>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css?parameter=1" rel="stylesheet">

    <script src="js/jquery-ui.js"></script>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
     <%--timepicker  --%> 
        <%--<link rel="stylesheet" type="text/css" media="screen" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" />--%>
        <%--<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">--%>
        <%--<script type="text/javascript" src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>--%>
		
		<script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
        <script type="text/javascript" src="//code.jquery.com/jquery-2.1.1.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <script src="scripts/bootstrap-datetimepicker.js"></script>
        <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />
    <%--timepicker  --%> 

    <link rel="icon" href="icons/form.ico">
    <style>
        #draggable { width: 150px; height: 150px; padding: 0.5em; border: 1px solid #ccc; display: table; }  
        #centerdiv { text-align:center; vertical-align: middle; display: table-cell;}  
    </style>
</head>
<body>
<form id="MVRSForm" runat="server">
    <div class="container">
         <h1 style="text-align:center">MVRS Meter Reading Search</h1>  
         <div id="search" class="col-md-4 col-md-offset-4 starter">
            <div class="searchform">
                <h4>Enter Account Number(11 digits) or Meter Number(8 digits) or Meter Reader ID(2 digits) <a href="Document/HUL%20file%20codes.docx">Column/Code Definitions</a></h4>
                <input type="text" class="form-control pop" id="accountsearch" runat="server" />
<%--                data-content="Use 'Account Number','Meter number', or 'Meter reader id' for search"
                       data-placement="top" data-trigger="hover">--%>
                       
<%--datetime--%>
                <div style="padding-top:20px">
                    <div class='col-md-6' style="padding:0 0 0 0">
                        <h4>From Date</h4>
                     </div>
                     <div class='col-md-6' style="padding:0 0 0 0">
                        <h4>To Date</h4>
                     </div>
                     <div class='col-md-6' style="padding:0 0 0 0">
                            <input type="text" class="form-control pop" id="datetimepickerFrom" runat="server" />
                     </div>
                     <div class='col-md-6' style="padding:0 0 0 0">
                            <input type="text" class="form-control pop" id="datetimepickerTo" runat="server" />
                     </div>
                </div>
<%--End datetime--%>
                <div class="col-md-12 btn-topdown">
                  <%--<asp:LinkButton ID="Search" CssClass="btn btn-default" runat="server" OnClick="BtnSearch">Search</asp:LinkButton>--%>
                  <asp:LinkButton ID="testgrid" CssClass="btn btn-default" runat="server" OnClick="BtnGrid">Search</asp:LinkButton>
                  <%--<%=getWhileLoopData() %>--%>
                  <asp:Label ID="lblerrobox" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
         </div>
    </div>

    <div id="resultGV" class="container" style="text-align:center; margin-top:20px;"> 
<%--        <asp:Label ID="inputfrom" runat="server" Text=""></asp:Label>
        <asp:Label ID="inputto" runat="server" Text=""></asp:Label>
        <br />--%>
        <asp:Label ID="f" runat="server" Text=""></asp:Label>
        <asp:Label ID="t" runat="server" Text=""></asp:Label>
        <label style="text-align:center; font-size:18px;">Note: Data is not available for 3/13/2015 to 6/24/2015.</label><br />
        <label for="TotalResult" style="text-align:center; font-size:18px;">Results(limit to 500 records): </label>
        <asp:Label ID="TotalResult" runat="server" Text=""></asp:Label>

    </div>
    <asp:GridView ID="GVResult" class="table table-hover table-striped" runat="server" AllowCustomPaging="True"></asp:GridView>

<!-- success box-->
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
  <script type="text/javascript">
      $(document).ready(function () {
          $('.pop').popover();
          $('#datetimepickerFrom').datetimepicker({ format: "MM/DD/YYYY" });
          $('#datetimepickerTo').datetimepicker({ format: "MM/DD/YYYY" });
      });
      $(".nav a").on("click", function () {
          //Acitive nav. Default no highlight, 
          $(".nav").find(".active").removeClass("active");
          $(this).parent().addClass("active");
          //alert($(this).html());
          $(".container .mainStarter .starter").addClass("hideContainer").removeClass("col-md-4");
          $(".container .mainStarter #" + $(this).html()).removeClass("hideContainer").addClass("col-md-8 col-md-offset-2");
      });
      $(".navbar-brand").on("click", function () {
          $(".nav").find(".active").removeClass("active");
          $(".container .mainStarter .starter").removeClass("hideContainer col-md-8 col-md-offset-2").addClass("col-md-4");
          $(".container .mainStarter #Summary-Sheet").removeClass("col-md-4").addClass("col-md-8");
          $(".container .mainStarter #Main").removeClass("col-md-4").addClass("col-md-8");
          
      });
      //show success box from code behind
      function openModal() {
          $('#myModal').modal('show');
      }
  </script>
<script type="text/javascript">    (function () { return window.SIG_EXT = {}; })()</script>

</form>
</body>
</html>
