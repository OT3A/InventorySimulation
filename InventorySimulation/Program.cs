using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryTesting;
using System.Windows.Forms;
using InventoryModels;
namespace InventorySimulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String path = "D:\\PDF\\4th year\\first term\\Symister 1\\Modeling and Simulations\\New\\Lab7_Task 3\\[Students]_Template\\InventorySimulation\\InventorySimulation\\TestCases\\TestCase4.txt";
            SimulationSystem system = new SimulationSystem();
            ReadDateFromText data = new ReadDateFromText(path, system);
            FillTables.FillSimulationTable(system);
            FillTables.FillPerformanceMeasures(system);
            String testingResult = TestingManager.Test(system, Constants.FileNames.TestCase4);
            MessageBox.Show(testingResult);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(system));
        }
    }
}
