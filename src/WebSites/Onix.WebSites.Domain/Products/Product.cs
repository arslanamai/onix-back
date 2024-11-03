using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Photos;
using Onix.WebSites.Domain.Products.ValueObjects;

namespace Onix.WebSites.Domain.Products;

public class Product : SharedKernel.Entity<ProductId>
{
    //ef core
    private Product(ProductId id) : base(id)
    {
    }
    
    private Product(
        ProductId id,
        Name name,
        Description description,
        Price price,
        Link link) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Link = link;
    }
    
    public Name Name {get; private set; }
    public Description Description { get; private set; }
    public Price Price { get; private set; }
    public Link Link { get; private set; }
    
    public IReadOnlyList<Photo> ProductPhotos => _productPhotos ;
    private readonly List<Photo> _productPhotos = [];

    public static Result<Product> Create(
        ProductId id,
        Name name,
        Description description,
        Price price,
        Link link)
    {
        return new Product(
            id,
            name,
            description,
            price,
            link); 
    }

    public UnitResult<Error> Update(
        Name name,
        Description description,
        Price price,
        Link link)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Link = link;

        return UnitResult.Success<Error>();
    }
    
    //photo
    public UnitResult<Error> AddPhoto(
        Photo photo)
    {
        if (_productPhotos.Count >= Constants.MAX_PHOTO_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.Photo));

        _productPhotos.Add(photo);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemovePhoto(
        Photo photo)
    {
        if (_productPhotos.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.Photo));

        _productPhotos.Remove(photo);
        return UnitResult.Success<Error>();
    }
}