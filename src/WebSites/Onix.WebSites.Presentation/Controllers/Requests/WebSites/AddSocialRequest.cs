using Onix.Core.Dtos;
using Onix.WebSites.Application.Commands.WebSites.AddSocial;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record AddSocialRequest(
    List<SocialMediaDto> SocialMedias)
{
    public List<SocialMedia> Convert(List<SocialMediaDto> socialMedia)
    {
        var social = socialMedia.Select(
            s => SocialMedia.Create(s.Social, s.Link).Value).ToList();

        return social;
    }
    
    public AddSocialCommand ToCommand(Guid id)
        => new AddSocialCommand(id, Convert(SocialMedias));
}