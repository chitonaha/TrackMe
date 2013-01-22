<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TrackMe.Web.Site._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="first-column">
      <p class="text-type-1">Controlá tu flota de vehículos por</p>
      <p class="text-type-2">INTERNET <span class="d-blk">en TIEMPO REAL</span></p>
      <p>Con solo entrar a tu panel de control podrás ver al instante la posición de cada uno de tus vehículos, las rutas recorridas y sus distancias.</p>
      <div class="smatphone-publicity">"Además, si contás con un smartphone, podrás seguir tus móviles desde el mismo!"</div>
    </div>
    <div class="second-column">
      <ul class="welcome-content-list">
        <li>Localizar sus vehiculos de forma individual o en grupo.</li>
        <li>Controlar y mejorar las condiciones de conduccion.</li>
        <li>Verificar los tiempos de entrega y ofrecer un mejor servicio a sus clientes.</li>
        <li>Comprobar la informacion historica de sus moviles.</li>
        <li>Encontrar los vehiculos mas cercanos a su objetivo.</li>
        <li>Controlar la correcta utilizacion de su flota.</li>
        <li>Recuperar sus vehiculos y bienes en caso de robo.</li>
        <li>Verificar la actividad diaria de las personas de la empresa, la oficina o el hogar.</li>
        <li>Realizar un analisis de rendimiento por movil basado en datos historiales almacenados.</li>
      </ul>
      <div class="call-all"><span>Llamanos al</span> <span>0291 154 239860</span> <span>02943 155 2365987</span> </div>
    </div>
    <div class="third-column">
      <h1>Bienvenido</h1>
      <p class="pdn-b20">Somos una empresa joven que impulsada en la vasta experiencia de sus profesionales ofrece un servicio de seguridad integral de alta calidad. Nuestra premisa fundamental es brindar soluciones a todos nuestros clientes, integrando seguridad, comunicación y logística. Nos adaptamos a las exigencias de nuestros socios de negocio, brindándoles el producto exacto que ellos requieren.</p>
      <div class="welcome-sub-new"> <img src="<%=ResolveClientUrl("~/Content/Images/welcome_pic_1.png")%>" width="190" height="130" />
        <div class="welcome-sub-new-text"><span>El mejor servicio al más bajo costo</span> Podrás controlar toda tu flota, ver rutas y distancias recorridas, sacar información estadística de tus móviles a un precio de los más competitivos que existen en el mercado. Completá el formulario en la sección de contacto para pedir asesoramiento. </div>
      </div>
      <div class="welcome-sub-new"> <img src="<%=ResolveClientUrl("~/Content/Images/welcome_pic_2.png")%>" width="190" height="130" />
        <div class="welcome-sub-new-text"><span>Alcance de nuestro servcio</span> Ofrecemos un servicio de seguimiento satelital y auditoria de todo tipo de vehículos (automóviles, camiones, utilitarios, camionetas 4x4, maquinas agrícolas, equipos industriales y viales, containers, etc). </div>
      </div>
    </div>
    <div class="clear-all"></div>
</asp:Content>
