using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model.Enums;
using TrackMe.Support.Configuration;

namespace TrackMe.Model.DTOs
{
    public class TrackDTO
    {
        public int TrackId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public TrackMe.Model.Enums.DeviceType DeviceType { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackDateFormatted { get { return TrackDate.ToString("dd/MM/yyyy") + " " + TrackDate.ToString("HH:mm:ss"); } }
        public string Info { get; set; }
        public string ImagenFileName { get; set; }
        public string ImagenUrl { get { return string.Format(AppSettingsManager.Instance.AWSItemUrlFormat, ImagenFileName); } }
    }
}
