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
using System.Diagnostics;
using System.IO;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    class GenerateTextFileFlow
    {
        private readonly GenerateTextFileView view;
        private readonly Arguments arguments;

        public GenerateTextFileFlow(GenerateTextFileView view, Arguments arguments)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (arguments == null) throw new ArgumentNullException("arguments");

            this.view = view;
            this.arguments = arguments;
        }

        public void Start()
        {
            FileDescriptor fileDescriptor = ReadDescriptorFile(arguments.OptionsFileName);
            GenerateTextFile(fileDescriptor);
        }

        private FileDescriptor ReadDescriptorFile(string fileName)
        {
            FileDescriptor options;

            view.DisplayOptionFileReading(fileName);

            using (Stream inputStream = File.OpenRead(fileName))
            {
                OptionsSerializer serializer = new OptionsSerializer();
                options = serializer.Deserialize(inputStream);
            }

            view.DisplayOptionFileReadingDone();

            return options;
        }

        private void GenerateTextFile(FileDescriptor fileDescriptor)
        {
            string outputFileName = GenerateOutputFileName(arguments.OptionsFileName);

            Stopwatch stopwatch = new Stopwatch();

            view.DisplayOutputFileGenerating(outputFileName);

            Generator generator = new Generator(fileDescriptor);

            using (Stream outputStream = File.Create(outputFileName))
            {
                stopwatch.Start();
                generator.Generate(outputStream);
                stopwatch.Stop();
            }

            view.DisplayOutputFileGenerateDone();

            view.DisplayElapsedTime(stopwatch.Elapsed);
        }

        private static string GenerateOutputFileName(string optionsFileName)
        {
            string directoryPath = Path.GetDirectoryName(optionsFileName);
            string outputFileName = Path.GetFileNameWithoutExtension(optionsFileName) + ".output.txt";

            return Path.Combine(directoryPath, outputFileName);
        }
    }
}
