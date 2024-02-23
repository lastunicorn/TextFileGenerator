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

using Autofac;
using DustInTheWind.TextFileGenerator.Cli.Flows;

namespace DustInTheWind.TextFileGenerator.Cli;

internal class Bootstrapper
{
    public async Task Run(string[] args)
    {
        IContainer container = CreateContainer(args);

        await RunApplication(container);
    }

    private static IContainer CreateContainer(string[] args)
    {
        ContainerBuilder containerBuilder = new();
        Setup.ConfigureDependencies(containerBuilder, args);

        IContainer container = containerBuilder.Build();
        return container;
    }

    private static async Task RunApplication(IContainer container)
    {
        MainFlow mainFlow = container.Resolve<MainFlow>();
        await mainFlow.Start();
    }
}