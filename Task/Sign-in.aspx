<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sign-in.aspx.cs" Inherits="Task.Sign_in" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
    <title>Mobile first web app theme | first</title>
    <meta name="description" content="mobile first, app, web app, responsive, admin dashboard, flat, flat ui">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <link rel="stylesheet" href="assets/css/font.css">
    <link rel="stylesheet" href="assets/css/app.v2.css" type="text/css" />
    
     <!--[if lt IE 9]> <script src="js/ie/respond.min.js"></script> <script src="js/ie/html5.js"></script> <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
     <header id="header" class="navbar" style="padding-top:8px;background-color:#ffffff;box-shadow:1px 1px 1px #f2eeee"> 
    <a href="docs.html" class="btn btn-link pull-right m-t-mini">
        <i class="fa fa-question fa-lg text-default"></i></a>
            <img src="assets/images/TASKHOPS.png" style="height:35px;" /></header>
    <section id="content"> <div class="main padder"> <div class="row"> 
        <div class="col-lg-4 col-lg-offset-4 m-t-large">
         <section class="panel">
             <header class="panel-heading text-center"> Sign in </header> 
            <div  class="panel-body"> 
             <div class="block"> <label class="control-label">Email</label>
                 <input type="email" placeholder="test@example.com" class="form-control" style="height:40px"> </div>
              <div class="block"> <label class="control-label">Password</label>
                  <input type="password" id="inputPassword" placeholder="Password" class="form-control" style="height:40px"> </div>
              <div class="checkbox"> <label> <input type="checkbox"> Keep me logged in </label>
                 </div>
              <a href="#" class="pull-right m-t-mini"><small>Forgot password?</small></a>
                <input type="button" id="BtnLog" class="btn btn-info" value="Sign In" />
               
              <div class="line line-dashed"></div> <p class="text-muted text-center"><small>Do not have an account?</small></p> 
             <a href="signup.html" class="btn btn-white btn-block">Create an account</a>
              </div> </section>
         </div>
         </div>
         </div>
     </section>
    <footer id="footer"> <div class="text-center padder clearfix"> <p>
         <small>© Powered by : knowledgeflex Technologies Pvt. Ltd.</small><br><br> <a href="#" class="btn btn-xs btn-circle btn-twitter"><i class="fa fa-twitter"></i></a> <a href="#" class="btn btn-xs btn-circle btn-facebook"><i class="fa fa-facebook"></i></a> <a href="#" class="btn btn-xs btn-circle btn-gplus"><i class="fa fa-google-plus"></i></a> </p> </div> </footer>
    <script src="assets/js/app.v2.js"></script>
    <script src="assets/Operation/Login.js"></script>
    </form>
</body>
</html>
