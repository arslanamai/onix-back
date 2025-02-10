using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Blocks;
using Onix.WebSites.Domain.Locations;
using Onix.WebSites.Domain.Media;
using Onix.WebSites.Domain.Products;

namespace Onix.WebSites.Domain.WebSites;

public class WebSite : SharedKernel.Entity<WebSiteId>
{
    //ef core
    private WebSite(WebSiteId id) : base(id)
    {
    }

    private WebSite(
        WebSiteId id,
        SubDomain subDomain,
        Name name,
        DateTime createdDate,
        bool isPublish = true) : base(id)
    {
        SubDomain = subDomain;
        Name = name;
        CreatedDate = createdDate;
        IsPublish = isPublish;
    }

    public SubDomain SubDomain { get; private set; }
    public Name Name { get; private set; }
    public Favicon Favicon { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public bool IsPublish { get; private set; }

    public IReadOnlyList<Block> Blocks => _blocks;
    private readonly List<Block> _blocks = [];
    public IReadOnlyList<Product> Products => _products;
    private readonly List<Product> _products = [];
    public IReadOnlyList<Location> Locations => _locations;
    private readonly List<Location> _locations = [];

    //website
    public static Result<WebSite, ErrorList> Create(
        WebSiteId id,
        SubDomain subDomain,
        Name name,
        DateTime createdDate,
        bool isPublish = true)
    {
        return new WebSite(
            id,
            subDomain,
            name,
            createdDate, 
            isPublish);
    }

    public UnitResult<Error> Update(
        SubDomain newSubDomain,
        Name newName)
    {
        this.SubDomain = newSubDomain;
        this.Name = newName;
        
        return UnitResult.Success<Error>();
    }
    
    //реализовать позиции
    //block
    public UnitResult<Error> AddBlock(Block block)
    {
        if (_blocks.Count >= Constants.MAX_BLOCK_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.MaxCount(ConstType.Block));

        block.UpdateIndex(_blocks.Count);
        _blocks.Add(block);
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> UpdateOrder( List<Block> blocks)
    {
        var blocksById = blocks.ToDictionary(b => b.Id, b => b.Index);

        _blocks
            .Where(existingBlock => blocksById.ContainsKey(existingBlock.Id))
            .ToList()
            .ForEach(existingBlock => existingBlock.UpdateIndex(blocksById[existingBlock.Id]));

        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveBlock(
        Block block)
    {
        if (_blocks.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.Empty(ConstType.Block));

        _blocks.Remove(block);
        return UnitResult.Success<Error>();
    }

    //favicon
    public UnitResult<Error> AddFavicon(
        Favicon favicon)
    {
        this.Favicon = favicon;
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveFavicon()
    {
        if (this.Favicon == null)
            return UnitResult.Success<Error>();

        this.Favicon = null;
        return UnitResult.Success<Error>();
    }

    //status
    public UnitResult<Error> UpdateStatus(bool isPublish)
    {
        IsPublish = isPublish;
        return UnitResult.Success<Error>();
    }
    
    //product
    public UnitResult<Error> AddProduct(Product product)
    {
        if (_products.Count >= Constants.MAX_PRODUCT_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.MaxCount(ConstType.Product));

        _products.Add(product);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveProduct(Product product)
    {
        if (_products.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.Empty(ConstType.Product));

        _products.Remove(product);
        return UnitResult.Success<Error>();
    }
    
    //location
    public UnitResult<Error> AddLocation(Location location)
    {
        if (_locations.Count >= Constants.MAX_LOCATION_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.MaxCount(ConstType.Location));

        _locations.Add(location);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveLocation(
        Location location)
    {
        if (_locations.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.Empty(ConstType.Location));

        _locations.Remove(location);
        return UnitResult.Success<Error>();
    }
}