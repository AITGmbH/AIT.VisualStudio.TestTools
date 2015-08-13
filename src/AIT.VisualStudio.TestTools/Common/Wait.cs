﻿namespace AIT.VisualStudio.TestTools.Common
{
    using System;

    /// <summary>
    /// Assert Extension methods.
    /// </summary>
    public static class Wait
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Waits until the action returns true.
        /// </summary>
        /// <exception cref="TimeoutException">Thrown when the timeout (30 seconds) exceeds.</exception>
        public static void UntilTrue(Func<bool> action)
        {
            UntilTrue(action, DefaultTimeout);
        }

        /// <summary>
        /// Waits until the action returns true.
        /// </summary>
        /// <exception cref="TimeoutException">Thrown when the timeout exceeds.</exception>
        public static void UntilTrue(Func<bool> action, TimeSpan timeout)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var retryUnil = DateTime.Now.Add(timeout);

            while (retryUnil >= DateTime.Now)
            {
                var result = action.Invoke();

                if (result)
                {
                    return;
                }
            }

            throw new TimeoutException();
        }

        /// <summary>
        /// Waits until the action returns false.
        /// </summary>
        /// <exception cref="TimeoutException">Thrown when the timeout (30 seconds) exceeds.</exception>
        public static void UntilFalse(Func<bool> action)
        {
            UntilFalse(action, DefaultTimeout);
        }

        /// <summary>
        /// Waits until the action returns false.
        /// </summary>
        /// <exception cref="TimeoutException">Thrown when the timeout (30 seconds) exceeds.</exception>
        public static void UntilFalse(Func<bool> action, TimeSpan timeout)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var retryUnil = DateTime.Now.Add(timeout);

            while (retryUnil >= DateTime.Now)
            {
                var result = action.Invoke();

                if (!result)
                {
                    return;
                }
            }

            throw new TimeoutException();
        }
    }
}