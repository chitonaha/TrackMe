using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Model
{
    public partial class User
    {
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
    }
}
