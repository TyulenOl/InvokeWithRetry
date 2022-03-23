using System;

namespace InvokeWithRetry
{
    public static class Program
    {
        static void Main()
        {
        
        }
        
        public static bool InvokeWithRetry(Action action, int maxAttempts)
        {
            for (var i = 0; i < maxAttempts; i++)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception)
                {
                    if (i == maxAttempts - 1)
                        return false;
                }
            }
            return true;
        }
    }
}