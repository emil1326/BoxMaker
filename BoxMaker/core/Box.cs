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
            var s = TH.NormalizeString(text);
            s = TH.PadText(s, padding);
            s = EncloseWithBorders(s);
            Height = TH.GetStringHeight(s) + padding * 2;
            Width = TH.GetStringWidth(s) + padding * 2 + 2;

            Text = s;
        }

        string EncloseWithBorders(string text)
        {
            return DoLineTop() + DoSides(text) + DoLineTop();
        }
        string DoSides(string text)
        {
            var lines = text.Split('\n');
            string result = string.Empty;
            foreach (var line in lines)
            {
                result += $"|{line}|\n";
            }
            return result;
        }
        string DoLineTop()
        {
            return new string('-', Width) + "\n";
        }

        public string GetText()
        {
            return Text;
        }
    }
}