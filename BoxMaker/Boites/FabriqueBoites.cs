namespace Boites
{
    public static class FabriqueBoites
    {
        public static Box Box(object text, int padding = 0)
            => new Box(new object[] { text }, padding);

        public static string Mono(string text, int padding = 0)
            => new Box(new string[] { text }, padding).ToString();

        public static string ComboHorizontal(int padding = 0, params object[] texts)
            => new ComboHorizontal(padding, texts).ToString();

        public static Box Créer(string description)
        {
            string[] lines = description.Split('\n');
            (Box box, int _) = ParseBox(lines, 0);
            return box;
        }

        private static (Box box, int nextLineIndex) ParseBox(string[] lines, int startIndex)
        {
            if (startIndex >= lines.Length)
                throw new System.ArgumentException("Invalid box description: unexpected end of input");

            string currentLine = lines[startIndex].Trim();

            if (currentLine.StartsWith("mono "))
            {
                string text = currentLine.Substring(5);
                return (new Box(text), startIndex + 1);
            }

            if (currentLine == "ch")
            {
                (Box left, int nextIndex1) = ParseBox(lines, startIndex + 1);
                (Box right, int nextIndex2) = ParseBox(lines, nextIndex1);
                return (new ComboHorizontal(left, right), nextIndex2);
            }

            if (currentLine == "cv")
            {
                (Box top, int nextIndex1) = ParseBox(lines, startIndex + 1);
                (Box bottom, int nextIndex2) = ParseBox(lines, nextIndex1);
                return (new ComboVertical(new object[] { top, bottom }), nextIndex2);
            }

            if (currentLine == "mc")
            {
                (Box inner, int nextIndex) = ParseBox(lines, startIndex + 1);
                return (new MonoCombo(inner), nextIndex);
            }

            throw new System.ArgumentException($"Invalid box description: unknown prefix '{currentLine}'");
        }
    }
}
