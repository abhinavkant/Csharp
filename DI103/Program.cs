using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI103
{
    public interface IFileSystemAdapter { }

    public class FileSystemAdapter : IFileSystemAdapter { }

    public interface IBuildDirectoryStructureService
    {
        IFileSystemAdapter GetFileSystemAdapter();
    }

    public class BuildDirectoryStructureService : IBuildDirectoryStructureService
    {
        private readonly IFileSystemAdapter _fileSystemAdapter;

        public BuildDirectoryStructureService(IFileSystemAdapter fileSystemAdapter)
        {
            _fileSystemAdapter = fileSystemAdapter;
        }

        public IFileSystemAdapter GetFileSystemAdapter()
        {
            return _fileSystemAdapter;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new IoC();

            ioc.Register<IFileSystemAdapter, FileSystemAdapter>();
            ioc.Register<IBuildDirectoryStructureService, BuildDirectoryStructureService>();

            IBuildDirectoryStructureService service = ioc.Resolve<IBuildDirectoryStructureService>();
            Console.WriteLine(service.GetFileSystemAdapter());
            Console.ReadKey();
        }
    }
}
