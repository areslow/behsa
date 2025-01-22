namespace bghBackend.Application.Hubs
{
    public interface IChatHubClient
    {
        Task RecieveMessage(string message);
    }
}
