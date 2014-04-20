namespace BSUIR.TermWork.ImageViewer.Shared
{
    public interface IHashGenerator
    {
        string GetPasswordHash(string password, string salt);

        string GenerateSalt(int length);
    }
}
