<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AboutUs.aspx.cs" Inherits="TrackMe.Web.Site.AboutUs" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="left-column">
      <h1>Nuestra Empresa</h1>
      <p class="mrg-l10">Somos una empresa que esta en continuo contacto con las nuevas tendencias en el mundo de la informatica.</p>
      <p class="text-type-2">Nuestro objetivo es brindar a nuestrso clientes soluciones integrales que faciliten sus labores diarias y sumar valor agregado a su trabajo.</p>
      <p class="mrg-l10">Compuesto por un grupo de profesionales altamente calificado, desarrollamos productos de alta calidad. Nos constituimos como un socio estrategico de nuestros clientes, gbasandonos en la mejora contunua para aumentar la calidad de nuestros productos y servicios.</p>
      <p class="f-b">Bla bla bla:</p>
      <ul>
        <li ><span>asdfasdf.</span></li>
        <li ><span>asdfasdf.</span></li>
        <li ><span>asdfasdf.</span></li>
      </ul>
      <p class="mrg-l10">Para pedir asesoramiento haga click <a href="#">aqui</a></p>
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
