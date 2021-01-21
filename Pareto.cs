using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Pareto
    {
        public int more, less, equal;
        public List<int> ind = new List<int>();
        private void Compare(List<int> v1, List<int> v2)
        {
            more = 0;
            less = 0;
            equal = 0;
            for (int i = 0; i < v1.Count; i++)
            {
                if (v1[i] > v2[i])
                    more++;
                else if (v1[i] < v2[i]) less++;
                else equal++;
            }
        }

        // возвращает истину если v1 >= v2
        private bool MoreOrEqual()
        {
            if (more >= 0 && less == 0)
                return true;
            else return false;
        }

        // y – список решений
        public void DeleteDominated(List<List<int>> y)
        {
            foreach (List<int> yi in y)
            {
                foreach (List<int> gj in y)
                {
                    if (!Equals(gj, yi))
                    {
                        Compare(gj, yi);
                        if (MoreOrEqual())
                        {
                            ind.Add(y.IndexOf(yi));
                            y.Remove(yi);
                            DeleteDominated(y);
                            return;
                        }
                        Compare(yi, gj);
                        if (MoreOrEqual())
                        {
                            ind.Add(y.IndexOf(gj));
                            y.Remove(gj);
                            DeleteDominated(y);
                            return;
                        }
                    }
                }
            }

        }

        public List<List<int>> GetParetoList(List<List<int>> y)
        {
            DeleteDominated(y);
            return y;
        }
    }
}
