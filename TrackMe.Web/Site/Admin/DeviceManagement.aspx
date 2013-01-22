<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeviceManagement.aspx.cs" Inherits="TrackMe.Web.Site.Admin.DeviceManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <div><h1>Administración de Dispositivos</h1></div>
    <div style="clear: both"></div>
    <div>
        <div>Nombre<asp:TextBox runat="server" ID="txtName"></asp:TextBox></div>
        <div>Descripción<asp:TextBox runat="server" ID="txtDescription"></asp:TextBox></div>
        <div>Tipo<asp:DropDownList runat="server" ID="cboDeviceType" DataTextField="Description" DataValueField="DeviceTypeId"></asp:DropDownList></div>
        <div>Imagen<asp:FileUpload runat="server" ID="fuImage" Width="600px" /></div>
    </div>
    <div style="clear: both"><asp:Button runat="server" ID="btnAdd" Text="Agregar" 
            onclick="btnAdd_Click" /></div>
    <div style="clear: both"></div>
    <div>
        <asp:GridView runat="server" ID="grDevices" AutoGenerateColumns="false" OnRowCommand="grDevices_RowCommand">
            <Columns>
                <asp:BoundField DataField="DeviceId" HeaderText="DeviceId" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="DeviceType" HeaderText="DeviceType" />
                <asp:BoundField DataField="ImageKey" HeaderText="ImageKey" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Image Link
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ImageUrl")%>' Text='<%# DataBinder.Eval(Container.DataItem, "ImageFileName")%>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsDisabled" HeaderText="IsDisabled" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkRemove" Text="Delete" CommandName="DeleteDevice" CommandArgument='<%# Eval("DeviceId") %>' OnClientClick="return confirm('Está seguro?');"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkDisable" Text="Enable/Disable" CommandName="EnableDisableDevice" CommandArgument='<%# Eval("DeviceId") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
</asp:Content>
