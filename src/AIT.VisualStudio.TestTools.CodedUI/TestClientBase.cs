using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

using AIT.VisualStudio.TestTools.CodedUI.Extensions;

using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace AIT.VisualStudio.TestTools.CodedUI
{
    /// <summary>
    /// Client base class for UI tests.
    /// </summary>
    public class TestClientBase : IDisposable
    {
        #region Fields

        private ApplicationUnderTest _applicationUnderTest;

        #endregion

        /// <summary>
        /// Gets or sets the application under test.
        /// </summary>
        protected ApplicationUnderTest ApplicationUnderTest
        {
            get {
                return _applicationUnderTest ??
                       (_applicationUnderTest = ApplicationUnderTest.FromProcess(ApplicationUnderTestProcess));
            }
        }

        /// <summary>
        /// Gets or sets the application under test process.
        /// </summary>
        protected Process ApplicationUnderTestProcess
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the executable.
        /// </summary>
        protected FileInfo Executable
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an already running application under test should get reused.
        /// </summary>
        public bool ReuseExistingApplicationUnderTest
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClientBase"/> class.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="reuseExistingApplicationUnderTest">if set to <c>true</c> an already running application under test should get reused.</param>
        public TestClientBase(string executable, bool reuseExistingApplicationUnderTest = false)
        {
            FileInfo fileInfo;
            if (executable == null || (fileInfo = new FileInfo(executable)) == null || !fileInfo.Exists)
            {
                throw new ArgumentException("Executable file is invalid or was not found!");
            }

            ReuseExistingApplicationUnderTest = reuseExistingApplicationUnderTest;
            Executable = fileInfo;

            SetPlaybackSettings();
            LaunchApplicationUnderTest();
        }

        private void LaunchApplicationUnderTest()
        {
            if (Executable != null && Executable.Exists)
            {
                var directoryInfo = Executable.Directory;
                var workingDirectory = directoryInfo != null ? directoryInfo.FullName : Environment.CurrentDirectory;

                if (ReuseExistingApplicationUnderTest)
                {
                    ApplicationUnderTestProcess = GetExistingApplicationUnderTest(Executable);

                    if (ApplicationUnderTestProcess != null)
                    {
                        OnExistingApplicationUnderTestReused();
                    }
                }

                if (ApplicationUnderTestProcess == null)
                {
                    CloseAllExistingApplicationUnderTest(Executable);

                    var processStartInfo = new ProcessStartInfo(Executable.FullName) { UseShellExecute = true, WorkingDirectory = workingDirectory };
                    ApplicationUnderTestProcess = Process.Start(processStartInfo);

                    OnApplicationUnderTestStarted();
                }
            }
            else
            {
                if (Executable != null)
                {
                    throw new FileNotFoundException(Executable.FullName);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }

            Playback.PlaybackError += OnPlaybackError;
        }

        /// <summary>
        /// Called when a playback error occurs.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PlaybackErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPlaybackError(object sender, PlaybackErrorEventArgs e)
        {
            if (e != null)
            {
                Trace.WriteLine(e.Error);
            }
        }

        /// <summary>
        /// Searches the executable.
        /// </summary>
        /// <returns></returns>
        protected virtual FileInfo SearchExecutable()
        {
            return null;
        }

        /// <summary>
        /// Called when the <see cref="ApplicationUnderTest"/> was started.
        /// </summary>
        protected virtual void OnApplicationUnderTestStarted()
        {
        }

        /// <summary>
        /// Called when an existing application under test is reused.
        /// </summary>
        protected virtual void OnExistingApplicationUnderTestReused()
        {
        }

        /// <summary>
        /// Searches the executable.
        /// </summary>
        public static string SearchExecutable(string fileName)
        {
            var fileInfo = new FileInfo(Path.GetFullPath(fileName));

            var localFileInfo = new FileInfo(Path.Combine(Environment.CurrentDirectory, fileInfo.Name));

            if (localFileInfo.Exists)
            {
                return localFileInfo.FullName;
            }
            else
            {
                var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                var searchedFileInfo = currentDirectory.SearchFile(fileInfo.Name, true).FirstOrDefault();

                if (searchedFileInfo != null && searchedFileInfo.Exists)
                {
                    return searchedFileInfo.FullName;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the playback settings.
        /// </summary>
        private void SetPlaybackSettings()
        {
            Playback.PlaybackSettings.ThinkTimeMultiplier = 1;
            Playback.PlaybackSettings.SearchTimeout = 5000;
            Playback.PlaybackSettings.WaitForReadyTimeout = 2000;
            Playback.PlaybackSettings.SkipSetPropertyVerification = true;
            Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.None;
            Playback.PlaybackSettings.MatchExactHierarchy = false;
        }

        #region Process Handling

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static Process GetExistingApplicationUnderTest(FileInfo executable)
        {
            foreach (var p in Process.GetProcesses())
            {
                try
                {
                    var fileName = Path.GetFullPath(p.MainModule.FileName);

                    if (string.Compare(executable.FullName, fileName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return p;
                    }
                }
                catch (Exception e)
                {
                    // Just catch all exceptions
                    Trace.TraceWarning("Could not compare process name with given executable: {0}", e);
                }
            }

            return null;
        }

        private static void CloseAllExistingApplicationUnderTest(FileInfo executable)
        {
            Process process = GetExistingApplicationUnderTest(executable);

            if (process != null)
            {
                process.CloseAllInstances();
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose data import job
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the dependent objects of data import job
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Playback.PlaybackError -= OnPlaybackError;

                if (!ReuseExistingApplicationUnderTest)
                {
                    if (_applicationUnderTest != null)
                    {
                        _applicationUnderTest.Close();
                        _applicationUnderTest.Dispose();
                    }
                    else if (ApplicationUnderTestProcess != null)
                    {
                        ApplicationUnderTestProcess.CloseAllInstances();
                    }
                }

                _applicationUnderTest = null;
                ApplicationUnderTestProcess = null;
            }
        }

        #endregion
    }
}