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
using System.IO;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Properties;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    internal class ScaffoldFlow : IFlow
    {
        private const string ScaffoldResourceFilePath = "DustInTheWind.TextFileGenerator.ConsoleApplication.Scaffold.xml";
        private const string ScaffoldDefaultFileName = "file.xml";

        private readonly UserInterface ui;

        public ScaffoldFlow(UserInterface ui)
        {
            this.ui = ui ?? throw new ArgumentNullException(nameof(ui));
        }

        public void Start()
        {
            using (Stream outputStream = File.Create(ScaffoldDefaultFileName))
            {
                using (Stream inputStream = EmbededResources.GetEmbededStream(ScaffoldResourceFilePath))
                {
                    inputStream.CopyTo(outputStream);
                }
            }

            DisplayOutputFileGenerateDone(ScaffoldDefaultFileName);
        }

        public void DisplayOutputFileGenerateDone(string outputFileName)
        {
            ui.Write(Resources.ScaffoldFile_Success);
            ui.WriteEnhanced(outputFileName);
            ui.WriteLine();
        }
    }
}