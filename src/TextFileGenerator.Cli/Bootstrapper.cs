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
using DustInTheWind.TextFileGenerator.Cli.CommandArguments;
using DustInTheWind.TextFileGenerator.Cli.Flows;
using DustInTheWind.TextFileGenerator.UserAccess;

namespace DustInTheWind.TextFileGenerator.Cli
{
    internal class Bootstrapper
    {
        private MainFlow mainFlow;

        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    CreateUi(o);
                    StartUi();
                });
        }

        private void CreateUi(Options options)
        {
            UserInterface ui = new();
            MainView mainView = new();

            mainFlow = new MainFlow(ui, mainView, options);
        }

        private void StartUi()
        {
            mainFlow.Start();
        }
    }
}
