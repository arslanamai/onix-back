using CSharpFunctionalExtensions;
using Onix.SharedKernel;

namespace Onix.WebSites.Domain.WebSites.ValueObjects;

public class Faq
{
    //ef core 
    public Faq()
    {
        
    }
    
    private Faq(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }

    public string Question { get; init; }
    public string Answer { get; init; }

    public static Result<Faq> Create(string question, string answer)
    {
        return new Faq(question, answer);
    }
}