/// <summary>
///     Create simple projects to practice semaphores. philosopher problem eventually
/// </summary>

using System;
using System.Threading;
using System.Windows.Forms;

namespace Semaphores
{
    public partial class mutualExclusion : Form
    {
        static Random randomNum = new Random();

        private static EventWaitHandle eventWaitHandle;

        public int Counter = 0;

        public System.Timers.Timer timer = new System.Timers.Timer(100);

        public mutualExclusion()
        {
            /// Init the Semaphore needed for this simple example
            /// Semaphore eventWaitHanle = new Semaphore();
            eventWaitHandle = new EventWaitHandle(true, EventResetMode.AutoReset);

            timer.Elapsed += Timer_Elapsed;
            InitializeComponent();
            /// Need to have both forks to eat something
            /// Right now it is just a testLock though :)

            InitThreads();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            /// used to refresh the view in my windows application. Probably 
            ///     should have been a console application but oh well
            ConsoleLogger.Refresh();
        }


        /// <summary>
        ///     Creates philosopher threads
        /// </summary>
        public void InitThreads()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread( 
                                new ParameterizedThreadStart(Philosopher));

                thread.Start(i);
            }
        }


        /// <summary>
        ///     Used to write to a control with multiple threads. 
        /// </summary>
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


        /// <summary>
        ///     Philosopher method, we can have as many philosophers as we want!
        ///     Create a random amount of time for each process to wait before
        ///     trying to "hold" their resource. Also pick a random number of
        ///     iterations before the process stops. 
        /// </summary>
        /// <param name="data"></param>
        public void Philosopher(Object data)
        {
            /// index of thread/process passed with the method from the thread creation
            int index = (int)data;
            string name = "Philosohper" + (++index);

            int iteration = 0;
            int count     = randomNum.Next(1, 5);

            while (iteration < count)
            {
                Thread.Sleep(randomNum.Next(200));

                /// Bellow is the closest example to wait(mutex) I could find, it is also thread
                ///     safe!
                ///     
                /// Interlocked.Increment(ref threadCount);

                /// wait(eventHandler); and blocks if >= 0
                eventWaitHandle.WaitOne();

                /// < Critical_Section >
                {
                    /// Calls method AppendText that allows me to use multiple threads to write
                    ///     to a control. 
                    AppendText(name + " Holds the resource\n");


                    /// sleep some arbitrary amount of time. 
                    int resourceTime = randomNum.Next(150);
                    Thread.Sleep(resourceTime);

                    AppendText(name + " put the resource down in " + resourceTime + " miliseconds \n");
                    AppendText(name + " has itereated " + ++iteration + " times \n \n");
                }
                /// </ Critical_Section >

                /// Signal(EventWaitHandle);
                eventWaitHandle.Set();
            }
        }
    }
}
