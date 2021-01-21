using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class File
    {
        private StreamWriter writer;
        private StreamReader reader;

        public void WriteData(List<List<int>> y, string fileName)
        {

            writer = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
            writer.WriteLine(y.Count.ToString() + " " + y[0].Count.ToString());
            for (int i = 0; i < y.Count; i++)
            {
                for (int j = 0; j < y[i].Count; j++)
                {
                    writer.Write(y[i][j].ToString() + " ");
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        public List<List<int>> ReadData(string fileName)
        {
            List<List<int>> y = new List<List<int>>();
            int n, m;

            reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            while (!reader.EndOfStream)
            {
                char[] separator = { ' ' };
                string[] vals = reader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                n = Convert.ToInt32(vals[0]);
                m = Convert.ToInt32(vals[1]);

                for (int i = 0; i < n; i++)
                {
                    List<int> list = new List<int>();
                    vals = reader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < m; j++)
                    {
                        list.Add(Convert.ToInt32(vals[j]));
                    }
                    y.Add(list);
                }
            }
            reader.Close();
            return y;
        }
    }
}
