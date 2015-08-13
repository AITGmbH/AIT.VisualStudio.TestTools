namespace AIT.VisualStudio.TestTools.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Extension methods for the <see cref="Process"/> class.
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Closes the specified process.
        /// </summary>
        public static void Close(this Process process, bool force)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            var timeout = TimeSpan.FromSeconds(30);
            var retryUntil = DateTime.Now.Add(timeout);

            process.Refresh();

            try
            {
                while (!process.HasExited && retryUntil < DateTime.Now)
                {
                    process.CloseMainWindow();
                    Thread.Sleep(100);
                    process.Refresh();
                }

                process.Refresh();

                if (!process.HasExited && force)
                {
                    process.Kill();
                    Thread.Sleep(100);
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// Closes all process instances having the same name except this.
        /// </summary>
        public static void CloseAllInstancesButThis(this Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            var processes = Process.GetProcessesByName(process.ProcessName);

            foreach (var item in processes.Where(item => process.Id != item.Id))
            {
                item.Close(true);
            }
        }

        /// <summary>
        /// Closes all process instances having the same name.
        /// </summary>
        public static void CloseAllInstances(this Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            var testApplications = Process.GetProcessesByName(process.ProcessName);

            foreach (var testApplication in testApplications)
            {
                testApplication.Close(true);
            }
        }
    }
}