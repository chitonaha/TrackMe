<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentLocation.aspx.cs" Inherits="TrackMe.Web.Site.User.CurrentLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    var deviceId = -1;
    var device = 0;
    var isRoute = false;
    var trajectories = [];

    setTimeout("getTracks();", 9000);

    function onSuccess(data) {
        trajectories = data.d;
        clearMarkers();
        markTracksInMap(trajectories, "map");

        //Set zoom only if user didn't change it
        if (!zoomChanged) {
            if (trajectories.length > 1) {
                fitZoom(trajectories);
            }
            else {
                setZoom(16);
            }
        }

        if (device == 0)
            clearFields();
        else
            getDirection(trajectories[0].LastTrack.Latitude, trajectories[0].LastTrack.Longitude, fillFields);
    }

    function fillFields(address) {
        
        var index = trajectories.findByDeviceId(device);
        var trajectory = trajectories[index];

        $("#lbDeviceId").text(trajectory.LastTrack.DeviceId);
        $("#lbFecha").text(trajectory.LastTrack.TrackDateFormatted);
        $("#lbLat").text(trajectory.LastTrack.Latitude);
        $("#lbLng").text(trajectory.LastTrack.Longitude);
        $("#lbAddress").html(address);
    }

    function clearFields() {
        $("#lbDeviceId").text("");
        $("#lbFecha").text("");
        $("#lbLat").text("");
        $("#lbLng").text("");
        $("#lbAddress").text("");
    }

    function updateMap(lnkButton, viewAll) {
        $('#details').show();
        deviceId = 0;
        isRoute = false;

        if (viewAll == 'false') {
            deviceId = $(lnkButton).parent().find(":hidden[name$='hdnDeviceId']").val();
            device = deviceId;
        }
        else
            device = 0;

        getTracks();
    }

    function getTracks() {

        if (!isRoute) {
            if (deviceId >= 0)
                callWebMethod("GetDeviceCoordinates", "{ deviceId: '" + deviceId + "' }", onSuccess);
        }

        setTimeout("getTracks();", 9000);
    }

    function initialize() {
        $('#details').hide();
        initializeMap("map");
    }

    $(document).ready(function () {
        initialize();
        google.maps.event.addListener(map, 'zoom_changed', function () {
            zoomChanged = true;
        });
        $("a[id$='lnkViewAllDevices']").click();
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="main-body">
  <div class="content-body">
    <div class="full-column">
      <h1>Localizacion actual</h1>
      <h3 class=" text-type-1"> Mapa</h3>
      <div class="map-history-container">
            <div id="map" style="width:860px;height:470px;" >
            </div>
      </div>
      <div class="distance-history-container">
        <h3 class=" text-type-1">Dispositivos</h3>
        <div class="track-grid-container mrg-b20">
          <ul class="location-list">
            <li><asp:LinkButton runat="server" ID="lnkViewAllDevices" Text="Ver todos" OnClientClick="updateMap(this, 'true'); return false;"></asp:LinkButton></li>
            <asp:Repeater runat="server" ID="rptDevices">
                <ItemTemplate>
                    <li>
                        <asp:HiddenField runat="server" ID="hdnDeviceId" Value='<%# Eval("DeviceId") %>' />
                        <asp:LinkButton runat="server" ID="lnkDevice" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Description") %>' OnClientClick="updateMap(this, 'false'); return false;"></asp:LinkButton>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
          </ul>
        </div>
      </div>
      <div class="track-history-container">
        <h3 class=" text-type-1">Detalles del dispositivo</h3>
        <div class="track-detail-container">
          <ul class="device-detail-list">
            <li>Dispositivo iD:&nbsp;<span><label id="lbDeviceId"></label></span></li>
            <li>Fecha:&nbsp;<span><label id="lbFecha"></label></span></li>
            <li>Latitud:&nbsp;<span><label id="lbLat"></label></span></li>
            <li>Longitud:&nbsp;<span><label id="lbLng"></label></span></li>
            <li>Direccion:&nbsp;<br /><span><label id="lbAddress"></label></span></li>
          </ul>
        </div>
      </div>
      <div class="clear-all"></div>
    </div>
  </div>
</div>
</asp:Content>
