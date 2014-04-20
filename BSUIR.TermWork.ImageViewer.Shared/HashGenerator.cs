namespace BSUIR.TermWork.ImageViewer.Shared
{
    public sealed class HashGenerator : IHashGenerator
    {
        public string GetPasswordHash(string password, string salt)
        {
            return CryptoHelper.ComputePasswordHash(password, salt);
        }

        public string GenerateSalt(int length)
        {
            return CryptoHelper.GenerateSalt(length);
        }
    }
}
