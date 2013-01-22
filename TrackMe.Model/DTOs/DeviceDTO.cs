using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Support.Configuration;

namespace TrackMe.Model.DTOs
{
    public class DeviceDTO
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DeviceTypeId { get; set; }
        public string DeviceType { get; set; }
        public string ImageFileName { get; set; }
        public string ImageKey { get; set; }
        public string ImageUrl 
        { 
            get { return string.Format(AppSettingsManager.Instance.AWSItemUrlFormat, ImageFileName); } 
        }
        public bool IsDisabled { get; set; }
    }
}
