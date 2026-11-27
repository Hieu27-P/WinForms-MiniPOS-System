namespace MiniPOS.DTO
{
    public static class SessionManager
    {
        public static UserDTO CurrentUser { get; set; }

        public static void Clear()
        {
            CurrentUser = null;
        }
    }
}
