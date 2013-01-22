<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="TrackMe.Web.Site.User.Administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Dispositivos</h1>
<div>
    <asp:GridView runat="server" ID="grDevices" AutoGenerateColumns="false" OnRowCommand="grDevices_RowCommand">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Nombre" />
            <asp:BoundField DataField="Description" HeaderText="Descripción" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkReset" Text="Reiniciar" CommandName="ResetDevice" CommandArgument='<%# Eval("DeviceId") %>' OnClientClick="return confirm('Está seguro?');"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
</asp:Content>
