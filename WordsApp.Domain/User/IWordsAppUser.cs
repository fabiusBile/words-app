namespace WordsApp.Domain.User;

public interface IWordsAppUser
{
    string UserName { get; }
    Guid UserId { get; }
    string ProfilePicture { get; }
    string AuthenticationType { get; }
}