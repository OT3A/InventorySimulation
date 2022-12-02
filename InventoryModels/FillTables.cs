using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class FillTables
    {
        FillTables() { }
        public static void FillSimulationTable(SimulationSystem sys)
        {
            Random rd = new Random();
            int cy = 1;
            int shortagetmp = 0;
            int leadday =0;
            int copy = 0;
            int size = sys.NumberOfDays;
            for(int i = 0; i < size; i++)
            {
                SimulationCase row = new SimulationCase();
                row.Day = i + 1;
                //if (i == 0) {
                //    row.Cycle = 1;
                //}
                //else
                //{
                row.DayWithinCycle = CalculationsForSimulationtable.CalculateDayWithCycle(row.Day, sys.ReviewPeriod);
                if (i == 0)
                {
                    cy = 1;
                    row.Cycle = cy;
                }
                else {
                    cy = CalculationsForSimulationtable.CalculateCycleNumber( cy,row.DayWithinCycle);
                    row.Cycle = cy;
                }
                //}
                if (i == 0)
                {
                    row.BeginningInventory = sys.StartInventoryQuantity;

                }
                else
                {
                    row.BeginningInventory = CalculationsForSimulationtable.CalculateBeginningInventory(sys.SimulationTable[i - 1].EndingInventory, sys.SimulationTable[i - 1].DaysUntillOrderArrives, CalculationsForSimulationtable.qu);
                    
                }
                copy = row.BeginningInventory;
                if (row.BeginningInventory != 0)
                {
                    if (row.BeginningInventory <= shortagetmp)
                    {
                        shortagetmp -= row.BeginningInventory;
                        copy = 0;
                    }
                    else
                    {
                        copy = row.BeginningInventory - shortagetmp;
                        shortagetmp = 0;        
                    }
                }
                row.RandomDemand = rd.Next(1, 100);
                row.Demand = CalculationsForSimulationtable.SelectDemand(row.RandomDemand, sys);
                if(i==0)
                row.EndingInventory = CalculationsForSimulationtable.CalculateEndingInventory(row.Demand, copy,0);
                else
                    row.EndingInventory = CalculationsForSimulationtable.CalculateEndingInventory(row.Demand, copy, 0);
                if (i == 0)
                {

                    shortagetmp += CalculationsForSimulationtable.CalculateShortageQuantity(row.Demand, copy, 0);
                    row.ShortageQuantity = shortagetmp;
                }
                else
                {
                    shortagetmp += CalculationsForSimulationtable.CalculateShortageQuantity(row.Demand, copy,0);
                    row.ShortageQuantity = shortagetmp;
                    //row.ShortageQuantity = CalculationsForSimulationtable.CalculateShortageQuantity(row.Demand, row.BeginningInventory,sys.SimulationTable[i-1].ShortageQuantity);
                }
                if (i == 0)
                {
                    CalculationsForSimulationtable.qu = sys.StartOrderQuantity;
                    row.OrderQuantity = 0;
                }
                else
                {
                    row.OrderQuantity = CalculationsForSimulationtable.CalculateOrderQuantity(row.Day, sys.ReviewPeriod, sys.OrderUpTo, row.EndingInventory, row.ShortageQuantity);
                }
                if (row.Day % sys.ReviewPeriod == 0)
                {
                    row.RandomLeadDays = rd.Next(1, 100);
                }
                row.LeadDays = CalculationsForSimulationtable.SelectDay(row.RandomLeadDays, sys, row.Day);
                if (row.LeadDays != 0)
                {
                    leadday = CalculationsForSimulationtable.SelectDay(row.RandomLeadDays, sys, row.Day);
                }
                if (i == 0)
                {
                    leadday = sys.StartLeadDays - 1;
                    row.DaysUntillOrderArrives = leadday;
                }
                else
                {
                    leadday = CalculationsForSimulationtable.CalculateDaysUntillOrderArrives(row.Day, sys, leadday);
                    row.DaysUntillOrderArrives = leadday;
                }
                sys.SimulationTable.Add(row);
            }
        }
        public static void FillPerformanceMeasures(SimulationSystem sys)
        {
            sys.PerformanceMeasures.EndingInventoryAverage = CalcualtionsPerformanceMeaasures.CalculateEndingInventoryAverage(sys);
            sys.PerformanceMeasures.ShortageQuantityAverage = CalcualtionsPerformanceMeaasures.CalculateShortageQuantityAverage(sys);
        }
    }
}
