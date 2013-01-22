<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="TrackMe.Web.Site.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="left-column">
              <h1>Contáctenos</h1>
  <p class="mrg-l10">Estos son los departamentos con los cuales puede contactarse por preguntas y asesoramiento.</p>
        <h3 class=" text-type-1"> Nuestras oficinas</h3>
        <p class="mrg-l10"> Blablabla 1502 - Capital Federal.<br />
        <span>TEL: (+5411) - 4545-4545<br />
        FAX:
        </span><span class="email">(+5411) - 4545-4545 int 123</span></p>
    <h3 class=" text-type-1">Departamento Soporte</h3>
        <p class="mrg-l10"> Respuesta a dudas o problemas.<br />
        <span>info@tranckme.com.ar</span></p>
        <h3 class="text-type-1"> Informaci&oacute;n general </h3>
        <p class="mrg-l10">Para solicitar presupuesto o asesoramiento acerca de alguno de nuestros servicios, complete el formulario que se encuentra a continuaci&oacute;n. </p>
<table class="mrg-l10" width="433" border="0" cellpadding="0" cellspacing="3">
        <tbody>
          <tr>
            <td width="185" align="left" valign="middle" bgcolor="#3B3D42"><p>Nombre y apellido*</p></td>
            <td width="239" align="left" valign="top">
                <asp:TextBox runat="server" ID="txtName" CssClass="mrg-t4 contact-input" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtName" 
                                        ErrorMessage="" Display="None" ToolTip="Campo requerido." 
                                        ValidationGroup="ContactValidationGroup"></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle" bgcolor="#3B3D42"><p>Empresa</p></td>
            <td align="left" valign="top">
                <asp:TextBox runat="server" ID="txtCompany" CssClass="mrg-t4 contact-input" MaxLength="50"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle" bgcolor="#3B3D42"><p>Tel&eacute;fono*</p></td>
            <td align="left" valign="top">
                <asp:TextBox runat="server" ID="txtTelephone" CssClass="mrg-t4 contact-input" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="TelephoneRequired" runat="server" ControlToValidate="txtTelephone" 
                                        ErrorMessage="" Display="None" ToolTip="Campo requerido." 
                                        ValidationGroup="ContactValidationGroup"></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle" bgcolor="#3B3D42"><p>Mail*</p></td>
            <td align="left" valign="top">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="mrg-t4 contact-input" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MailRequired" runat="server" ControlToValidate="txtEmail" 
                                        ErrorMessage="" Display="None" ToolTip="Campo requerido." 
                                        ValidationGroup="ContactValidationGroup"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValidEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Introduzca un E-mail válido."
                    ToolTip="El Email no es válido." ValidationGroup="ContactValidationGroup"></asp:RegularExpressionValidator>    
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle" bgcolor="#3B3D42"><p>Direcci&oacute;n</p></td>
            <td align="left" valign="top">
                <asp:TextBox runat="server" ID="txtAddress" CssClass="mrg-t4 contact-input"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle" bgcolor="#3B3D42"><p>Escriba aqui su consulta*</p></td>
            <td align="left" valign="top">
                <asp:TextBox runat="server" ID="txtComment" CssClass="contact-textarea" MaxLength="50" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CommentRequired" runat="server" ControlToValidate="txtComment" 
                                        ErrorMessage="" Display="None" ToolTip="Campo requerido." 
                                        ValidationGroup="ContactValidationGroup"></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle" bgcolor="#3B3D42">
                <asp:HiddenField runat="server" ID="hdnNum1" />
                <asp:HiddenField runat="server" ID="hdnNum2" />
                <p><asp:Label runat="server" ID="lblSum"></asp:Label></p>
            </td>
            <td align="left" valign="top">
                <asp:TextBox runat="server" ID="txtSum" CssClass="mrg-t4 contact-input" MaxLength="50" onkeypress="javascript:return allowNumbers(event);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSum" 
                                        ErrorMessage="" Display="None" ToolTip="Campo requerido." 
                                        ValidationGroup="ContactValidationGroup"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvCheckSum" runat="server" OnServerValidate="cvCheckSum_CheckSum"
                    ControlToValidate="txtSum" Display="None" ErrorMessage="" ValidationGroup="ContactValidationGroup"></asp:CustomValidator>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle"></td>
            <td align="left" valign="bottom">
                <asp:Button runat="server" ID="btSubmit" Text="enviar" CssClass="btn-blue-small mrg-t4" ValidationGroup="ContactValidationGroup"/>
            </td>
          </tr>
          <tr>
            <td align="left" valign="middle"></td>
            <td align="left" valign="bottom">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-message"
                                        HeaderText="Debe completar los campos requeridos." ValidationGroup="ContactValidationGroup"/>
                <asp:Label runat="server" ID="lblMessage" Text="Gracias por su consulta, nos contactaremos con usted a la brevedad." Visible="false"></asp:Label>
            </td>
          </tr>
        </tbody>
        </table>
            </div>
            <div class="right-column">
      <div class="column-products-title">Productos</div>
      <img src="<%=ResolveClientUrl("~/Content/Images/product_full.png")%>" width="280" height="115" />
      <div class="sub-h1">Incluye:</div>
      <ul class="product-detail-list">
        <li>Dispositivo GPS de trackeo</li>
        <li>Instalación</li>
        <li>Usuario de acceso a panel de control para visualizar localización actual, rutas recorridas e historial desde una desktop o smartphone.</li>
      </ul>
      <img src="<%=ResolveClientUrl("~/Content/Images/product_lite.png")%>" width="280" height="115" />
      <div class="sub-h1">Incluye:</div>
      <ul class="product-detail-list">
        <li>Software para tu smartphone para utilizar como dispositivo de trackeo</li>
<li>Configuración inicial</li>
<li>Usuario de acceso a panel de control para visualizar localización actual, rutas recorridas e historial desde una desktop o smartphone.</li>
      </ul>
    </div>
            <div class="clear-all"></div>
</asp:Content>
