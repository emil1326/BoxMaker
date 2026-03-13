namespace BoxMaker.core
{
    static internal class TextHelpers
    {
        public static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text[..maxLength] + "...";
        }
        public static string NormalizeString(string text)
        {
            return text.Replace("\r\n", "\n").Replace("\r", "\n");
        }
        public static int GetStringHeight(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            var normalized = NormalizeString(text);
            return normalized.Split('\n').Length;
        }
        public static int GetStringWidth(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            var normalized = NormalizeString(text);
            var lines = normalized.Split('\n');
            int maxWidth = 0;
            foreach (var line in lines)
            {
                if (line.Length > maxWidth)
                    maxWidth = line.Length;
            }
            return maxWidth;
        }
        public static string GetEmptyLine(int width)
        {
            return new string(' ', width);
        }
        public static string GetMultiple(string text, int count)
        {
            string result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                result += text;
            }
            return result;
        }
        public static string PadText(string text, int padding)
        {
            text = NormalizeString(text);

            var lines = text.Split('\n');
            string paddedText = GetMultiple(GetEmptyLine(GetStringWidth(text) + padding * 2) + "\n", padding); // Start with a newline for top padding

            foreach (string line in lines)
            {
                paddedText += new string(' ', padding) + line + new string(' ', padding) + "\n";
            }

            paddedText += GetMultiple(GetEmptyLine(GetStringWidth(text) + padding * 2) + "\n", padding); // Add bottom padding

            return paddedText;
        }
    }
}