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
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    internal class DisplayUsageFlow : IFlow
    {
        private readonly UserInterface ui;

        public DisplayUsageFlow(UserInterface ui)
        {
            if (ui == null) throw new ArgumentNullException("ui");

            this.ui = ui;
        }

        public void Start()
        {
            WriteUsageHelp();
        }

        public void WriteUsageHelp()
        {
            using (Stream stream = EmbededResources.GetUsageStream())
            {
                if (stream == null)
                    throw new Exception("The 'usage' embeded file could not be found.");

                StreamReader reader = new StreamReader(stream);
                ui.WriteLine(reader.ReadToEnd());
            }
        }
    }
}