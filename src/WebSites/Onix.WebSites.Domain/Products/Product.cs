using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Media;

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
        Code code) : base(id)
    {
        Name = name;
        Code = code;
    }
    
    public Name Name {get; private set; }
    public Code Code { get; private set; }
    
    public IReadOnlyList<Photo> Photos => _photos ;
    private readonly List<Photo> _photos = [];

    public static Result<Product> Create(
        ProductId id,
        Name name,
        Code code)
    {
        return new Product(
            id,
            name,
            code); 
    }

    public UnitResult<Error> Update(
        Name name,
        Code code)
    {
        this.Name = name;
        this.Code = code;
        return UnitResult.Success<Error>();
    }
    
    //photo
    public UnitResult<Error> AddPhoto(
        Photo photo)
    {
        if (_photos.Count > Constants.MAX_PHOTO_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.MaxCount(ConstType.Photo));

        _photos.Add(photo);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> SetMainPhoto (
        Photo photo)
    {
        _photos.Remove(photo);
        _photos.Insert(0, photo);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemovePhoto(
        Photo photo)
    {
        if (_photos.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domains.Empty(ConstType.Photo));

        _photos.Remove(photo);
        return UnitResult.Success<Error>();
    }
}