// /***************************************************************************/
// Datum:        17:41, 19.12.2013
// Project:      PathFindingSample/FinalDarkness - Performance/HiPerfTimer.cs
// Copyright (c) Kai Cissarek
// /***************************************************************************/
#region

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

#endregion

namespace DrawTextTest
{
    public class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long startTime, stopTime;
        private readonly long freq;

        // Constructor
        public HiPerfTimer()
        {
            this.startTime = 0;
            this.stopTime = 0;

            // high-performance counter not supported 
            if (QueryPerformanceFrequency(out this.freq) == false)
                throw new Win32Exception();
        }

        // Start the timer
        public void Start()
        {
            // lets do the waiting threads there work
            Thread.Sleep(0);
            QueryPerformanceCounter(out this.startTime);
        }

        // Stop the timer
        public void Stop()
        {
            QueryPerformanceCounter(out this.stopTime);
        }

        // Returns the duration of the timer (in seconds)
        public double Duration
        {
            get { return (this.stopTime - this.startTime) / (double)this.freq; }
        }
    }
}