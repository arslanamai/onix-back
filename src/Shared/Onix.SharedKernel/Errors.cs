namespace Onix.SharedKernel;

public static class Errors
{
    private const string DefaultValue = "value";
    
    public static class General
    {
        public static Error NotFound(string name)
        {
            return Error.NotFound($"{name}.not.found", $"{name} not found");
        }
    }
    
    public static class Domains
    {
        public static Error Required(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.is.required", $"{name} is required");
        }
        
        public static Error Invalid(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.is.invalid", $"{name} is invalid");
        }
        
        public static Error AlreadyExist(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.already.exists", $"{name} already exists");
        }
        
        public static Error MaxCount(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.max.count", $"{name} exceeds the maximum allowed count");
        }
        
        public static Error Empty(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.is.empty", $"{name} cannot be empty");
        }
        
        public static Error MaxLength(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.max.length", $"{name} exceeds the maximum allowed length");
        }
        
        public static Error MinLength(string? name = DefaultValue)
        {
            return Error.Validation($"{name}.min.length", $"{name} is shorter than the minimum allowed length");
        }
    }
}