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

using System.Collections.Generic;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Flows;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication
{
    class Bootstrapper
    {
        private Arguments arguments;
        private MainFlow mainFlow;

        public void Run(IReadOnlyList<string> args)
        {
            CreateArgumentsService(args);
            CreateUi();
            StartUi();
        }

        private void CreateArgumentsService(IReadOnlyList<string> args)
        {
            arguments = new Arguments(args);
        }

        private void CreateUi()
        {
            UserInterface ui = new UserInterface();
            mainFlow = new MainFlow(ui, arguments);
        }

        private void StartUi()
        {
            mainFlow.Start();
        }
    }
}
