using System;
using System.Collections.Generic;

namespace lab3
{
    public partial class Workroom
    {
        public int WorkroomId { get; set; }
        public int? CarId { get; set; }
        public int? MechanicId { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? Cost { get; set; }

        public override string ToString()
        {
            return WorkroomId.ToString() + ' ' + CarId.ToString() + ' ' + MechanicId.ToString()
                + ' ' + Convert.ToDateTime(ReceiptDate).ToString("dd.MM.yyyy") + Cost.ToString();
        }
    }
}
