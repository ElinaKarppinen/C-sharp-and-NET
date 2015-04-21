using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pistelaskuri
{
    public class Test
    {
        public string Date { get; set; }
        public string Place { get; set; }
        public string Type { get; set; } //Toko, RT, Mejä...
        public string TestResult { get; set; } //ALO1, ALO2...
        public int TestPoints { get; set; } //kokeen pistemäärä
        public string TestSija { get; set; } //1., 2...
        public int KisaPoints { get; set; } //kisassa ansaitut pisteet
        public Dog Doggi { get; set; }

        //date, place, testType, testClass, testResult, points, dog
        public Test(string date, string place, string testType, string testResult, int koepiste, string sijoitus, int points, Dog dog)
        {
            Date = date;
            Place = place;
            Type = testType;
            TestResult = testResult;
            TestPoints = koepiste;
            TestSija = sijoitus;
            KisaPoints = points;
            Doggi = dog;
        }

        public Test()
        {
        }

        
    }
}
