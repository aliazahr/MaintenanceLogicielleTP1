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

        // Async pour mieux performance quand plusieurs paiements se font en même temps
        static public async Task SimulateNetworkDelayAsync()
        {
            try
            {
                int delay;
                lock (_lock)
                {
                    delay = _random.Next(_minDelay, _maxDelay);
                }

                // Protéger contre le comportement inattendu du Random
                if (delay < 0 || delay > 30000) // 30 seconds max
                {
                    throw new InvalidOperationException($"Generated delay is out of acceptable range: {delay} ms");
                }

                await Task.Delay(delay);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("\nNetwork delay has failed", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("\nAn error occurred while simulating network delay.", ex);
            }
        }

        static public async Task<int> PayEntityAsync(int balance, int income)
        {
            if (balance < 0 || income < 0)
            {
                throw new ArgumentException("Balance and income must be non-negative.");
            }

            if (balance > int.MaxValue - income)
            {
                throw new OverflowException("Adding income would cause overflow.");
            }

            try
            {
                await SimulateNetworkDelayAsync();

                // Pour éviter integer overflow: https://stackoverflow.com/questions/2954970/best-way-to-handle-integer-overflow-in-c
                checked
                {
                    int newBalance = balance + income;
                    return newBalance; // Cette méthode ne devrait pas retourner de message
                }
            }
            catch (OverflowException)
            {
                throw new InvalidOperationException($"\nPayment operation caused an overflow: balance={balance}, income={income}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("\nAn error occurred during the payment operation.", ex);
            }
        }

        internal static void Configure(int minMs, int maxMs)
        {
            throw new NotImplementedException();
        }
    }
}
