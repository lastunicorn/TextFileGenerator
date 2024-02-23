// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Diagnostics;
using DustInTheWind.TextFileGenerator.Domain.FileGeneration;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Ports.ProjectAccess;
using DustInTheWind.TextFileGenerator.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.TextFileGenerator.Cli.Application.Generate;

internal class GenerateUseCase : IRequestHandler<GenerateRequest>
{
    private readonly IUserInterface userInterface;
    private readonly IProjectRepository projectRepository;

    public GenerateUseCase(IUserInterface userInterface, IProjectRepository projectRepository)
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public Task Handle(GenerateRequest request, CancellationToken cancellationToken)
    {
        string projectFileName = request.DescriptorFileNames[0];
        Project project = ReadDescriptorFile(projectFileName);

        string outputFileName = GenerateOutputFileName(projectFileName);
        GenerateTextFile(project, outputFileName);

        return Task.CompletedTask;
    }

    private Project ReadDescriptorFile(string projectFileName)
    {
        userInterface.DisplayOptionFileReading(projectFileName);
        Project project = projectRepository.Get(projectFileName);
        userInterface.DisplayOk();

        return project;
    }

    private void GenerateTextFile(Project project, string outputFileName)
    {
        userInterface.DisplayOutputFileGenerating(outputFileName);

        TimeSpan time = Measure(() =>
        {
            userInterface.ExecuteWithSpinner(() =>
            {
                using Stream outputStream = File.Create(outputFileName);
                using Output output = new(outputStream);
                output.AddSections(project.Sections);
            });
        });

        userInterface.DisplayOk();
        userInterface.DisplayElapsedTime(time);
    }

    private static TimeSpan Measure(Action action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();

        return stopwatch.Elapsed;
    }

    private string GenerateOutputFileName(string projectFileName)
    {
        string directoryPath = Path.GetDirectoryName(projectFileName);
        string outputFileName = Path.GetFileNameWithoutExtension(projectFileName) + ".output.txt";

        return Path.Combine(directoryPath, outputFileName);
    }
}