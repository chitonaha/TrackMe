<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Background.aspx.cs" Inherits="TrackMe.Web.Site.User.Background" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
    var routes = [];
    var trackFrom;
    var trackTo;
    var trackFromId;
    var trackToId;
    var deviceId;

    function getDistance() {
        trackFromId = $("#" + '<%=ddlFrom.ClientID %>' + " option:selected").val();
        trackToId = $("#" + '<%=ddlTo.ClientID %>' + " option:selected").val();
        deviceId = $("#" + '<%=ddlAllDevices.ClientID %>' + " option:selected").val();

        callWebMethod("GetRoutesByTrackFromAndTrackTo", "{ trackFrom: '" + trackFromId + "', trackTo: '" + trackToId + "', deviceId: '" + deviceId + "' }", getDistanceSuccess);
    }

    function getDistanceSuccess(data) {
        routes = data.d;

        for (var i = 0; i < routes.length; i++) {
            var indexFrom = routes.findByTrackId(trackFromId, 'true');
            var indexTo = routes.findByTrackId(trackToId, 'false');

            if (indexFrom >= 0) {
                trackFrom = routes[indexFrom].Origin
            }

            if (indexTo >= 0) {
                trackTo = routes[indexTo].Destination
            }
        }

        clearMarkers();
        markTracksFromAndToInMap(trackFrom, trackTo, "map");
        calculateDistance(routes, showDistance);
    }

    function showDistance(distances) {
        var total = 0;

        for (var i = 0; i < distances.length; i++) {
            total += distances[i];
        }

        total = total / 1000;

        $("#<%=gvTracks.ClientID %> tr").each(function () {

            //Skip first(header) row
            if (this.rowIndex == 0) return;

            var value = 0;
            if (this.rowIndex > 1)
                value = distances[this.rowIndex - 2] / 1000;

//            $(this).find("span[id*='lblDistance']").text(value.toFixed(1) + " km");
//            $(this).find("span[id*='lblTotalDistance']").text(total.toFixed(1) + " km");

            $(this).find(".clsFind").text(value.toFixed(1) + " km");
            $(this).find(".clsFindTotalDistance").text(total.toFixed(1) + " km");
        });

        calculateRoute(routes, "directionsPanel");
    }

    function changeDateTrack() {
        $("#<%=gvTracks.ClientID %>").hide();
        $("#divTravel").hide();
    }

    function initialize() {
        $("#divTravel").hide();

        initializeMap("map");
    }

    function showDistancies() {
        ShowPopUp("Distancias", $("#distanciasDiv").html());
    }
    
    function showDirections() {
        ShowPopUp("Direcciones", $("#directionsPanel").html());
    }

    window.onload = initialize; 
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-body">
        <div class="full-column">
                    <h1>Historial</h1>
                    <h3 class=" text-type-1">Busqueda de historial</h3>
                    <div class="filter-container">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div class="filter-col">
                    <div class="drop-label">Dispositivos:</div>
                    <asp:DropDownList ID="ddlAllDevices" runat="server" DataTextField="Description" 
                                        DataValueField="DeviceId" Width="250px" AutoPostBack="true" CssClass="wdt-250 mrg-b20" 
                                        onselectedindexchanged="ddlAllDevices_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="filter-col">
                    <div class="drop-label">Fecha Desde:</div>
                        <asp:DropDownList ID="ddlFrom" runat="server" DataTextField="TrackDateFormatted" CssClass="wdt-250 mrg-b20" 
                                        DataValueField="TrackId" Width="250px" AutoPostBack="true" onchange="changeDateTrack(); return false;"></asp:DropDownList>
                </div>
                <div class="filter-col">
                    <div class="drop-label">Fecha Hasta:</div>
                        <asp:DropDownList ID="ddlTo" runat="server" DataTextField="TrackDateFormatted" CssClass="wdt-250 mrg-b20"
                                        DataValueField="TrackId" Width="250px" AutoPostBack="true" onchange="changeDateTrack(); return false;"></asp:DropDownList>
                </div>
                <div class="filter-col-button">
                    <asp:Button ID="btnFind" CssClass="form_blue_btn" runat="server" Text="Buscar" onclick="btnFind_Click" />
                </div>
                </ContentTemplate>
                

            </asp:UpdatePanel>
            </div>  
                    <h3 class=" text-type-1"> Mapa</h3>
                    <div class="map-history-container">
                        <div id="map" style="width:860px;height:470px;"></div>
                    </div>
                    <div class="distance-history-container">
                        <h3 class=" text-type-1">Distancia Recorrida</h3>
                            <div class="track-grid-container mrg-b20">
                <asp:UpdatePanel runat="server" ID="upGridView">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnFind" EventName="Click"></asp:AsyncPostBackTrigger>
                    </Triggers>
                    <ContentTemplate>
                    
                    <asp:GridView ID="gvTracks" runat="server" ShowHeader="true" EmptyDataText="No hay datos para mostrar" ShowHeaderWhenEmpty="true"
                                    Width="375px" AutoGenerateColumns="false" HeaderStyle-Font-Size="8pt" RowStyle-Font-Size="8pt" ShowFooter="true"
                                    FooterStyle-Font-Size="8pt" CssClass="track-grid" HeaderStyle-CssClass="device-track-header">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="75px" DataField="TrackDateFormatted" HeaderText="Fecha" />
                            <asp:BoundField ItemStyle-Width="125px" DataField="Latitude" HeaderText="Latitud" />
                            <asp:BoundField ItemStyle-Width="125px" DataField="Longitude" HeaderText="Longitud" />
                            <asp:TemplateField HeaderText="Distancia" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" CssClass="clsFind" ID="lblDistance" Text=""></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalDistance"  CssClass="clsFindTotalDistance" runat="server" />
                                    </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                            <div class="search-button-container mrg-b20">
                    <input class="form_blue_btn_large mrg-r5" name="" type="button" value="ver distancias" />
                    <input class="form_blue_btn_large" name="" type="button" value="ver direcciones" />
                </div>
                    </div>
                    <div class="track-history-container"> 
                        <h3 class=" text-type-1">Recorrido Realizado</h3>
                        <div id="directionsPanel" class="track-detail-container"></div>
                    </div>
                    <div class="clear-all"></div>
              
        </div>
    </div>
</asp:Content>
