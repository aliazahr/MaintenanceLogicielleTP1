using System;
using System.Threading;

namespace Util
{
    public class NetworkDelay
    {
        private static readonly Random _random = new Random();
        private static readonly object _lock = new object();

        private const int _minDelay = 1000;
        private const int _maxDelay = 5000;

        public static int MinDelay
        {
            get { return _minDelay; }
        }

        public static int MaxDelay
        {
            get { return _maxDelay; }
        }

        static public async Task SimulateNetworkDelayAsync()
        {
            int delay;
            lock (_lock)
            {
                delay = _random.Next(_minDelay, _maxDelay);
            }

            await Task.Delay(delay);
        }

        static public async Task<int> PayEntityAsync(int balance, int income)
        {
            await SimulateNetworkDelayAsync();
            
            int newBalance = balance + income;
            return newBalance; // Cette méthode ne devrait pas retourner de message
        }
    }
}
