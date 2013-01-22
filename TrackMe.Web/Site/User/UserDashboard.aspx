<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="TrackMe.Web.Site.User.UserDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    var deviceId = -1;
    var ddlDeviceId = '<%=ddlAllDevices.ClientID%>';
    var device = 0;
    var isRoute = false;
    var trajectories = [];

    setTimeout("getTracks();", 30000);

    function onSuccess(data) {
        trajectories = data.d;

        $("#" + ddlDeviceId).val(device);

        if (device == 0) {
            clearDetails();
        }
        else {
            changeDevice(ddlDeviceId);
        }

        clearMarkers();
        markTracksInMap(trajectories, "map");
                 
        if (deviceId > 0) {
             $("#" + ddlDeviceId).attr("disabled", "disabled");
        }
        else {
             $("#" + ddlDeviceId).removeAttr("disabled");
        }
    }

    function clearDetails() {
        $('#' + '<%=lnkRoute.ClientID %>').hide();
        $("#lblDistance").text("");
        $("#lblDuration").text("");
        $("#lblFrom").text("");
        $("#lblTo").text("");
    }

    function onGetDistanceByDeviceId() {
        var index = trajectories.findByDeviceId(device);

        if (index >= 0) {
            var trajectory = trajectories[index];
            calculateDistance(trajectory.Routes, showDistance);
            $("#lblFrom").text(trajectory.From);
            $("#lblTo").text(trajectory.To);
            $('#' + '<%=lnkRoute.ClientID %>').show();
        }
        else
            clearDetails();
    }

    function showDistance(distanceAprox, durationAprox) {
        distanceAprox = distanceAprox / 1000;
        $("#lblDistance").text(distanceAprox.toFixed(1) + " km");
        $("#lblDuration").text(convertSecondsToDays(durationAprox));
    }

    function onGetRouteByDeviceId() {
        var index = trajectories.findByDeviceId(device);

        if (index >= 0) {
            var trajectory = trajectories[index];
            calculateRoute(trajectory.Routes);
        }
    }

    function updateMap(lnkButton, viewAll) {
        $('#details').show();
        deviceId = 0;
        isRoute = false;

        if (viewAll == 'false') {
            deviceId = $(lnkButton).parent("div").find(":hidden[name$='hdnDeviceId']").val();
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

        setTimeout("getTracks();", 30000);
    }

    function changeDevice() {
        device = $("#" + ddlDeviceId + " option:selected").val();

        if (device > 0)
            onGetDistanceByDeviceId();
        else
            clearDetails();
    }

    function showRoute() {
        isRoute = !isRoute;
        var text = (isRoute) ? "Mostrar ubicación" : "Mostrar ruta";
        $('#' + '<%=lnkRoute.ClientID %>').text(text);

        if (isRoute) {
            onGetRouteByDeviceId();
        }
        else {
            getTracks();
        }
    }

    function initialize() {
        $('#details').hide();
        initializeMap("map");
    }

    window.onload = initialize; 
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    
    <div class="f-l" style="width: 150px">
        Dispositivos
        <br />
        <asp:LinkButton runat="server" ID="lnkViewAllDevices" Text="Ver todos" OnClientClick="updateMap(this, 'true'); return false;"></asp:LinkButton>
        <asp:Repeater runat="server" ID="rptDevices">
            <ItemTemplate>
            <div>
                <asp:HiddenField runat="server" ID="hdnDeviceId" Value='<%# Eval("DeviceId") %>' />
                <asp:LinkButton runat="server" ID="lnkDevice" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Description") %>' OnClientClick="updateMap(this, 'false'); return false;"></asp:LinkButton>
            </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="f-l">
        <div id="map" style="width: 700px; height: 450px; background-color: #e5e3df;">
        </div>
    </div>
</div>
<div>
    <div class="f-l" style="width: 150px">&nbsp;</div>
    <div class="f-l">
        <div id="details" style="width: 700px;">
            <table width="100%">
                <tr>
                    <td colspan="2"><asp:Label runat="server" Font-Bold="true">Detalle</asp:Label></td>
                </tr>
                <tr>
                    <td style="width:250px;">Dispositivo:</td>
                    <td colspan="2" style="width:525px;">
                        <asp:DropDownList ID="ddlAllDevices" runat="server" DataTextField="Description" 
                            DataValueField="DeviceId" Width="250px" 
                            onchange="changeDevice(this);"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:250px;">Distancia total aproximada:</td>
                    <td colspan="2" style="width:450px;"><label id="lblDistance"></label></td>
                </tr>
                <tr>
                    <td style="width:250px;">Duración total aproximada:</td>
                    <td colspan="2" style="width:450px;"><label id="lblDuration"></label></td>
                </tr>
                <tr>
                    <td style="width:250px;">Fecha Desde:</td>
                    <td style="width:100px;"><label id="lblFrom"></label></td>
                    <td style="width:350px;"><asp:LinkButton runat="server" ID="lnkRoute" Text='Mostrar ruta' ToolTip='Mostrar ruta' OnClientClick="showRoute(); return false;"></asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width:250px;">Fecha Hasta:</td>
                    <td colspan="2" style="width:525px;"><label id="lblTo"></label></td>
                </tr>
             </table>
        </div>
    </div>
</div>

</asp:Content>
