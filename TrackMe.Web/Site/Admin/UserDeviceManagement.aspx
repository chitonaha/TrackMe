<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDeviceManagement.aspx.cs" Inherits="TrackMe.Web.Site.Admin.UserDeviceManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <div><h1>Asignación de Dispositivos</h1></div>

    <div style="clear: both"></div>
    
    <div>
        <div>Usuario
            <asp:DropDownList runat="server" ID="cboUsers" DataValueField="UserId" DataTextField="UserName" OnSelectedIndexChanged="cboUsers_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>

    <div style="clear: both">Dispositivos del Usuario</div>

    <div>
        <asp:Label runat="server" ID="lblUserDevicesMessage" Text="No existen dispositivos asignados al usuario." ForeColor="Red"></asp:Label>
        <asp:GridView runat="server" ID="grUserDevices" AutoGenerateColumns="false" OnRowCommand="grUserDevices_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="DeviceId" DataField="DeviceId" />
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="Description" DataField="Description" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkRemove" Text="Desasignar" CommandName="UnAssign" CommandArgument='<%# Eval("DeviceId") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    
    <div style="clear: both"></div>
    <div style="clear: both; padding-top: 20px;">Dispositivos no Asignados</div>
    <div>
        <asp:Label runat="server" ID="lblUnassignedDevicesMessage" Text="No existen dispositivos no asignados." ForeColor="Red"></asp:Label>
        <asp:GridView runat="server" ID="grUnnassignDevices" AutoGenerateColumns="false" OnRowCommand="grUserDevices_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnDeviceId" Value='<%# Eval("DeviceId") %>' />
                        <asp:CheckBox runat="server" ID="chkSelect" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="DeviceId" DataField="DeviceId" />
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="Description" DataField="Description" />
            </Columns>
        </asp:GridView>
    </div>

    <div style="clear: both"><asp:Button runat="server" ID="btnAssign" Text="Asignar" onclick="btnAssign_Click" /></div>
</div>

</asp:Content>
