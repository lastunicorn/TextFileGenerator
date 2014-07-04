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
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication
{
    class MainPresenter
    {
        private readonly MainView view;

        public MainPresenter(MainView view)
        {
            if (view == null) throw new ArgumentNullException("view");
            this.view = view;
        }

        public void Start(String[] args)
        {
            view.WriteHeader();

            try
            {
                ValidateArguments(args);

                string optionsFileName = args[0];
                GeneratorOptions options = ReadOptionsFile(optionsFileName);

                string outputFileName = GenerateOutputFileName(optionsFileName);
                GenerateFile(outputFileName, options);
            }
            catch (Exception ex)
            {
                view.DisplayError(ex);
            }

            view.Pause();
        }

        private void ValidateArguments(string[] args)
        {
            if (args.Length == 0)
            {
                view.WriteUsageHelp();
                throw new Exception("Insufficient arguments.");
            }
        }

        private static string GenerateOutputFileName(string optionsFileName)
        {
            string directoryPath = Path.GetDirectoryName(optionsFileName);
            string outputFileName = Path.GetFileNameWithoutExtension(optionsFileName) + ".output.txt";

            return Path.Combine(directoryPath, outputFileName);
        }

        private GeneratorOptions ReadOptionsFile(string optionsFileName)
        {
            GeneratorOptions options;

            view.DisplayOptionFileReading(optionsFileName);

            using (Stream inputStream = File.OpenRead(optionsFileName))
            {
                OptionsSerializer serializer = new OptionsSerializer();
                options = serializer.Deserialize(inputStream);
            }

            view.DisplayOptionFileReadingDone();

            return options;
        }

        private void GenerateFile(string outputFileName, GeneratorOptions options)
        {
            Stopwatch stopwatch = new Stopwatch();

            view.DisplayOutputFileGenerating(outputFileName);

            Generator generator = new Generator(options);

            using (Stream outputStream = File.Create(outputFileName))
            {
                stopwatch.Start();
                generator.Generate(outputStream);
                stopwatch.Stop();
            }

            view.DisplayOutputFileGenerateDone();

            view.DisplayElapsedTime(stopwatch.Elapsed);
        }
    }
}
