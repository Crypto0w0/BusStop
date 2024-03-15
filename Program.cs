using System;
using System.Threading;

class Program
{
    static event EventHandler<BusArrivalEventArgs> BusArrived;
    
    const int busPassengers = 30;
    static int stationPassengers = 0;
    static int onthebus = 0;
    static Thread busStop = new Thread(StationWorks);
    
    static void Main()
    {
        busStop.Start();
        StationWorks();
    }

    static void StationWorks()
    {
        while (true)
        {
            Random r = new Random(1);
            if (r.Next(1, 5) == 1)
            {
                stationPassengers += r.Next(1, 8);
            }
            else if (r.Next(1, 5) == 2)
            {
                if (stationPassengers + onthebus <= 30)
                {
                    onthebus += stationPassengers;
                    stationPassengers = 0;
                }
                else
                {
                    while (onthebus <= 30)
                    {
                        onthebus++;
                        stationPassengers--;
                    }
                }
                BusArrived?.Invoke(busStop, new BusArrivalEventArgs(stationPassengers));
            }
        }
    }
    class BusArrivalEventArgs : EventArgs
    {
        public int PassCount;
        public BusArrivalEventArgs(int passengers)
        {
            PassCount = passengers;
        }
    }
}