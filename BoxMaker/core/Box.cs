using TH = BoxMaker.core.TextHelpers;

namespace BoxMaker.core
{
    public class Box
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Padding { get; set; }
        public string Text { get; set; } = string.Empty;

        public Box(string text, int padding = 0)
        {
            Padding = padding;

            // Normalize once and calculate dimensions before building the padded text.
            var normalizedText = TH.NormalizeString(text);
            Height = TH.GetStringHeight(normalizedText) + padding * 2;
            Width = TH.GetStringWidth(normalizedText) + padding * 2 + 2;
            Text = TH.PadText(normalizedText, padding);
        }

        public string GetText()
        {
            return Text;
        }
    }
}