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

using DustInTheWind.TextFileGenerator.Application.Generate;
using DustInTheWind.TextFileGenerator.Application.Scaffold;
using DustInTheWind.TextFileGenerator.Cli.CommandArguments;
using DustInTheWind.TextFileGenerator.ProjectAccess;
using DustInTheWind.TextFileGenerator.UserAccess;
using MediatR;

namespace DustInTheWind.TextFileGenerator.Cli.Flows
{
    /// <summary>
    /// This is a hub that chooses a flow and starts it.
    /// </summary>
    internal class MainFlow
    {
        private readonly MainView mainView;
        private readonly Options options;
        private readonly IMediator mediator;

        public MainFlow(MainView mainView, Options options, IMediator mediator)
        {
            this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Start()
        {
            mainView.WriteHeader();

            try
            {
                if (options.DescriptorFileNames is { Count: > 0 })
                {
                    await GenerateOutput();
                }
                else if (options.GenerateScaffold)
                {
                    await GenerateScaffoldFile();
                }
            }
            catch (Exception ex)
            {
                mainView.DisplayError(ex);
            }
        }

        private async Task GenerateOutput()
        {
            GenerateRequest request = new()
            {
                DescriptorFileNames = options.DescriptorFileNames
            };
            await mediator.Send(request);
        }

        private async Task GenerateScaffoldFile()
        {
            ScaffoldRequest request = new();
            await mediator.Send(request);
        }
    }
}
