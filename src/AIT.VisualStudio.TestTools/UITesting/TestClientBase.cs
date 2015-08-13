namespace AIT.VisualStudio.TestTools.UITesting
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    using AIT.VisualStudio.TestTools.Extensions;

    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;

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
            get
            {
                if (this._applicationUnderTest == null)
                {
                    this._applicationUnderTest = ApplicationUnderTest.FromProcess(ApplicationUnderTestProcess);
                }

                return this._applicationUnderTest;
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
        protected virtual FileInfo Executable
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
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClientBase"/> class.
        /// </summary>
        /// <param name="reuseExistingApplicationUnderTest">if set to <c>true</c> an already running application under test should get reused.</param>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestClientBase(bool reuseExistingApplicationUnderTest = false)
        {
            ReuseExistingApplicationUnderTest = reuseExistingApplicationUnderTest;

            InitializeExecutable();
            SetPlaybackSettings();
            LaunchApplicationUnderTest();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClientBase"/> class.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="reuseExistingApplicationUnderTest">if set to <c>true</c> an already running application under test should get reused.</param>
        /// <param name="searchExecutableIfNotFound">if set to <c>true</c> and the executable is not found it gets searched in other directories.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestClientBase(string executable, bool reuseExistingApplicationUnderTest = false, bool searchExecutableIfNotFound = true)
        {
            ReuseExistingApplicationUnderTest = reuseExistingApplicationUnderTest;

            InitializeExecutable(executable, searchExecutableIfNotFound);
            SetPlaybackSettings();
            LaunchApplicationUnderTest();
        }

        private void LaunchApplicationUnderTest()
        {
            if (Executable != null && Executable.Exists)
            {
                var directoryInfo = this.Executable.Directory;
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

        private void InitializeExecutable()
        {
            if (Executable == null)
            {
                Executable = SearchExecutable();
            }
        }

        private void InitializeExecutable(string executable, bool searchExcecutableIfNotFound = true)
        {
            var fileInfo = new FileInfo(Path.GetFullPath(executable));

            if (fileInfo.Exists)
            {
                Executable = fileInfo;
            }
            else if (searchExcecutableIfNotFound)
            {
                Executable = this.SearchExecutable(fileInfo.Name);
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
        protected virtual FileInfo SearchExecutable(string fileName)
        {
            var fileInfo = new FileInfo(Path.GetFullPath(fileName));

            var localFileInfo = new FileInfo(Path.Combine(Environment.CurrentDirectory, fileInfo.Name));

            if (localFileInfo.Exists)
            {
                return localFileInfo;
            }
            else
            {
                var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                var searchedFileInfo = currentDirectory.SearchFile(fileInfo.Name, true).FirstOrDefault();

                if (searchedFileInfo != null && searchedFileInfo.Exists)
                {
                    return searchedFileInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the playback settings.
        /// </summary>
        protected virtual void SetPlaybackSettings()
        {
            Playback.PlaybackSettings.ThinkTimeMultiplier = 1;
            Playback.PlaybackSettings.SearchTimeout = 5000;
            Playback.PlaybackSettings.SearchInMinimizedWindows = true;
            Playback.PlaybackSettings.WaitForReadyTimeout = 2000;
            Playback.PlaybackSettings.DelayBetweenActions = 50;
            Playback.PlaybackSettings.SkipSetPropertyVerification = true;
            Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.None;
            Playback.PlaybackSettings.MatchExactHierarchy = false;
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
        }

        #region Process Handling

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static Process GetExistingApplicationUnderTest(FileInfo executable)
        {
            foreach (var p in Process.GetProcesses())
            {
                try
                {
                    var fileName = Path.GetFullPath(p.MainModule.FileName);

                    if (System.String.Compare(executable.FullName, fileName, System.StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return p;
                    }
                }
                catch
                {
                    // Just catch all exceptions
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