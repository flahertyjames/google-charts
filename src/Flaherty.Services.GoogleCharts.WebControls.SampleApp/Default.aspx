<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Flaherty.Services.GoogleCharts.WebControls.SampleApp.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <google:PieChart ID="PieChart1" runat="server" 
            Options-Title="My Daily Activities" Width="900" Height="500" />
        <google:PieChart ID="PieChart2" runat="server" 
            Options-Title="My Daily Activities" Options-Is3D="true" Width="900" Height="500" />
        <google:PieChart ID="PieChart3" runat="server" 
            Options-Title="My Daily Activities" Options-PieHole="0.4" Width="900" Height="500" />
        <google:PieChart ID="PieChart4" runat="server" 
            Options-Title="My Daily Activities" Options-PieStartAngle="90" Width="900" Height="500" />
        <google:PieChart ID="PieChart5" runat="server" 
            Options-Title="My Daily Activities" Width="900" Height="500">
            <Slice Index="1" Color="#ff0000" />
            <Slice Index="2" Offset="0.2" />
        </google:PieChart>
        <google:PieChart ID="PieChart6" runat="server" 
                         Options-Title="My Daily Activities" Palette="Red,Blue,Green,#fff000" Width="900" Height="500" />
    </form>
</body>
</html>
