using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Products;

namespace Onix.WebSites.Domain.Categories;

public class Category : SharedKernel.Entity<CategoryId>
{
    //ef core
    private Category(CategoryId id) : base(id)
    {
    }

    private Category(
        CategoryId id,
        Name name,
        Category? parentCategory) : base(id)
    {
        Name = name;
        ParentCategory = parentCategory;
    }
    
    public Name Name { get; private set; }
    public Category? ParentCategory { get; private set; }
    
    public IReadOnlyList<Category> SubCategory => _subCategories;
    private readonly List<Category> _subCategories = [];
    
    public IReadOnlyList<Product> Products => _products;
    private readonly List<Product> _products = [];

    //category
    public static Result<Category, ErrorList> Create(
        CategoryId id,
        Name name,
        Category? parentCategory)
    {
        return new Category(
            id,
            name,
            parentCategory);
    }

    public UnitResult<Error> Update(Name name)
    {
        this.Name = name;
        return UnitResult.Success<Error>();
    }

    //subcategory
    public UnitResult<Error> AddSubCategory(
        Category subcategory)
    {
        if (_products.Count is not Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Invalid(ConstType.SubCategory));

        if (_subCategories.Count >= Constants.MAX_CATEGORY_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.SubCategory));
        
        _subCategories.Add(subcategory);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> DeleteSubCategory(
        Category subcategory)
    {
        if (_subCategories.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.SubCategory));

        _subCategories.Remove(subcategory);
        return UnitResult.Success<Error>();
    }

    
    //product
    public UnitResult<Error> AddProduct(
        Product product)
    {
        if (_products.Count >= Constants.MAX_PRODUCT_COUNT)
            return UnitResult.Failure(
                Errors.Domain.MaxCount(ConstType.Product));
        
        _products.Add(product);
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> RemoveProduct(
        Product product)
    {
        if (_products.Count is Constants.MIN_COUNT)
            return UnitResult.Failure<Error>(
                Errors.Domain.Empty(ConstType.Product));

        _products.Remove(product);
        return UnitResult.Success<Error>();
    }
}