namespace CouperfectServer.WebApp.Services.Token;

public partial class TokenService
{
    public class Options
    {
        public required byte[] TokenKey { get; set; }
    }
}
