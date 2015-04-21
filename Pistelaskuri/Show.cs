using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pistelaskuri
{
    public class Show
    {
        public string Date { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public string Placing { get; set; }
        public int Points { get; set; }
        public Dog Doggi { get; set; }

        public Show(string date, string place, string type, string placing, int points, Dog dog)
        {
            Date = date;
            Place = place;
            Type = type;
            Placing = placing;
            Points = points;
            Doggi = dog;
        }

        public Show()
        {
        }

    }
}
