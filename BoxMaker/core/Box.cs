using TH = BoxMaker.core.TextHelpers;

namespace BoxMaker.core
{
    public class Box
    {
        public int Height { get => TH.GetStringHeight(Text); }
        public int Width { get => TH.GetStringWidth(Text); }
        public int Padding { get; set; }
        public string Text { get; set; } = string.Empty;

        public Box()
        {
            Text = string.Empty;
        }
        public Box(string text, int padding = 0)
        {
            Padding = padding;

            // Normalize once and calculate dimensions before building the padded text.
            Text = TH.NormalizeString(text);
            Text = TH.PadText(Text, padding);
            Text = EncloseWithBorders(Text);
        }

        string EncloseWithBorders(string text)
        {
            string res = string.Empty;
            res += HorizontalLine();
            res += VerticalLine(text);
            res += HorizontalLine();
            return res;
        }
        string VerticalLine(string text)
        {
            var lines = TH.SplitSafe(text);
            string result = string.Empty;
            foreach (var line in lines)
            {
                result += $"|{line}|\n";
            }
            return result;
        }
        string HorizontalLine()
        {
            string res = string.Empty;
            res += '+';
            res += new string('-', Width);
            res += "+\n";
            return res;
        }

        public virtual string GetText()
        {
            return Text;
        }
    }
}