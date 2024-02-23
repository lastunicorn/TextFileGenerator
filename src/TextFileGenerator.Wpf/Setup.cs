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

using Autofac;
using DustInTheWind.TextFileGenerator.Wpf.Application;
using DustInTheWind.TextFileGenerator.Wpf.Application.PresentProjects;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace DustInTheWind.TextFileGenerator.Wpf;

public static class Setup
{
    public static void ConfigureDependencies(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<MainWindow>().AsSelf();
        containerBuilder.RegisterType<MainViewModel>().AsSelf();

        containerBuilder.RegisterType<ApplicationState>().AsSelf();

        MediatRConfiguration mediatRConfiguration = MediatRConfigurationBuilder.Create(typeof(PresentProjectsRequest).Assembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();

        containerBuilder.RegisterMediatR(mediatRConfiguration);
    }
}