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

                if (box is Box b)
                {
                    // get the padded text and split it properly
                    string[] paddedTexts = b.GetPaddedTexts();
                    List<string> splitLines = new List<string>();
                    foreach (string text in paddedTexts)
                    {
                        string[] lines = TH.SplitSafe(text);
                        splitLines.AddRange(lines);
                    }
                    boxLines = splitLines.ToArray();
                }
                else if (box is string str)
                {
                    boxLines = TH.SplitSafe(str);
                }
                else
                {
                    boxLines = [box?.ToString() ?? "Error: null object"];
                }

                // split lines and filter out empty
                List<string> nonEmptyLines = new List<string>();
                foreach (string line in boxLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        nonEmptyLines.Add(line);
                }

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
                // add content lines from this box
                foreach (string line in allBoxLines[i])
                {
                    // check if this is a separator line (only dashes)
                    if (line.Length > 0 && line.All(c => c == '-'))
                    {
                        // extend the separator line with more dashes
                        result.Add(new string('-', maxWidth));
                    }
                    else
                    {
                        //regular line: pad with spaces
                        result.Add(line.PadRight(maxWidth));
                    }
                }

                // add separator between boxes 
                if (i < allBoxLines.Count - 1)
                {
                    result.Add(new string('-', maxWidth));
                }
            }

            return [.. result];
        }
    }
}
