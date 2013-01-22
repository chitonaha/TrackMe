<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="TrackMe.Web.Site.Services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="left-column">
              <h1>Rastreo Vehicular</h1>
               <p class="mrg-l10">Agregue seguridad y logistica a sus moviles</p>
               <p class="text-type-2">Brindamos a nuestros clientes la posibilidad de realizar un seguimiento de sus moviles en tiempo real las 24 hs los 365 dias del año.</p>
             
              
              <p class="mrg-l10">Mediante la instalacion de un dispositivo GPS en su movil logramos que este reporte, cada un tiempo
                tiempo configurable, su posicionamiento actual. Esta Informacion es recolectada por nuestros servidores y puesta a su disposicion a traves de nuestro sitio web. A nuestros clientes se les otorga un usuario con el cual podran acceder a su panel de control para poder visualizar todos los vehiculos que estan siguiendo. La informacion de posicionamiento y rutas es presentada mediante el uso de google maps.</p>
              <h3 class="mrg-l10 text-type-1">Con nuestro servicio usted podrá:</h3>
              <ul>
                
             
             <li ><span>Localizar sus vehículos de forma individual o en grupo.</span></li>
<li ><span>Controlar y mejorar las condiciones de conducción.</span></li>
<li ><span>Verificar los tiempos de entrega y ofrecer un mejor servicio a sus clientes.</span></li>
<li ><span>Comprobar la información histórica de sus móviles.</span></li>
<li ><span>Encontrar los vehículos más cercanos a su objetivo.</span></li>
<li ><span>Controlar la correcta utilización de su flota.</span></li>
<li ><span>Recuperar sus vehículos y bienes en caso de robo.</span></li>
<li ><span>Verificar la actividad diaria de las personas de la empresa, la oficina o el hogar.</span></li>
<li ><span>Realizar un análisis de rendimiento por móvil basado en datos historiales almacenados.</span></li>
<li ><span>Utilizar su smartphone para realizar el seguimiento.</span></li>
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
