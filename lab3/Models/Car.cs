using System;

namespace lab3
{
    public partial class Car
    {
        public int CarId { get; set; }
        public int? OwnerId { get; set; }
        public string Model { get; set; }
        public int? Vis { get; set; }
        public string Colour { get; set; }
        public DateTime? YearOfIssue { get; set; }
        public int? BodyNumber { get; set; }
        public int? EngineNumber { get; set; }

        public override string ToString()
        {
            return CarId.ToString() + ' ' + OwnerId.ToString() + ' ' + Model + ' '
                + Vis.ToString() + ' ' + Colour + ' ' + Convert.ToDateTime(YearOfIssue).ToString("yyyy") + ' '
                + BodyNumber + ' ' + EngineNumber;
        }
    }
}
