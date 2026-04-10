namespace BoxMaker.core
{
    public static class BoxFactory
    {
        /// Creates a Box and returns its rendered text.
        /// This allows calling Box(text, padding) as a shortcut.
        // public static string Box(string text, int padding = 0)
        //     => new Box(text, padding).GetText();

        public static Box Box(object text, int padding = 0)
            => new([text], padding);

        public static string Mono(string text, int padding = 0)
            => new Box([text], padding).ToString();

        public static string ComboHorizontal(int padding = 0, params object[] texts)
            => new ComboHorizontal(padding, texts).ToString();

        // Creates a box from a text description.
        // mono creates a Box with the given text
        // ch followed by two box descriptors - creates a ComboHorizontal
        // cv followed by two box descriptors - creates a ComboVertical
        // mc followed by one box descriptor - creates a MonoCombo
        public static Box Créer(string description)
        {
            string[] lines = description.Split('\n');
            (Box box, int _) = ParseBox(lines, 0);
            return box;
        }

        private static (Box box, int nextLineIndex) ParseBox(string[] lines, int startIndex)
        {
            if (startIndex >= lines.Length)
                throw new ArgumentException("Invalid box description: unexpected end of input");

            string currentLine = lines[startIndex].Trim();

            // mono TEXT - single line box
            if (currentLine.StartsWith("mono "))
            {
                string text = currentLine.Substring(5); // Remove "mono "
                return (new Box(text), startIndex + 1);
            }

            // ch - horizontal combo
            if (currentLine == "ch")
            {
                (Box left, int nextIndex1) = ParseBox(lines, startIndex + 1);
                (Box right, int nextIndex2) = ParseBox(lines, nextIndex1);
                return (new ComboHorizontal(left, right), nextIndex2);
            }

            // cv - vertical combo
            if (currentLine == "cv")
            {
                (Box top, int nextIndex1) = ParseBox(lines, startIndex + 1);
                (Box bottom, int nextIndex2) = ParseBox(lines, nextIndex1);
                return (new ComboVertical([top, bottom]), nextIndex2);
            }

            // mc - mono combo (bonus)
            if (currentLine == "mc")
            {
                (Box inner, int nextIndex) = ParseBox(lines, startIndex + 1);
                return (new MonoCombo(inner), nextIndex);
            }

            throw new ArgumentException($"Invalid box description: unknown prefix '{currentLine}'");
        }
    }
}
