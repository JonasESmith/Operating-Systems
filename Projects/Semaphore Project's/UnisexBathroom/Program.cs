using System;
using System.Threading;

namespace UnisexBathroom
{
    class Program
    {
        private static EventWaitHandle turnStyle;

        private static EventWaitHandle car;
        private static EventWaitHandle passenger;
        private static EventWaitHandle maleS;
        private static EventWaitHandle femS;

        public static long numMale = 0, numFemale = 0;

        static void Main(string[] args)
        {
            car = new EventWaitHandle(false, EventResetMode.AutoReset);
            passenger = new EventWaitHandle(false, EventResetMode.AutoReset);

            turnStyle = new EventWaitHandle(true, EventResetMode.AutoReset);

            maleS = new EventWaitHandle(false, EventResetMode.AutoReset);
            femS = new EventWaitHandle(false, EventResetMode.AutoReset);


            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(
                            new ParameterizedThreadStart(Male));

                thread.Start(i);
            }

            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(
                            new ParameterizedThreadStart(Female));

                thread.Start(i);
            }

            for (int i = 0; i < 3; i++)
            {
                maleS.Set();
                femS.Set();
            }


            Console.ReadLine();
        }

        static void Female(Object thread)
        {
            int counter = 0;
            int index = (int)thread;

            // A male or female has a life of doing things then waiting in line at the
            // restroom.

            while (counter < 5)
            {

                /// <summary>
                ///     There is definitely a problem here where a few of the females
                ///     specifically the first female to reach the door blocks out and
                ///     never gets to go because she is almost always blocked. until
                ///     the end when she is released. 
                /// </summary>

                if (Interlocked.Read(ref numFemale) == 0)
                {
                    Interlocked.Increment(ref numFemale);
                    turnStyle.WaitOne();
                }
                else
                {
                    Interlocked.Increment(ref numFemale);
                }

                femS.WaitOne();

                // could add a timer here to compliment the time needed for each process to finish
                // and add some variation when the process picks between a female or male to get in line. 

                Thread.Sleep(10);
                // do stuff();
                Console.WriteLine("Female{0} has entered", index);

                Interlocked.Decrement(ref numFemale);
                femS.Set();


                if (Interlocked.Read(ref numFemale) == 0)
                {
                    turnStyle.Set();
                }

                counter++;
            }
        }

        static void Male(Object thread)
        {
            int counter = 0;
            int index = (int)thread;

            // A male or female has a life of doing things then waiting in line at the
            // restroom.

            while (counter < 5)
            {

                if (Interlocked.Read(ref numMale) == 0)
                {
                    Interlocked.Increment(ref numMale);
                    turnStyle.WaitOne();
                }
                else
                {
                    Interlocked.Increment(ref numMale);
                }
                maleS.WaitOne();

                // could add a timer here to compliment the time needed for each process to finish
                // and add some variation when the process picks between a female or male to get in line. 

                Thread.Sleep(10);
                // do stuff();
                Console.WriteLine("Male{0} has entered", index);

                Interlocked.Decrement(ref numMale);
                maleS.Set();


                if (Interlocked.Read(ref numMale) == 0)
                {
                    turnStyle.Set();
                }

                counter++;
            }
        }
    }
}
