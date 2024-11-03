using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;

namespace Onix.WebSites.Domain.WebSites.ValueObjects;

public class SocialMedia
{
    private SocialMedia(
        string social,
        string link)
    {
        Social = social;
        Link = link;
    }
    
    public string Social { get; }
    public string Link { get;}

    public static Result<List<SocialMedia>> Create(
        List<SocialMedia> socialMedias)
    {
        return new List<SocialMedia>(socialMedias);
    }
}