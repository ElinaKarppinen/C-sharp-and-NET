using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pistelaskuri
{
    public class WriteDog : StreamWriter
    {
        string row;

        public WriteDog(string filename)
            : base(filename, true)
        {
        }

        public void WriteDogRow(Dog dog)
        {
            StringBuilder builder = new StringBuilder();
            string value;

            value = dog.VirName;
            builder.Append(value);
            builder.Append(',');
            value = dog.KutsName;
            builder.Append(value);
            builder.Append(',');
            value = dog.BasePoints.ToString();
            builder.Append(value);               
            row = builder.ToString();
            WriteLine(row);
        }
    }

    public class ReadDog : StreamReader
    {
        public ReadDog(string filename)
            : base(filename)
        {
        }

        public List<Dog> ReadDogRow()
        {
            string row;
            List<Dog> list = new List<Dog>();

            while ((row = ReadLine()) != null)
            {
                string[] words = row.Split(',');
                Dog doggi = new Dog();
                doggi.VirName = words[0];
                doggi.KutsName = words[1];
                doggi.BasePoints = int.Parse(words[2]);
                list.Add(doggi);
            }
            return list;
        }

    }

    public class WriteTest : StreamWriter
    {
        string row;

        public WriteTest(string filename)
            : base(filename, true)
        {
        }

        public void WriteTestRow(Test test)
        {
            StringBuilder builder = new StringBuilder();
            string value;

            value = test.Date;
            builder.Append(value);
            builder.Append(',');
            value = test.Place;
            builder.Append(value);
            builder.Append(',');
            value = test.Type;
            builder.Append(value);
            builder.Append(',');
            value = test.TestResult;
            builder.Append(value);
            builder.Append(',');
            value = test.TestPoints.ToString();
            builder.Append(value);
            builder.Append(',');
            value = test.TestSija;
            builder.Append(value);
            builder.Append(',');
            value = test.KisaPoints.ToString();
            builder.Append(value);
            builder.Append(',');
            value = test.Doggi.VirName;
            builder.Append(value);
            builder.Append(',');
            value = test.Doggi.KutsName;
            builder.Append(value);

            row = builder.ToString();
            WriteLine(row);
        }
    }

    public class ReadTest : StreamReader
    {
        public ReadTest(string filename)
            : base(filename)
        {
        }

        public List<Test> ReadTestRow()
        {
            string row;
            List<Test> list = new List<Test>();

            while ((row = ReadLine()) != null)
            {
                string[] words = row.Split(',');
                Test test = new Test();
                test.Date = words[0];
                test.Place = words[1];
                test.Type = words[2];
                test.TestResult = words[3];
                test.TestPoints = int.Parse(words[4]);
                test.TestSija = words[5];
                test.KisaPoints = int.Parse(words[6]);
                Dog dog = new Dog();

                dog.VirName = words[7];
                dog.KutsName = words[8];
                test.Doggi = dog;
                list.Add(test);
            }
            return list;
        }
    }

    public class WriteShow : StreamWriter
    {
        string row;

        public WriteShow(string filename)
            : base(filename, true)
        {
        }

        public void WriteShowRow(Show show)
        {
            StringBuilder builder = new StringBuilder();
            string value;
            
            value = show.Date;
            builder.Append(value);
            builder.Append(',');
            value = show.Place;
            builder.Append(value);
            builder.Append(',');
            value = show.Type;
            builder.Append(value);
            builder.Append(',');
            value = show.Placing;
            builder.Append(value);
            builder.Append(',');
            value = show.Points.ToString();
            builder.Append(value);
            builder.Append(',');
            value = show.Doggi.VirName;
            builder.Append(value);
            builder.Append(',');
            value = show.Doggi.KutsName;
            builder.Append(value);

            row = builder.ToString();
            WriteLine(row);
        }
    }

    public class ReadShow : StreamReader
    {
        public ReadShow(string filename)
            : base(filename)
        {
        }

        public List<Show> ReadShowRow()
        {
            string row;
            List<Show> list = new List<Show>();

            while ((row = ReadLine()) != null)
            {
                string[] words = row.Split(',');
                Show show = new Show();
                show.Date = words[0];
                show.Place = words[1];
                show.Type = words[2];
                show.Placing = words[3];
                show.Points = int.Parse(words[4]);
                Dog dog = new Dog();

                dog.VirName = words[5];
                dog.KutsName = words[6];
                show.Doggi = dog;
                list.Add(show);
            }
            return list;
        }
    }
}
