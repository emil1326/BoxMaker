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
            if (Boxes == null || Boxes.Length == 0)
                return Texts;

            // Collect the individual boxes' content
            List<Box> boxList = new List<Box>();
            foreach (var item in Boxes)
            {
                if (item is Box boxItem)
                {
                    // Check if this box contains a ComboHorizontal child
                    if (boxItem.Boxes != null && boxItem.Boxes.Length == 1 && boxItem.Boxes[0] is ComboHorizontal ch)
                    {
                        // Extract the boxes from the inner ComboHorizontal
                        if (ch.Boxes != null)
                        {
                            foreach (var subBox in ch.Boxes)
                            {
                                if (subBox is Box b)
                                    boxList.Add(b);
                            }
                        }
                    }
                    else
                    {
                        // Regular box or ComboVertical, add it directly
                        boxList.Add(boxItem);
                    }
                }
            }

            // Get the string representation of each box
            List<string> boxStrings = new List<string>();
            foreach (Box box in boxList)
            {
                if (box is ComboVertical || box is ComboHorizontal)
                {
                    // For combo boxes, use GetPaddedTexts which returns properly formatted content
                    string[] paddedTexts = box.GetPaddedTexts();
                    string boxStr = string.Join("\n", paddedTexts);
                    boxStrings.Add(boxStr);
                }
                else
                {
                    // For regular boxes, split the texts and use them directly
                    List<string> lines = new List<string>();
                    foreach (string text in box.Texts)
                    {
                        string[] splitLines = TH.SplitSafe(text);
                        lines.AddRange(splitLines);
                    }
                    string boxStr = string.Join("\n", lines);
                    boxStrings.Add(boxStr);
                }
            }

            string[] result = VerticalLinesWithoutBorders(boxStrings.ToArray()).TrimEnd('\n').Split('\n');
            return result;
        }
    }
}