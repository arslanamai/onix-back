using CSharpFunctionalExtensions;
using Path = Onix.SharedKernel.ValueObjects.Path;

namespace Onix.WebSites.Domain.Media;

public class Favicon
{
    private Favicon(Path path)
    {
        Path = path;
    }

    public Path Path { get; private set; }

    public static Result<Favicon> Create(Path path)
    {
        return new Favicon(path);
    }
}