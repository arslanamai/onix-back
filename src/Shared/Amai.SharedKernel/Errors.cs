namespace Amai.SharedKernel;

public static class Errors
{
    private const string DefaultValue = "value";

    public static class General
    {
        public static Error Unexpected(string? detail = null) =>
            Error.Failure("general.unexpected", "An unexpected error occurred");

        public static Error NotFound(string name = DefaultValue) =>
            Error.NotFound($"{name}.not.found", $"{name} not found");

        public static Error Custom(string code, string message) =>
            Error.Failure(code, message);
        
        public static Error AlreadyExists(string field = DefaultValue) =>
            Error.Conflict($"{field}.already.exists", $"{field} already exists");
        
        public static Error JsonParseError(string field = DefaultValue) =>
            Error.Conflict($"{field}.failed.parse", $"{field} failed to parse");
    }

    public static class Validation
    {
        public static Error Required(string field = DefaultValue) =>
            Error.Validation($"{field}.required", $"{field} is required", field);

        public static Error Invalid(string field = DefaultValue) =>
            Error.Validation($"{field}.invalid", $"{field} is invalid", field); 

        public static Error MaxLength(string field = DefaultValue) =>
            Error.Validation($"{field}.max.length", $"{field} exceeds the maximum allowed length", field);

        public static Error MinLength(string field = DefaultValue) =>
            Error.Validation($"{field}.min.length", $"{field} is shorter than the minimum allowed length", field);
    }

    public static class Authentication
    {
        public static Error TokenError() =>
            Error.Failure("auth.token.error", "Token is invalid or missing");

        public static Error LoginFailed() =>
            Error.Failure("auth.login.failed", "Login failed");

        public static Error RegistrationFailed() =>
            Error.Failure("auth.registration.failed", "Registration failed");

        public static Error Unauthorized() =>
            Error.Failure("auth.unauthorized", "Unauthorized access");
    }
}
