using Onix.WebSites.Application.Commands.WebSites.AddSocial;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record AddSocialRequest(
    List<SocialMedia> SocialMedias)
{
    public AddSocialCommand ToCommand(Guid id)
        => new AddSocialCommand(id, SocialMedias);
}