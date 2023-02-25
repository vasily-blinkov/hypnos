namespace Hypnos.Desktop.Utils
{
    public static class AuthenticationUtility
    {
        /// <summary>
        /// Authentication token.
        /// </summary>
        public static string Token { get; set; }

        public static short? UserId { get; set; }

        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token) && UserId.HasValue;
    }
}
