<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MVRS._default" %>
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
<%--         <h3 style="text-align:center"> <a href="Document/HUL%20file%20codes.docx">Column/Code Definitions</a></h3> --%>
         <div style="text-align:center; margin-top:20px;">
             <button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#myModal"> <!-- data-target="#openModal" -->
              Column/Code Definitions</button>
         </div>
        <!-- Panal box starting here -->
         <div id="myModal" class="modal fade in" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" > 
             <%--style="overflow: auto; position: relative;"--%>
         <!-- style="display: block;" style="overflow: auto; overflow-y: hidden;"-->
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                  </button>
                  <h4 class="modal-title" id="myModalLabel">Code Reference for MV-RS HUL File</h4>
                </div>
                <div class="modal-body">
                    <p>The following fields are available:</p>
                    <p>Account Number</p>
                    <p>Meter Number</p>
                    <p>Comment – if a comment is entered by the meter reader</p>
                    <p>Read – the dial read, not including decimal places</p>
                    <p>Read Date</p>
                    <p>Read Time</p>
                    <p>Read Code:</p>
                    <ul style="margin-bottom:5px;">
                        <li>C – Customer Read</li>
                        <li>F – Failed probe attempt</li>
                        <li>I – Fixed Network</li>
                        <li>N – Normal Read</li>
                        <li>O – Operator (meter reader) manual read or office read</li>
                        <li>S – Skipped Read</li>
                        <li>Blank – Missed Read</li>
                    </ul>
                    <p>Skip Code</p>
                    <p>TCode 1 – Trouble Code 1</p>
                    <p>TCode 2 – Trouble Code 2</p>
                    <p>MReaderID – Meter Reader ID</p>
                    <p>PrevRead – the previous read for this meter dial</p>
                    <p>Read Method:</p>
                    <ul style="margin-bottom:5px;">
                        <li>K – Keyed</li>
                        <li>N – No read</li>
                        <li>P – Probed optically</li>
                        <li>R – Radio</li>
                        <li>W – Wand</li>
                    </ul>
                    <p>Text Prompt – text prompt provided to meter reader for type of read</p>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  <button type="button" class="btn btn-primary" onclick="location.href='Document/HUL%20file%20codes.docx'">Download</button>
                </div>

              </div><!-- /.modal-content -->
            </div><!-- /.modal-dialog -->
          </div>
        <!-- Panal box End here -->
         <div id="search" class="col-md-4 col-md-offset-4 starter">
            <div class="searchform">
                <h4>Enter Account Number(11 digits) or Meter Number(8 digits) or Meter Reader ID(2 digits) </h4>
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
          $('[data-toggle="tooltip"]').tooltip();
      });
      $(document).ready(function () {
          $('[data-toggle="popover"]').popover();
      });
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
