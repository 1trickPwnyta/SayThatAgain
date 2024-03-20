namespace SayThatAgain
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{SayThatAgainMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
