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

    public static Result<List<Faq>> Create(List<Faq> faqs)
    {
        return new List<Faq>(faqs);
    }
}