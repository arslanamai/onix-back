using Onix.Core.Dtos;
using Onix.WebSites.Application.Commands.WebSites.AddSocial;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record AddSocialRequest(
    List<SocialMedia> SocialMedias)
{
    public AddSocialCommand ToCommand(Guid id)
        => new AddSocialCommand(id, SocialMedias);
}