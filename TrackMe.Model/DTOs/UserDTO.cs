using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Model.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
    }
}
