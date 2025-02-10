using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.WebSites.Create;

public record CreateWebSiteCommand(string SubDomain, string Name) : ICommand;