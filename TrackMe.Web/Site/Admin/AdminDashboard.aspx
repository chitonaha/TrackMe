<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="TrackMe.Web.Site.Admin.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:HyperLink runat="server" ID="btnUserManagement" ToolTip="Administración de usuarios" Text="Administración de usuarios" NavigateUrl="~/Site/Admin/UserManagement.aspx" /><br />
<asp:HyperLink runat="server" ID="btnDeviceManagement" ToolTip="Administración de dispositivos" Text="Administración de dispositivos" NavigateUrl="~/Site/Admin/DeviceManagement.aspx" /><br />
<asp:HyperLink runat="server" ID="btnUserDeviceManagement" ToolTip="Asignación de dispositivos" Text="Asignación de dispositivos" NavigateUrl="~/Site/Admin/UserDeviceManagement.aspx" />
</asp:Content>
