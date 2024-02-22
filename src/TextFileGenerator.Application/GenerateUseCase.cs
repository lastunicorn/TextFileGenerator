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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileGeneration;
using DustInTheWind.TextFileGenerator.Ports.ProjectAccess;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    public class GenerateUseCase
    {
        private readonly IUserInterface userInterface;
        private readonly IProjectRepository projectRepository;
        private readonly IList<string> descriptorFileNames;

        public GenerateUseCase(IUserInterface userInterface, IProjectRepository projectRepository, IList<string> descriptorFileNames)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.descriptorFileNames = descriptorFileNames ?? throw new ArgumentNullException(nameof(descriptorFileNames));
        }

        public void Execute()
        {
            FileDescriptor fileDescriptor = ReadDescriptorFile();
            GenerateTextFile(fileDescriptor);
        }

        private FileDescriptor ReadDescriptorFile()
        {
            userInterface.DisplayOptionFileReading(descriptorFileNames[0]);
            FileDescriptor fileDescriptor = projectRepository.Get(descriptorFileNames[0]);
            userInterface.DisplayOk();

            return fileDescriptor;
        }

        private void GenerateTextFile(FileDescriptor fileDescriptor)
        {
            string outputFileName = GenerateOutputFileName();

            userInterface.DisplayOutputFileGenerating(outputFileName);

            TimeSpan time = Measure(() =>
            {
                userInterface.ExecuteWithSpinner(() =>
                {
                    Generator generator = new Generator(fileDescriptor);

                    using (Stream outputStream = File.Create(outputFileName))
                    {
                        generator.Generate(outputStream);
                    }
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

        private string GenerateOutputFileName()
        {
            string directoryPath = Path.GetDirectoryName(descriptorFileNames[0]);
            string outputFileName = Path.GetFileNameWithoutExtension(descriptorFileNames[0]) + ".output.txt";

            return Path.Combine(directoryPath, outputFileName);
        }
    }
}
