using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Models
{
    public class SCWChangePassword
    {
        public string staffId { get; set; }
        public string password { get; set; }
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }
    }

    public class SCWChangePasswordResult
    {
        public SCWStatus status { get; set; }
    }
}

/*

{
    "staffId": "00333",
    "password": "e10adc3949ba59abbe56e057f20f883e",
    "newPassword": "e10adc3949ba59abbe56e057f20f883e",
    "confirmNewPassword": "e10adc3949ba59abbe56e057f20f883e"
}

*/