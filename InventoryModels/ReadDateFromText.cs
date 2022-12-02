using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class ReadDateFromText
    {
        ReadDateFromText() { }
       public ReadDateFromText(String pathData, SimulationSystem sys)
        {
            lines = File.ReadAllLines(pathData);
            int size = lines.Count();
            for(int i = 0; i < size; i++)
            {
                if (lines[i] == null || lines[i].Length == 0 || lines[i] == "")
                {
                    continue;
                }
                else if (lines[i] == "OrderUpTo")
                {
                    sys.OrderUpTo =int.Parse( lines[i + 1]);
                }
                else if(lines[i]== "ReviewPeriod")
                {
                    sys.ReviewPeriod = int.Parse(lines[i + 1]);
                }
                else if (lines[i]== "StartInventoryQuantity")
                {
                    sys.StartInventoryQuantity = int.Parse(lines[i + 1]);
                }
                else if (lines[i]== "StartLeadDays")
                {
                    sys.StartLeadDays = int.Parse(lines[i + 1]);
                }
                else if (lines[i] == "StartOrderQuantity")
                {
                    sys.StartOrderQuantity = int.Parse(lines[i + 1]);
                }
                else if (lines[i]== "NumberOfDays")
                {
                    sys.NumberOfDays = int.Parse(lines[i + 1]);
                }
                else if (lines[i]== "DemandDistribution")
                {
                    for(int j = i + 1; j < size; j++)
                    {
                        if (lines[j] == null || lines[j].Length == 0 || lines[j] == "")
                            break;
                        int demand = int.Parse(lines[j].Split(',')[0]);
                        decimal p = decimal.Parse(lines[j].Split(',')[1]);
                        Distribution obj = new Distribution();
                        obj.Value = demand;
                        obj.Probability = p;
                        sys.DemandDistribution.Add(obj);
                    }
                }
                else if (lines[i]== "LeadDaysDistribution")
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        if (lines[j] == null || lines[j].Length == 0 || lines[j] == "")
                            break;
                        int demand = int.Parse(lines[j].Split(',')[0]);
                        decimal p = decimal.Parse(lines[j].Split(',')[1]);
                        Distribution obj = new Distribution();
                        obj.Value = demand;
                        obj.Probability = p;
                        sys.LeadDaysDistribution.Add(obj);
                    }
                }

            }
            Distribution.FillTableDemandDistribution(sys.DemandDistribution);
            Distribution.FillTableLeadDaysDistribution(sys.LeadDaysDistribution);

        }
        public String[] lines { get; set; }
    }
}
