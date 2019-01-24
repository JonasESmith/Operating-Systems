/// <summary>
///     Create simple projects to practice semaphores. philosopher problem eventually
/// </summary>

using System;
using System.Threading;
using System.Windows.Forms;

namespace Semaphores
{
    public partial class BaboonProblem : Form
    {
        static Random randomNum = new Random();

        private static EventWaitHandle eventWaitHandle;
        private static EventWaitHandle rope = new EventWaitHandle(true, EventResetMode.AutoReset);

        public long Counter = 0;
        public long numGoingLeft = 0, numGoingRight = 0;

        public System.Timers.Timer timer = new System.Timers.Timer(100);

        public BaboonProblem()
        {
            /// Init the Semaphore needed for this simple example
            /// Semaphore eventWaitHanle = new Semaphore();
            eventWaitHandle = new EventWaitHandle(true, EventResetMode.AutoReset);

            /// We need to create a method that will randomly choose between two baboons
            ///     and either queue them or send them to the other side of the rope. 

            timer.Elapsed += Timer_Elapsed;
            InitializeComponent();
            /// Need to have both forks to eat something
            /// Right now it is just a testLock though :)

            

            InitBaboons();
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
        public void InitBaboons()
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    Thread thread = new Thread(
            //                    new ParameterizedThreadStart(BaboonGoingRight));

            //    thread.Start(i);
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    Thread thread = new Thread(
            //                    new ParameterizedThreadStart(BaboonGoingLeft));

            //    thread.Start(i);
            //}

            /// used to randomly select a number of baboons i
            /// 

            for (int i = 0; i < 15; i++)
            {
                Thread thread = new Thread(
                                new ParameterizedThreadStart(BaboonGoingLeft));

                thread.Start(i);
            }

            for (int i = 0; i < 25; i++)
            {
                if (randomNum.NextDouble() <= 0.5)
                {
                    Thread thread = new Thread(
                                new ParameterizedThreadStart(BaboonGoingRight));

                    thread.Start(i);
                }
                else
                {
                    Thread thread = new Thread(
                                new ParameterizedThreadStart(BaboonGoingLeft));

                    thread.Start(i);
                }
            }
        }


        /// <summary>
        ///     Used to write to a control with multiple threads. 
        /// </summary>
        delegate void AppendTextDelegate(string text);
        public void AppendText(string text)
        {
            Interlocked.Increment(ref Counter);

            if (ConsoleLogger.InvokeRequired)
            {
                ConsoleLogger.Invoke(new AppendTextDelegate(this.AppendText), new object[] { Interlocked.Read(ref Counter) + " " + text });
            }
            else
            {
                ConsoleLogger.Text = ConsoleLogger.Text += Interlocked.Read(ref Counter) + " " + text;
            }
        }


        public void BaboonGoingLeft(Object data)
        {
            int index = (int)data;

            string name = string.Format("Left  {0,2}",++index);

            retry:

            if (Interlocked.Read(ref numGoingLeft) < 2)
            {
                if (Interlocked.Read(ref numGoingLeft) == 0)
                {
                    rope.WaitOne();
                }

                Interlocked.Increment(ref numGoingLeft);

                /// <Critical>

                /// Add a thread.sleep(n) to add some actuall travel time

                AppendText(name + " Has crossed the rope\n");


                /// </Critical>

                Interlocked.Decrement(ref numGoingLeft);

                if(Interlocked.Read(ref numGoingLeft) == 0)
                {
                    rope.Set();
                }
            }
            else
            {
                goto retry;
            }
        }

        public void BaboonGoingRight(Object data)
        {
            int index = (int)data;
            string name = string.Format("Right {0,2}", ++index);

        retry :

            if (Interlocked.Read(ref numGoingRight) < 5)
            {
                if (Interlocked.Read(ref numGoingRight) == 0)
                {
                    rope.WaitOne();
                }

                Interlocked.Increment(ref numGoingRight);

                /// <Critical>


                AppendText(name + " Has crossed the rope\n");

                /// </Critical>

                Interlocked.Decrement(ref numGoingRight);

                if (Interlocked.Read(ref numGoingRight) == 0)
                {
                    rope.Set();
                }
            }
            else
            {
                goto retry;
            }
        }
    }
}
