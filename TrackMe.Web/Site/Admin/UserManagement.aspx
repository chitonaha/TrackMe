<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="TrackMe.Web.Site.Admin.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <div><h1>Administración de Usuarios</h1></div>

    <div style="clear: both"></div>
    
    <div>
        <div>Usuario<asp:TextBox runat="server" ID="txtUserName"></asp:TextBox></div>
        <div>Nombre<asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox></div>
        <div>Apellido<asp:TextBox runat="server" ID="txtLastName"></asp:TextBox></div>
        <div>Email<asp:TextBox runat="server" ID="txtEmail"></asp:TextBox></div>
        <div>Contraseña<asp:TextBox runat="server" ID="txtPassword"></asp:TextBox></div>
    </div>
    <div style="clear: both"><asp:Button runat="server" ID="btnAdd" Text="Agregar" onclick="btnAdd_Click" /></div>
    
    <div style="clear: both"></div>
    
    <div>
        <asp:GridView runat="server" ID="grUsers" AutoGenerateColumns="false" OnRowCommand="grUsers_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="UserId" DataField="UserId" />
                <asp:BoundField HeaderText="UserName" DataField="UserName" />
                <asp:BoundField HeaderText="FirstName" DataField="FirstName" />
                <asp:BoundField HeaderText="LastName" DataField="LastName" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="IsDisabled" DataField="IsDisabled" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkRemove" Text="Delete" CommandName="DeleteUser" CommandArgument='<%# Eval("UserId") %>' OnClientClick="return confirm('Está seguro?');"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkDisable" Text="Enable/Disable" CommandName="EnableDisableUser" CommandArgument='<%# Eval("UserId") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
</asp:Content>
