using System.Linq;
using System.Text;
using TH = BoxMaker.core.TextHelpers;
using BH = BoxMaker.core.BoxHelpers;

namespace BoxMaker.core
{
    public class ComboHorizontal : Box
    {
        private int _currentSegmentWidth;

        public override int Height => BH.GetHeight(false, Texts);
        public override int Width => BH.GetWidth(true, Texts) + Math.Max(0, Texts.Length - 1);
        public ComboHorizontal(int padding = 0, params object[] texts)
            : base(texts, padding)
        {
        }
        public ComboHorizontal(params object[] boxes) : base(boxes, 0)
        {
        }

        protected override string EncloseWithBorders(string[] texts)
        {
            string res = string.Empty;

            res += HorizontalLine(texts);
            res += VerticalLines(texts);
            res += HorizontalLine(texts);

            return res;
        }

        protected override string HorizontalLine(string[] text)
        {
            int width = text.Select(TH.GetStringWidth).Sum() + Math.Max(0, text.Length - 1);
            return "+" + new string('-', width) + "+\n";
        }

        protected string VerticalLines(string[] boxes)
        {
            string[][] splitBoxes = BH.SplitSafe(boxes);
            int[] widths = boxes.Select(TH.GetStringWidth).ToArray();
            StringBuilder result = new StringBuilder();

            for (int row = 0; row < Height; row++)
            {
                result.Append('|');

                for (int col = 0; col < splitBoxes.Length; col++)
                {
                    string line = row < splitBoxes[col].Length ? splitBoxes[col][row] : string.Empty;
                    _currentSegmentWidth = widths[col];
                    result.Append(VerticalLine(line));

                    if (col < splitBoxes.Length - 1)
                        result.Append('|');
                }

                result.Append("|\n");
            }

            return result.ToString();
        }

        override protected string VerticalLine(string text)
        {
            return text.PadRight(_currentSegmentWidth);
        }

        private string VerticalLinesWithoutBorders(string[] boxes)
        {
            string[][] splitBoxes = BH.SplitSafe(boxes);
            int[] widths = boxes.Select(TH.GetStringWidth).ToArray();
            StringBuilder result = new StringBuilder();

            int actualHeight = 0;
            foreach (string[] box in splitBoxes)
            {
                if (box.Length > actualHeight)
                    actualHeight = box.Length;
            }

            for (int row = 0; row < actualHeight; row++)
            {
                for (int col = 0; col < splitBoxes.Length; col++)
                {
                    string line = row < splitBoxes[col].Length ? splitBoxes[col][row] : string.Empty;
                    _currentSegmentWidth = widths[col];
                    result.Append(VerticalLine(line));

                    if (col < splitBoxes.Length - 1)
                        result.Append('|');
                }

                result.Append('\n');
            }

            return result.ToString();
        }

        public override string[] GetPaddedTexts()
        {
            string[] lines = VerticalLinesWithoutBorders(Texts).TrimEnd('\n').Split('\n');
            return lines;
        }
    }
}