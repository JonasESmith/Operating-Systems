using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphores
{
    public class Semaphore
    {
        private int semaphoreValue;

        public Semaphore(int value)
        {
            semaphoreValue = value;
        }

        public bool Wait()
        {
            bool value = true;
            semaphoreValue--;

            if(semaphoreValue < 0)
            {
                semaphoreValue = 0;
                value = false;
            }

            return value;
        }

        public void Signal()
        {
            semaphoreValue++;

            if(semaphoreValue <= 0)
            {
                /// WakeUp.Process();
            }
        }

        public bool GetSemaphoreStatues
        {
             get
            {
                bool value = false;

                if(semaphoreValue > 0)
                {
                    value = true;    
                }

                return value;
            }
        }
    }
}
