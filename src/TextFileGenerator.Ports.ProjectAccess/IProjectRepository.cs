using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace DustInTheWind.TextFileGenerator.Ports.ProjectAccess
{
    public interface IProjectRepository
    {
        FileDescriptor Get(string fileName);
    }
}