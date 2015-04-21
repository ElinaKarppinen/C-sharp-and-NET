using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pistelaskuri
{
    public class Dog
    {
        public string VirName { get; set; }
        public string KutsName { get; set; }
        public int BasePoints { get; set; }
        public int fullPointsShow { get; set; }
        public int fullPointsTest { get; set; }
        //kokeet/näyttelyt yhteensä, harrastusdoggi/kultadoggi pisteet, ...?

        public Dog()
        {
        }

        public Dog(string virName, string kutsName, int basePoint)
        {
            VirName = virName;
            KutsName = kutsName;
            BasePoints = basePoint;
        }
    }
}
