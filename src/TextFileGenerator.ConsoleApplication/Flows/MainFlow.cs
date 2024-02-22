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
using DustInTheWind.TextFileGenerator.ConsoleApplication.CommandArguments;
using DustInTheWind.TextFileGenerator.ConsoleApplication.Services;
using DustInTheWind.TextFileGenerator.ProjectAccess;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Flows
{
    internal class MainFlow
    {
        private readonly UserInterface userInterface;
        private readonly MainView mainView;
        private readonly Options arguments;

        public MainFlow(UserInterface userInterface, MainView mainView, Options arguments)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
            this.arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public void Start()
        {
            mainView.WriteHeader();

            try
            {
                ExecuteUseCase();
            }
            catch (Exception ex)
            {
                mainView.DisplayError(ex);
            }
        }

        private void ExecuteUseCase()
        {
            if (arguments.DescriptorFileNames != null && arguments.DescriptorFileNames.Count > 0)
            {
                ProjectRepository projectRepository = new ProjectRepository();
                GenerateUseCase generateUseCase = new GenerateUseCase(userInterface, projectRepository, arguments.DescriptorFileNames);
                generateUseCase.Execute();
            }
            else if (arguments.GenerateScaffold)
            {
                ScaffoldUseCase scaffoldUseCase = new ScaffoldUseCase(userInterface);
                scaffoldUseCase.Execute();
            }
        }
    }
}
