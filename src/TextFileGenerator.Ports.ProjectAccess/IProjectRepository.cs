using DustInTheWind.TextFileGenerator.FileDescription;

namespace DustInTheWind.TextFileGenerator.Ports.ProjectAccess
{
    public interface IProjectRepository
    {
        FileDescriptor Get(string fileName);
    }
}