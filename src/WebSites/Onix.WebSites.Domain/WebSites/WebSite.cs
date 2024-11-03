 using System.Runtime.Intrinsics.X86;
 using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Appearances;
using Onix.WebSites.Domain.Blocks;
using Onix.WebSites.Domain.Categories;
using Onix.WebSites.Domain.Locations;
using Onix.WebSites.Domain.Photos;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Domain.WebSites;

public class WebSite : SharedKernel.Entity<WebSiteId>
{
    //ef core
    private WebSite(WebSiteId id) : base(id)
    {
    }

    private WebSite(
        WebSiteId id,
        Url url,
        Name name,
        Appearance appearance,
        bool showStatus = true) : base(id)
    {
        Url = url;
        Name = name;
        ShowStatus = showStatus;
        Appearance = appearance;
    }

    public Url Url { get; private set; }
    public Name Name { get; private set; }
    public bool ShowStatus { get; private set; }

    public Appearance Appearance { get; private set; }
    
    public Phone? Phone { get; private set; }
    public Email? Email { get; private set; }
    
    public IReadOnlyList<Photo> Favicon => _favicon;
    private readonly List<Photo> _favicon = [];

    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
    private readonly List<SocialMedia> _socialMedias = [];

    public IReadOnlyList<Faq> Faqs => _faqs;
    private readonly List<Faq> _faqs = [];

    public IReadOnlyList<Block> Blocks => _blocks;
    private readonly List<Block> _blocks = [];

    public IReadOnlyList<Category> Categories => _categories;
    private readonly List<Category> _categories = [];
    
    public IReadOnlyList<Location> Locations => _locations;
    private readonly List<Location> _locations = [];

    //website
    public static Result<WebSite, ErrorList> Create(
        WebSiteId id,
        Url url,
        Name name,
        Appearance appearance,
        bool showStatus = true)
    {
        return new WebSite(
            id,
            url,
            name,
            appearance,
            showStatus);
    }

    public UnitResult<Error> Update(
        Url newUrl,
        Name newName)
    {
        this.Url = newUrl;
        this.Name = newName;
        
        return UnitResult.Success<Error>();
    }

    //contact
    public UnitResult<Error> AddSocial(
        List<SocialMedia> socialMedias)
    {
        if (_socialMedias.Count >= Constants.MAX_SOCIAL_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.SocialMedia));

        _socialMedias.Clear();
        _socialMedias.AddRange(socialMedias);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> UpdateContact(
        Phone phone, Email email)
    {
        this.Phone = phone;
        this.Email = email;
        return UnitResult.Success<Error>();
    }
    
    //faq
    public UnitResult<Error> UpdateFAQs(List<Faq> faqs)
    {
        if (faqs.Count > Constants.MAX_FAQ_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.FAQs));

        _faqs.Clear();
        _faqs.AddRange(faqs);

        return UnitResult.Success<Error>();
    }

    //category
    public UnitResult<Error> AddCategory(
        Category category)
    {
        if (_categories.Count >= Constants.MAX_CATEGORY_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.Category));
        
        _categories.Add(category);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveCategory(
        Category category)
    {
        if (_categories.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.Category));

        _categories.Remove(category);
        return UnitResult.Success<Error>();
    }
    
    //реализовать позиции
    //block
    public UnitResult<Error> AddBlock(Block block)
    {
        if (_blocks.Count >= Constants.MAX_BLOCK_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.Block));

        _blocks.Add(block);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveBlock(
        Block block)
    {
        if (_blocks.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.Block));

        _blocks.Remove(block);
        return UnitResult.Success<Error>();
    }

    //favicon
    public UnitResult<Error> AddFavicon(
        Photo favicon)
    {
        if (_favicon.Count is not Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.AlreadyExist(ConstType.Favicon));

        _favicon.Add(favicon);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveFavicon(
        Photo favicon)
    {
        if (_favicon.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.Favicon));

        _favicon.Remove(favicon);
        return UnitResult.Success<Error>();
    }

    //status
    public UnitResult<Error> UpdateStatus(bool status)
    {
        ShowStatus = status;
        return UnitResult.Success<Error>();
    }

    //location
    public UnitResult<Error> AddLocation(Location location)
    {
        if (_locations.Count >= Constants.MAX_LOCATION_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.Location));

        _locations.Add(location);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveLocation(
        Location location)
    {
        if (_locations.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.Location));

        _locations.Remove(location);
        return UnitResult.Success<Error>();
    }
}