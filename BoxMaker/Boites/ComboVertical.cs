using System.Linq;
using TH = Boites.TextHelpers;
using BH = Boites.BoxHelpers;

namespace Boites
{
    public class ComboVertical : Box
    {
        public override int Height
        {
            get
            {
                if (Boxes == null || Boxes.Length == 0)
                    return BH.GetHeight(true, Texts);

                int height = 0;
                foreach (string[] child in GetChildLines())
                {
                    height += child.Length;
                }

                if (Boxes.Length > 1)
                    height += Boxes.Length - 1;

                return height;
            }
        }

        public override int Width
        {
            get
            {
                if (Boxes == null || Boxes.Length == 0)
                    return BH.GetWidth(false, Texts);

                int width = 0;
                foreach (string[] child in GetChildLines())
                {
                    int childWidth = GetLineWidth(child);
                    if (childWidth > width)
                        width = childWidth;
                }

                return width;
            }
        }
        public ComboVertical(object[] text, int padding = 0) : base(text, padding)
        {
        }
        public ComboVertical(params object[] boxes) : base(boxes, 0)
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
                {
                    childLines.Add(TH.SplitSafe(str));
                }
                else
                {
                    childLines.Add(TH.SplitSafe(box?.ToString() ?? string.Empty));
                }
            }

            return childLines;
        }

        public override string[] GetPaddedTexts()
        {
            if (Boxes == null || Boxes.Length == 0)
                return Texts;

            List<string[]> allBoxLines = GetChildLines();
            int maxWidth = Width;

            List<string> result = new List<string>();
            for (int i = 0; i < allBoxLines.Count; i++)
            {
                foreach (string line in allBoxLines[i])
                {
                    if (line.Length > 0 && line.All(c => c == '-'))
                        result.Add(new string('-', maxWidth));
                    else
                        result.Add(line.PadRight(maxWidth));
                }

                if (i < allBoxLines.Count - 1)
                    result.Add(new string('-', maxWidth));
            }

            return [.. result];
        }
    }
}
