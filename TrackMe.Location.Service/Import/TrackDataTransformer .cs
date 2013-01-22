using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Support.Import;
using TrackMe.Support.SafeCast;

namespace TrackMe.Location.Service.Import
{
    public class TrackDataTransformer : ImportedItemTransformer<Track>
    {

        public TrackDataTransformer()
        {

        }

        public override Track TransformItem(object[] itemValues)
        {
            int deviceId = 1;
            decimal latitude = 0;
            decimal longitude = 0;
            string[] date = new string[0];
            string[] time = new string[0];
            //int year = 0;
            //int month = 0;
            //int day = 0;
            //int hour = 0;
            //int minute = 0;
            //int second = 0;

            DateTime trackDate = DateTime.Now;

            deviceId = SafeCast.ToInteger(itemValues[0]);
            latitude = SafeCast.ToDecimal(itemValues[1].ToString().Replace(".", ","));
            longitude = SafeCast.ToDecimal(itemValues[2].ToString().Replace(".", ","));

            Track result = new Track()
            {
                DeviceId = deviceId,
                Latitude = latitude,
                Longitude = longitude,
                TrackDate = DateTime.Now
            };

            return result;

            //latitude = SafeCast.ToDecimal(itemValues[2]);
            //longitude = SafeCast.ToDecimal(itemValues[3]);

            //date = itemValues[4].ToString().Split('/');
            //year = SafeCast.ToInteger(date[2]);
            //month = SafeCast.ToInteger(date[0]);
            //day = SafeCast.ToInteger(date[1]);

            //time = itemValues[5].ToString().Split(':');
            //hour = SafeCast.ToInteger(time[0]);
            //minute = SafeCast.ToInteger(time[1]);
            //second = SafeCast.ToInteger(time[2]);

            //trackDate = new DateTime(year, month, day, hour, minute, second);

            //Track result = new Track()
            //{
            //    DeviceId = deviceId,
            //    Latitude = latitude,
            //    Longitude = longitude,
            //    TrackDate = trackDate
            //};
   
            //return result;
        }

        /// <summary>
        /// Determines whether the specified item values represents a valid item.
        /// </summary>
        /// <param name="itemValues">The item values which represents an element.</param>
        /// <returns>
        /// 	<c>true</c> if the element is valid ; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValidItem(object[] itemValues)
        {
            bool isValid = true;

            if (IsBlankLine(itemValues) || itemValues.Count() < 3)
                isValid = false;

            return isValid;
        }
    }
}
