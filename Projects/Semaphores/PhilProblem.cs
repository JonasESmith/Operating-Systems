/// <summary>
///     Create simple projects to practice semaphores. philosopher
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Semaphores
{
    public partial class PhilProblem : Form
    {
        public int Counter = 0;
        public int philosopherCount = 0;

        // Create list of Philosophers threads to run in conjunction. 
        public List<Thread> philoList = new List<Thread>();


        // Create mutex, and testlock semaphores for testing
        public Semaphore mutex    = new Semaphore(1);
        public Semaphore testLock = new Semaphore(1);

        public System.Timers.Timer timer = new System.Timers.Timer(100);

        public PhilProblem()
        {
            timer.Elapsed += Timer_Elapsed;
            InitializeComponent();
            /// Need to have both forks to eat something

            InitPhilosopher();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ConsoleLogger.Refresh();
        }

        public void InitPhilosopher()
        {
            for(int i = 0; i < 2; i++)
            {
                philoList.Add(new Thread(Philosopher));

                philoList[i].Start();
            }
        }

        delegate void AppendTextDelegate(string text);

        public void AppendText(string text)
        {
            if (ConsoleLogger.InvokeRequired)
            {
                ConsoleLogger.Invoke(new AppendTextDelegate(this.AppendText), new object[] { text });
            }
            else
            {
                ConsoleLogger.Text = ConsoleLogger.Text += text;
            }
        }

        ManualResetEvent m_stopThreadsEvent = new ManualResetEvent(false);

        public void Philosopher()
        {
            Random rnd = new Random();
            philosopherCount++;
            string name = "Philosohper" + philosopherCount;

            bool testValue = false;

            for(; ;)
            {
                testValue = testLock.Wait();
                while(testValue)
                {
                    int n = rnd.Next(100, 5000);
                    Thread.Sleep(n);

                    AppendText(name + " picked up data. \n");
                    AppendText(name + " put data down. at " + n + "\n");
                    testLock.Signal();

                }
            }
        }

        private void PhilProblem_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_stopThreadsEvent.Set();
        }
    }
}
