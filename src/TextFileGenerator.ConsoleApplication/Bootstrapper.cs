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

using CommandLine;
using DustInTheWind.TextFileGenerator.ConsoleApplication.CommandArguments;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Flows;
using DustInTheWind.TextFileGenerator.UserAccess;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication
{
    internal class Bootstrapper
    {
        private Options options;
        private MainFlow mainFlow;

        public void Run(string[] args)
        {
            options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
                return;

            CreateUi();
            StartUi();
        }

        private void CreateUi()
        {
            UserInterface ui = new UserInterface();
            MainView mainView = new MainView();

            mainFlow = new MainFlow(ui, mainView, options);
        }

        private void StartUi()
        {
            mainFlow.Start();
        }
    }
}
