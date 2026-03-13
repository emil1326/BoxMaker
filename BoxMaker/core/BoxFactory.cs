namespace BoxMaker.core
{
    public static class BoxFactory
    {
        /// <summary>
        /// Creates a Box and returns its rendered text.
        /// This allows calling <c>Box(text, padding)</c> as a shortcut.
        /// </summary>
        public static string Box(string text, int padding = 0)
            => new Box(text, padding).GetText();
    }
}
