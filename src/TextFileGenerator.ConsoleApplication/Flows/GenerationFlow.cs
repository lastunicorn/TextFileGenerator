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
using DustInTheWind.TextFileGenerator.ConsoleApplication.Properties;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileGeneration;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    internal class GenerationFlow : IFlow
    {
        private readonly UserInterface ui;
        private readonly IList<string> descriptorFileNames;

        public GenerationFlow(UserInterface ui, IList<string> descriptorFileNames)
        {
            this.ui = ui ?? throw new ArgumentNullException(nameof(ui));
            this.descriptorFileNames = descriptorFileNames ?? throw new ArgumentNullException(nameof(descriptorFileNames));
        }

        public void Start()
        {
            FileDescriptor fileDescriptor = ReadDescriptorFile();
            GenerateTextFile(fileDescriptor);
        }

        private FileDescriptor ReadDescriptorFile()
        {
            FileDescriptor fileDescriptor;

            DisplayOptionFileReading(descriptorFileNames[0]);

            using (Stream inputStream = File.OpenRead(descriptorFileNames[0]))
            {
                FileDescriptorSerializer serializer = new FileDescriptorSerializer();
                fileDescriptor = serializer.Deserialize(inputStream);
            }

            DisplayOk();

            return fileDescriptor;
        }

        private void GenerateTextFile(FileDescriptor fileDescriptor)
        {
            string outputFileName = GenerateOutputFileName();

            DisplayOutputFileGenerating(outputFileName);

            TimeSpan time = Measure(() =>
            {
                ui.ExecuteWithSpinner(() =>
                {
                    Generator generator = new Generator(fileDescriptor);

                    using (Stream outputStream = File.Create(outputFileName))
                    {
                        generator.Generate(outputStream);
                    }
                });

            });

            DisplayOk();
            DisplayElapsedTime(time);
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

        public void DisplayOptionFileReading(string fileName)
        {
            ui.Write(Resources.ReadingDescriptionFile);
            ui.WriteEnhanced(fileName);
            ui.Write(" ");
        }

        public void DisplayOk()
        {
            ui.WriteLine(Resources.Done);
        }

        public void DisplayOutputFileGenerating(string fileName)
        {
            ui.Write(Resources.GeneratingFile);
            ui.WriteEnhanced(fileName);
            ui.Write(" ");
        }

        public void DisplayElapsedTime(TimeSpan elapsed)
        {
            ui.WriteLine();
            ui.Write(Resources.ElapsedTime);
            ui.WriteLineEnhanced(elapsed.ToString());
        }
    }
}
