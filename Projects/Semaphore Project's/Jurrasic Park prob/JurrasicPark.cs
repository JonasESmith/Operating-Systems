using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jurrasic_Park_prob
{
    class JurrasicPark
    {
        private static EventWaitHandle car;
        private static EventWaitHandle passenger;

        static void Main(string[] args)
        {
            car       = new EventWaitHandle(false, EventResetMode.AutoReset);
            passenger = new EventWaitHandle(false, EventResetMode.AutoReset);


            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(
                            new ParameterizedThreadStart(Passenger));

                thread.Start(i);
            }

            for (int i = 0; i < 3; i++)
            {
                    Thread thread = new Thread(
                                new ParameterizedThreadStart(Car));

                    thread.Start(i);
            }
        }

        static void Car(Object thread)
        {
            int index = (int)thread;

            while (true)
            {
                Console.WriteLine("Car{0} has arrived", index);
                Thread.Sleep(10);
                car.Set();
                passenger.WaitOne();

                /// Go on some trip
                /// drop off passenger

                Console.WriteLine("Some time has passed");
                Console.WriteLine("Passenger{0} has got out of car", index);
                Thread.Sleep(10);
            }
        }

        static void Passenger(Object thread)
        {
            int index = (int)thread;

            Console.WriteLine("Passenger{0} has arrived", index);
            Thread.Sleep(10);
            car.WaitOne();
            /// get in car
            Console.WriteLine("Passenger{0} has got in car", index);
            Thread.Sleep(10);
            passenger.Set();
        }
    }
}
