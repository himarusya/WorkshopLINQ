using System;

namespace lab3
{
    public partial class Mechanic
    {
        public int MechanicId { get; set; }
        public string FioMechanic { get; set; }
        public string Qualification { get; set; }
        public int? Experience { get; set; }
        public int? Salary { get; set; }

        public override string ToString()
        {
            return MechanicId.ToString() + ' ' + FioMechanic + ' ' + Qualification + ' '
                + Experience.ToString() + ' ' + Salary.ToString();
        }
    }
}
