using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    //1.creating delegate - see Worker.cs class
    public delegate int BizRulesDelegate(int x, int y);
        
    class Program
    {      
        static void Main(string[] args)
        {
            var custs = new List<Customer>
            {
                new Customer { City = "Phoenix", FirstName = "John", LastName = "Doe", ID = 1},
                new Customer { City = "Phoenix", FirstName = "Jane", LastName = "Doe", ID = 500},
                new Customer { City = "Seattle", FirstName = "Suki", LastName = "Pizzoro", ID = 3},
                new Customer { City = "New York City", FirstName = "Michelle", LastName = "Smith", ID = 4},
            };

            var phxCust = custs.Where(c => c.City == "Phoenix").OrderBy(c => c.FirstName);
            foreach (var phx in phxCust)
            {
                Console.WriteLine(phx.FirstName + " from " + phx.City);
            }
            

            //3.creating delegate variables
            //WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
            //WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
            //WorkPerformedHandler del3 = new WorkPerformedHandler(WorkPerformed3);

            //del1 += del2 + del3;

            //int finalHours = del1(10, WorkType.GenerateReports);
            //writes out only value for the last delegate
            //even tho 3 of 'em were invoked
            //Console.WriteLine(finalHours);

            var data = new ProcessData();
            BizRulesDelegate addDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;
            //data.Process(2, 3, multiplyDel);

            Func<int , int, int> funcAddDel = (x, y) => x + y;
            Func<int, int, int> funcMultiplyDel = (x, y) => x * y;
            data.ProcessFunc(2, 3, funcMultiplyDel);
            
            

            Action<int, int> myAddAction = (x, y) => Console.WriteLine(x + y);
            Action<int, int> myMultiplyAction = (x, y) => Console.WriteLine(x * y);
            //data.ProcessAction(2, 5, myMultiplyAction);

            var worker = new Worker();
            worker.WorkPerformed += (s,e) => Console.WriteLine("Hours worked: " + e.Hours + " hour(s) doing " + e.WorkType);  /*option 2 new EventHandler<WorkPerformedEventArgs>(Worker_WorkPerformed);*/
                       
            worker.WorkCompleted += (s,e) => Console.WriteLine("Worker is done"); /*option 2 new EventHandler(Worker_WorkCompleted);*/

            worker.DoWork(8, WorkType.GenerateReports);

            Console.ReadKey();
        }

        //static void Worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        //{
        //    Console.WriteLine("Hours worked: " + e.Hours + " " + e.WorkType);
        //}

        //static void Worker_WorkCompleted(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Worker is done");
        //}

        //this method doesn't know which delegate its invoking
        //static void DoWork(WorkPerformedHandler del)
        //{
        //   // del(5, WorkType.Golf);
        //}
        
        //2.writing method with the same signature
        static int WorkPerformed1(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPeformed1 called " + "with " + hours.ToString() + " hours");
            return hours + 1;
        }
        static int WorkPerformed2(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPeformed2 called " + "with " + hours.ToString() + " hours");
            return hours + 2;
        }
        static int WorkPerformed3(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPeformed3 called " + "with " + hours.ToString() + " hours");
            return hours + 3;
        }
    }
    public enum WorkType
    {
       GoToMeetings,
       Golf,
       GenerateReports
    }
}
