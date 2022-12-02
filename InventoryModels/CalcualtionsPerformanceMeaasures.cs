using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    class CalcualtionsPerformanceMeaasures
    {
        public static decimal CalculateEndingInventoryAverage(SimulationSystem sys)
        {
            decimal sum = 0, average ;
            int size = sys.NumberOfDays;
            for(int i = 0; i < size; i++)
            {
                sum += sys.SimulationTable[i].EndingInventory;
            }
            average = sum / size;
            return average;
        }
        public static decimal CalculateShortageQuantityAverage(SimulationSystem sys)
        {
            decimal sum = 0, average;
            int size = sys.NumberOfDays;
            for (int i = 0; i < size; i++)
            {
                sum += sys.SimulationTable[i].ShortageQuantity;
            }
            average = sum / size;
            return average;
        }
    }
}
