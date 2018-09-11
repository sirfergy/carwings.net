namespace carwings.net
{
    public interface ILoginProvider
    {
        string Username { get; }

        string GetEncryptedPassword(string encryptionKey);
    }
}
