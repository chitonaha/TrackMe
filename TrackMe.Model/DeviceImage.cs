//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrackMe.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeviceImage
    {
        public int DeviceImagesId { get; set; }
        public int DeviceId { get; set; }
        public int ImageId { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual Device Device { get; set; }
    }
}