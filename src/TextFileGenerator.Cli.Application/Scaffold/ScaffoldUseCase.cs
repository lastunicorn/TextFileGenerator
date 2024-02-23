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

using DustInTheWind.TextFileGenerator.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.TextFileGenerator.Cli.Application.Scaffold;

internal class ScaffoldUseCase : IRequestHandler<ScaffoldRequest>
{
    private const string ScaffoldResourceFilePath = "DustInTheWind.TextFileGenerator.Application.Scaffold.Scaffold.xml";
    private const string ScaffoldDefaultFileName = "file.xml";

    private readonly IUserInterface userInterface;

    public ScaffoldUseCase(IUserInterface userInterface)
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
    }

    public Task Handle(ScaffoldRequest request, CancellationToken cancellationToken)
    {
        using Stream outputStream = File.Create(ScaffoldDefaultFileName);
        using Stream inputStream = EmbeddedResources.GetEmbeddedStream(ScaffoldResourceFilePath);

        inputStream.CopyTo(outputStream);

        userInterface.DisplayOutputFileGenerateDone(ScaffoldDefaultFileName);

        return Task.CompletedTask;
    }
}