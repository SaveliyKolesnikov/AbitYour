namespace AbitYour.Models
{
    public interface IAbitYourParser
    {
        IResultList Parse(string url, string userName, string userScore);
    }
}