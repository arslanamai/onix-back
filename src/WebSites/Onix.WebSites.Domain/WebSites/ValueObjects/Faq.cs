using CSharpFunctionalExtensions;
using Onix.SharedKernel;

namespace Onix.WebSites.Domain.WebSites.ValueObjects;

public class Faq
{
    private Faq(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }

    public string Question { get; }
    public string Answer { get; }

    public static Result<Faq> Create(string question, string answer)
    {
        return new Faq(question, answer);
    }

    public Result<Faq, Error> Update(string question, string answer)
    {
        return new Faq(question, answer);
    }
}