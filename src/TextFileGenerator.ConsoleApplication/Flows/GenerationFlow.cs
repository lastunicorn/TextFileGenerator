﻿// TextFileGenerator
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
using DustInTheWind.TextFileGenerator.FileGeneration;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    class GenerationFlow : IFlow
    {
        private readonly UserInterface ui;
        private readonly string descriptorFileName;

        public GenerationFlow(UserInterface ui, string descriptorFileName)
        {
            if (ui == null) throw new ArgumentNullException("ui");
            if (descriptorFileName == null) throw new ArgumentNullException("descriptorFileName");

            this.ui = ui;
            this.descriptorFileName = descriptorFileName;
        }

        public void Start()
        {
            FileDescriptor fileDescriptor = ReadDescriptorFile();
            GenerateTextFile(fileDescriptor);
        }

        private FileDescriptor ReadDescriptorFile()
        {
            FileDescriptor fileDescriptor;

            DisplayOptionFileReading(descriptorFileName);

            using (Stream inputStream = File.OpenRead(descriptorFileName))
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

            Stopwatch stopwatch = new Stopwatch();

            DisplayOutputFileGenerating(outputFileName);

            using (ConsoleSpinner consoleSpinner = new ConsoleSpinner())
            {
                consoleSpinner.Start();

                try
                {
                    Generator generator = new Generator(fileDescriptor);

                    using (Stream outputStream = File.Create(outputFileName))
                    {
                        stopwatch.Start();
                        generator.Generate(outputStream);
                        stopwatch.Stop();
                    }
                }
                finally
                {
                    consoleSpinner.Stop();
                }
            }

            DisplayOk();
            DisplayElapsedTime(stopwatch.Elapsed);
        }

        private string GenerateOutputFileName()
        {
            string directoryPath = Path.GetDirectoryName(descriptorFileName);
            string outputFileName = Path.GetFileNameWithoutExtension(descriptorFileName) + ".output.txt";

            return Path.Combine(directoryPath, outputFileName);
        }

        public void DisplayOptionFileReading(string descriptorFileName)
        {
            ui.Write("Reading description file ");
            ui.WriteEnhanced(descriptorFileName);
            ui.Write(" ");
        }

        public void DisplayOk()
        {
            ui.WriteLine("[Done]");
        }

        public void DisplayOutputFileGenerating(string outputFileName)
        {
            ui.Write("Generating file ");
            ui.WriteEnhanced(outputFileName);
            ui.Write(" ");
        }

        public void DisplayElapsedTime(TimeSpan elapsed)
        {
            ui.WriteLine();
            ui.Write("Elapsed time: ");
            ui.WriteLineEnhanced(elapsed.ToString());
        }
    }
}