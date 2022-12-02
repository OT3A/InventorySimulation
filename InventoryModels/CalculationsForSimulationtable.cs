using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    class CalculationsForSimulationtable
    {
        public static int qu { get; set; }
        public static int CalculateCycleNumber(int cycle, int daywithecycle)
        {
            int c = cycle;
            if(daywithecycle==1)
            {
                c++;
            }
            return c;
        }
        public static int CalculateDayWithCycle(int day ,int ReviewPeriod)
        {
            int c;
            if (day % ReviewPeriod == 0)
            {
                c = 5;
            }
            else {
                c = day % ReviewPeriod;
            }
            return c;
        }
        public static int CalculateBeginningInventory(int end,int daysUntill,int order)
        {
            int x;
            if(daysUntill == 0 && order!=0)
            {
                x = end + order;
                CalculationsForSimulationtable.qu = 0;
            }
            else
            {
                x = end;
            }
            return x;
        }
        public static int SelectDemand(int rand ,SimulationSystem sys)
        {
            int count = sys.DemandDistribution.Count();
            for(int i = 0; i < count; i++)
            {
                if(sys.DemandDistribution[i].MinRange<= rand && rand <= sys.DemandDistribution[i].MaxRange)
                {
                    return sys.DemandDistribution[i].Value;
                }
            }
            return 0;
        }
        public static int CalculateEndingInventory(int Demand , int BeginningInventory,int shortage)
        {
            if (BeginningInventory >= (Demand+shortage))
            {
                return BeginningInventory - (Demand + shortage);
            }
            return 0;
        }
        public static int CalculateShortageQuantity(int Demand, int BeginningInventory,int shortage)
        {
            if(BeginningInventory < Demand)
            {
                return (Demand - BeginningInventory)+ shortage;
            }
            return 0;
        }
        public static int CalculateOrderQuantity(int day, int ReviewPeriod,int orderUpToLevel,int endingInv,int shortage)
        {
            if (day % ReviewPeriod == 0)
            {
                qu = (orderUpToLevel - endingInv + shortage);
                return (orderUpToLevel - endingInv + shortage);
            }
            return 0;
        }
        public static int SelectDay(int rand, SimulationSystem sys ,int day)
        {
            int count = sys.DemandDistribution.Count();
            if (day % sys.ReviewPeriod == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (sys.LeadDaysDistribution[i].MinRange <= rand && rand <= sys.LeadDaysDistribution[i].MaxRange)
                    {
                        return sys.LeadDaysDistribution[i].Value;
                    }
                }
            }
            return 0;
        }
        public static int CalculateDaysUntillOrderArrives(int day , SimulationSystem sys, int leadDays)
        {
            if (day % sys.ReviewPeriod == 0)
            {
                return leadDays;
            }
            else if (leadDays != 0)
            {
                int tmp = leadDays-1;
                return tmp;
            }
            return 0;
        }
    }
}
