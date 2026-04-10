using System.Linq;
using System.Text;
using TH = Boites.TextHelpers;
using BH = Boites.BoxHelpers;

namespace Boites
{
    public class ComboHorizontal : Box
    {
        private int _currentSegmentWidth;

        public override int Height
        {
            get
            {
                if (Boxes == null || Boxes.Length == 0)
                    return BH.GetHeight(false, Texts);

                int height = 0;
                foreach (string[] child in GetChildLines())
                {
                    if (child.Length > height)
                        height = child.Length;
                }

                return height;
            }
        }
        public override int Width
        {
            get
            {
                if (Boxes == null || Boxes.Length == 0)
                    return BH.GetWidth(true, Texts) + Math.Max(0, Texts.Length - 1);

                int width = 0;
                int columnCount = 0;
                foreach (string[] child in GetChildLines())
                {
                    width += GetLineWidth(child);
                    columnCount++;
                }

                if (columnCount > 1)
                    width += columnCount - 1;

                return width;
            }
        }
        public ComboHorizontal(int padding = 0, params object[] texts)
            : base(texts, padding)
        {
        }
        public ComboHorizontal(params object[] boxes) : base(boxes, 0)
        {
        }

        private static int GetLineWidth(string[] lines)
        {
            int width = 0;
            foreach (string line in lines)
            {
                int lineWidth = TH.GetStringWidth(line);
                if (lineWidth > width)
                    width = lineWidth;
            }

            return width;
        }

        private List<string[]> GetChildLines()
        {
            var childLines = new List<string[]>();

            if (Boxes == null)
                return childLines;

            foreach (object box in Boxes)
            {
                if (box is Box childBox)
                {
                    List<string> flattened = new List<string>();
                    foreach (string text in childBox.GetPaddedTexts())
                        flattened.AddRange(TH.SplitSafe(text));
                    childLines.Add([.. flattened]);
                }
                else if (box is string str)
                    childLines.Add(TH.SplitSafe(str));
                else
                    childLines.Add(TH.SplitSafe(box?.ToString() ?? string.Empty));
            }

            return childLines;
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
            int width = BH.GetWidth(false, text);
            if (width == 0 && Width > 0)
                width = Width;
            return "+" + new string('-', width) + "+\n";
        }

        protected string VerticalLines(string[] boxes)
        {
            StringBuilder result = new StringBuilder();

            foreach (string line in boxes)
            {
                result.Append('|');
                result.Append(line);
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

            int maxHeight = 0;
            foreach (string[] box in splitBoxes)
            {
                if (box.Length > maxHeight)
                    maxHeight = box.Length;
            }

            for (int row = 0; row < maxHeight; row++)
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
            if (Boxes == null || Boxes.Length == 0)
                return Texts;

            List<string[]> childLines = GetChildLines();
            int[] widths = childLines.Select(GetLineWidth).ToArray();
            int maxHeight = Height;

            List<string> result = new List<string>();
            for (int row = 0; row < maxHeight; row++)
            {
                StringBuilder line = new StringBuilder();
                for (int col = 0; col < childLines.Count; col++)
                {
                    string segment = row < childLines[col].Length ? childLines[col][row] : string.Empty;
                    line.Append(segment.PadRight(widths[col]));
                    if (col < childLines.Count - 1)
                        line.Append('|');
                }

                result.Add(line.ToString());
            }

            return [.. result];
        }
    }
}