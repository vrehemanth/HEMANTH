namespace EGI_Backend.Application.Utilities
{
    /// <summary>
    /// Provides shared utility methods for password generation.
    /// Centralizes the logic to avoid duplication across services (DRY principle).
    /// </summary>
    public static class PasswordHelper
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";

        public static string GenerateTemporaryPassword(int length = 10)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
