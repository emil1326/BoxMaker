using System.Linq;
using TH = BoxMaker.core.TextHelpers;
using BH = BoxMaker.core.BoxHelpers;

namespace BoxMaker.core
{
    public class ComboVertical : Box
    {
        public override int Height => BH.GetHeight(true, Texts);
        public override int Width => BH.GetWidth(false, Texts);
        public ComboVertical(object[] text, int padding = 0) : base(text, padding)
        {
        }
        public ComboVertical(params object[] boxes) : base(boxes, 0)
        {
        }

        public override string[] GetPaddedTexts()
        {
            if (Boxes == null || Boxes.Length == 0)
                return Texts;

            List<string> result = new List<string>();
            List<List<string>> allBoxLines = new List<List<string>>();

            for (int i = 0; i < Boxes.Length; i++)
            {
                object box = Boxes[i];
                string[] boxLines;

                if (box is ComboHorizontal || box is ComboVertical)
                {
                    // For Combo types, get their formatted output
                    Box b = (Box)box;
                    boxLines = b.GetPaddedTexts();
                }
                else if (box is Box b)
                {
                    // For regular Box, use Texts
                    boxLines = b.Texts;
                }
                else if (box is string str)
                {
                    boxLines = TH.SplitSafe(str);
                }
                else
                {
                    boxLines = [box?.ToString() ?? "Error: null object"];
                }

                // Split lines and filter out empty ones
                List<string> nonEmptyLines = new List<string>();
                foreach (string line in boxLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        nonEmptyLines.Add(line);
                }

                if (nonEmptyLines.Count > 0)
                    allBoxLines.Add(nonEmptyLines);
            }


            int maxWidth = 0;
            foreach (List<string> boxLines in allBoxLines)
            {
                foreach (string line in boxLines)
                {
                    if (line.Length > maxWidth)
                        maxWidth = line.Length;
                }
            }

            for (int i = 0; i < allBoxLines.Count; i++)
            {
                foreach (var line in allBoxLines[i])
                {
                    result.Add(line.PadRight(maxWidth));
                }

                if (i < allBoxLines.Count - 1)
                {
                    result.Add(new string('-', maxWidth));
                }
            }

            return [.. result];
        }
    }
}
