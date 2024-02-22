using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace DustInTheWind.TextFileGenerator.Ports.ProjectAccess
{
    public interface IProjectRepository
    {
        Project Get(string fileName);
    }
}