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
using System.Reflection;
using DustInTheWind.TextFileGenerator.ConsoleApplication.CommandArguments;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    /// <summary>
    /// This is a hub that chooses a flow and starts it.
    /// </summary>
    internal class MainFlow
    {
        private readonly UserInterface ui;
        private readonly Options arguments;

        public MainFlow(UserInterface ui, Options arguments)
        {
            if (ui == null) throw new ArgumentNullException(nameof(ui));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));

            this.ui = ui;
            this.arguments = arguments;
        }

        public void Start()
        {
            WriteHeader();

            try
            {
                IFlow flow = ChooseFlow();

                flow?.Start();
            }
            catch (Exception ex)
            {
                ui.DisplayError(ex);
            }
        }

        private IFlow ChooseFlow()
        {
            if (arguments.DescriptorFileNames != null && arguments.DescriptorFileNames.Count > 0)
                return new GenerationFlow(ui, arguments.DescriptorFileNames);

            if (arguments.GenerateScaffold)
                return new ScaffoldFlow(ui);

            return null;
        }

        public void WriteHeader()
        {
            Version version = GetVersion();

            ui.WriteLine("TextFileGenerator " + version.ToString(3));
            ui.WriteLine("===============================================================================");
            ui.WriteLine();
        }

        private static Version GetVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version;
        }
    }
}
