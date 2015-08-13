using System.Collections.Generic;
using System.IO;

namespace AIT.VisualStudio.TestTools.CodedUI.Extensions
{
    /// <summary>
    /// Contains extension methods for the <see cref="DirectoryInfo"/> class.
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Searches the file in current, sub and parent directories.
        /// </summary>
        /// <param name="baseDirectory">The base directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="includingParentDirectories">if set to <c>true</c> also parent directories are included.</param>
        public static IEnumerable<FileInfo> SearchFile(this DirectoryInfo baseDirectory, string fileName, bool includingParentDirectories)
        {
            if (baseDirectory != null)
            {
                foreach (var file in baseDirectory.EnumerateFiles(fileName, SearchOption.AllDirectories))
                {
                    yield return file;
                }

                if (includingParentDirectories)
                {
                    foreach (var file in SearchFile(baseDirectory.Parent, fileName, true))
                    {
                        yield return file;
                    }
                }
            }
        }

        #endregion
    }
}