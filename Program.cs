using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Process_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            int SelectedOption = 0;
            string SelectedOption_STR = string.Empty;
            bool RUN_PROCESS_MANGER_PROGRAM = true;
            string Options = @"
1 - SHOW ALL PROCESS(LIMIT 100)
2 - KILL A PROCESS
3 - FIND THE ID OF A PROCESS BY NAME
4 - Start a Process
";
            Console.Title = "ADVANCED PROCESS MANAGER";
            Console.WriteLine("WELCOME TO PROCESS MANAGER");

            do
            {
                Console.Write(Options);
                Console.Write("Option:"); SelectedOption_STR = Console.ReadLine();
                if (int.TryParse(SelectedOption_STR, out var Option))
                    SelectedOption = Option;
                else
                    SelectedOption = 0;
                switch (SelectedOption)
                {
                    case 1:
                        ShowCurrentProcess();
                        break;
                    case 2:
                        Console.Write("ENTER THE PROCESS ID:");var STR_ID = Console.ReadLine();
                        if (int.TryParse(STR_ID, out var PROCESSID))
                            Kill_A_Process(PROCESSID);
                        else
                            Console.Write("INVALID PROCESS ID");
                        break;
                    case 3:
                        Console.Write("ENTER THE PROCESS NAME:"); var FIND_PROCESSNAME = Console.ReadLine();
                        Find_A_ProcessByName(FIND_PROCESSNAME);
                        break;
                    case 4:
                        Console.Write("ENTER THE PROCESS NAME:"); var START_PROCESSNAME = Console.ReadLine();
                        Start_A_PROCESS(START_PROCESSNAME);
                        break;
                    default:
                        break;
                }
            } while (RUN_PROCESS_MANGER_PROGRAM);
        }
        static void ShowCurrentProcess(int Limit=100)
        {
            var Current_Process =Process.GetProcesses().Take(Limit);
            foreach (Process _process in Current_Process)
                Console.WriteLine($"Name: {_process.ProcessName} PID:{_process.Id}");
        }
        static void Kill_A_Process(int ID)
        {
            var SelectedProcessByID = Process.GetProcessById(ID);
            if (SelectedProcessByID != null)
                SelectedProcessByID.Kill();
        }
        static void Find_A_ProcessByName(string NAME)
        {
            var SelectedPRocessByName = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower().Contains(NAME.ToLower()));
            if (SelectedPRocessByName != null)
            {
                Console.WriteLine($"{SelectedPRocessByName.ProcessName} | {SelectedPRocessByName.Id} | (EXECUTION TIME:{SelectedPRocessByName.TotalProcessorTime.TotalMinutes} Minutes)");
            }
            else
            {
                Console.WriteLine($"INVALID PROCESS NAME \"{NAME}\"");
            }
        }
        static void Start_A_PROCESS(string NAME)
        {
            try
            {
                Process.Start(NAME);
            }
            catch (Exception)
            {
                Console.WriteLine("CANT START THE PROCESS");
            }
        }
    }
}
