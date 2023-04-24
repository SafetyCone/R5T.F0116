using System;
using System.Collections.Generic;
using System.Linq;

using R5T.F0000;
using R5T.T0132;
using R5T.T0180;
using R5T.T0180.Extensions;


namespace R5T.F0116
{
    [FunctionalityMarker]
    public partial interface IFileSystemOperator : IFunctionalityMarker,
        F0000.IFileSystemOperator
    {
        public IEnumerable<(DirectoryPath, T)> EnumerateChildDirectories_OfType<T>(
            IDirectoryPath directoryPath,
            Func<IDirectoryName, WasFound<T>> directoryNameDiscriminator)
        {
            var childDirectoryPaths = this.EnumerateAllChildDirectoryPaths(directoryPath.Value);

            var priorDirectoryDates = childDirectoryPaths
               .Select(directoryPath => new { DirectoryPath = directoryPath.ToDirectoryPath(), DirectoryName = Instances.PathOperator.GetDirectoryName(directoryPath).ToDirectoryName() })
               .Select(pair => new { pair.DirectoryPath, WasFound = directoryNameDiscriminator(pair.DirectoryName) })
               .Where(pair => pair.WasFound)
               .Select(pair => (pair.DirectoryPath, pair.WasFound.Result))
               ;

            return priorDirectoryDates;
        }

        public IEnumerable<(DirectoryPath DirectoryPath, DateTime DirectoryDateTime)> EnumerateChildDirectories_YYYYMMDD(
            IDirectoryPath directoryPath)
        {
            var priorDirectoryDates = this.EnumerateChildDirectories_OfType(
                directoryPath,
                directoryName => Instances.DateTimeOperator.Is_YYYYMMDD(directoryName.Value));

            return priorDirectoryDates;
        }

        public IEnumerable<(DirectoryPath DirectoryPath, DateTime DirectoryDateTime)> EnumerateChildDirectories_YYYYMMDD_HHMMSS(
            IDirectoryPath directoryPath)
        {
            var priorDirectoryDates = this.EnumerateChildDirectories_OfType(
                directoryPath,
                directoryName => Instances.DateTimeOperator.Is_YYYYMMDD_HHMMSS(directoryName.Value));

            return priorDirectoryDates;
        }
    }
}
