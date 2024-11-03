namespace Onix.SharedKernel;

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.NotFound("value.not.found", $"value not found {forId}");
        }
        
        public static Error NotFound(string? value = "value")
        {
            return Error.NotFound("value.not.found", $"{value} not vound");
        }

        public static Error WrongType(string? value = "value")
        {
            return Error.Validation("value.is.wrong.type", $"{value} is wrong type");
        }
    }
    
    public static class Domain
    {
        public static Error Required(string? value = "value")
        {
            return Error.Validation("value.is.required", $"{value} is required");
        }
        
        public static Error Invalid(string? value = "value")
        {
            return Error.Validation("value.is.invalid", $"{value} is invalid");
        }
        
        public static Error AlreadyExist(string? value = "value")
        {
            return Error.Validation("value.already.exist", $"{value} already exist");
        }

        public static Error MaxCount(string? value = "value")
        {
            return Error.Failure("value.max.count", $"{value} exceeds maximum allowed count");
        }
        
        public static Error Empty(string? value = "value")
        {
            return Error.Failure("value.empty", $"{value} cannot be empty");
        }

        public static Error MaxLength(string? value = "value")
        {
            return Error.Validation("value.max.length", $"{value} exceeds maximum allowed length");
        }
        
        public static Error MinLength(string? value = "value")
        {
            return Error.Validation("value.min.length", $"{value} exceeds min allowed length");
        }
    }
}