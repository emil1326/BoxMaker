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
        public override int Width
        {
            get
            {
                int width = BH.GetWidth(true, Texts) + Math.Max(0, Texts.Length - 1);
                // at least the separator width if there are multiple boxes
                if (width == 0 && Boxes != null && Boxes.Length > 1)
                    return Boxes.Length - 1; 
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
            //handles empty combo boxes that only have separators
            if (width == 0 && Width > 0)
                width = Width;
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

            // collect individual boxes content
            List<Box> boxList = new List<Box>();
            foreach (object item in Boxes)
            {
                if (item is Box boxItem)
                {
                    // check if box contains a ComboHorizontal child
                    if (boxItem.Boxes != null && boxItem.Boxes.Length == 1 && boxItem.Boxes[0] is ComboHorizontal ch)
                    {
                        //extract boxes from the inner ComboHorizontal
                        if (ch.Boxes != null)
                        {
                            foreach (object subBox in ch.Boxes)
                            {
                                if (subBox is Box b)
                                    boxList.Add(b);
                            }
                        }
                    }
                    else
                    {
                        boxList.Add(boxItem);
                    }
                }
            }

            // get string representation of each box
            List<string> boxStrings = new List<string>();
            foreach (Box box in boxList)
            {
                if (box is ComboVertical || box is ComboHorizontal)
                {
                    // for combo boxes get the padded text and split
                    string[] paddedTexts = box.GetPaddedTexts();
                    string boxStr = string.Join("\n", paddedTexts);
                    boxStrings.Add(boxStr);
                }
                else
                {
                    // for regular boxes, split the texts and use them directly
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
            
            if (result.Length == 1 && result[0] == "")
                return [];
            
            return result;
        }
    }
}