using System;
using System.Collections.Generic;

namespace lab3
{
    public partial class Owner
    {
        public int OwnerId { get; set; }
        public int? DriverLicense { get; set; }
        public string FioOwner { get; set; }
        public string Adress { get; set; }
        public int? Phone { get; set; }

        public override string ToString()
        {
            return OwnerId.ToString() + ' ' + DriverLicense.ToString() + ' ' + FioOwner.ToString()
                + Adress + ' ' + Phone;
        }
    }
}
