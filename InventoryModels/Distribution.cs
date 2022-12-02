using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class Distribution
    {
        public Distribution()
        {

        }
        public int Value { get; set; }
        public decimal Probability { get; set; }
        public decimal CummProbability { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }

        public static void FillTableDemandDistribution(List<Distribution> demad)
        {
            int count = demad.Count();
            for(int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    demad[i].CummProbability = demad[i].Probability;
                    demad[i].MinRange = 1;
                    int tmp = (int)((demad[i].CummProbability * 100));
                    demad[i].MaxRange = tmp;
                }
                else
                {
                    demad[i].CummProbability = demad[i-1].CummProbability + demad[i].Probability;
                    demad[i].MinRange = demad[i - 1].MaxRange + 1;
                    int tmp = (int)((demad[i].CummProbability * 100));
                    demad[i].MaxRange = tmp;
                }
            }
        }
        public static void FillTableLeadDaysDistribution(List<Distribution>leadDays)
        {
            int count = leadDays.Count();
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    leadDays[i].CummProbability = leadDays[i].Probability;
                    leadDays[i].MinRange = 1;
                    int tmp = (int)((leadDays[i].CummProbability * 100));
                    leadDays[i].MaxRange = tmp;
                }
                else
                {
                    leadDays[i].CummProbability = leadDays[i - 1].CummProbability + leadDays[i].Probability;
                    leadDays[i].MinRange = leadDays[i - 1].MaxRange + 1;
                    int tmp = (int)((leadDays[i].CummProbability * 100));
                    leadDays[i].MaxRange = tmp;
                }
            }
        }
    }
}
