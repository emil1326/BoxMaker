using TH = BoxMaker.core.TextHelpers;
using BH = BoxMaker.core.BoxHelpers;

namespace BoxMaker.core
{
    public class Box
    {
        public virtual int Height { get => BH.GetHeight(false, Texts); }
        public virtual int Width { get => BH.GetWidth(false, Texts); }
        public int Padding { get; set; }
        public int MaxPadding => GetMaxPadding(Boxes);
        public virtual object[] Boxes { get; set; } = [];
        public virtual string[] Texts { get; set; } = [];

        public Box()
        {
            Texts = [];
        }
        public Box(object[] boxes, int padding = 0)
        {
            Padding = padding;

            Boxes = boxes;

            Texts = HandleInput(boxes);

            Texts = BH.NormalizeLines(Texts);
        }

        string[] HandleInput(object[] boxes)
        {
            if (boxes == null)
                return [];

            var texts = new List<string>();
            for (int i = 0; i < boxes.Length; i++)
                texts.AddRange(HandleSingleInput(boxes[i]));

            return [.. texts];
        }
        string[] HandleSingleInput(object inputBox)
        {
            if (inputBox is Box box)
            {
                box.Padding = MaxPadding;
                return box.GetPaddedTexts();
            }
            else if (inputBox is string str)
            {
                return [str];
            }
            else
            {
                return [inputBox.ToString() ?? "Error: null object"];
            }
        }

        static int GetMaxPadding(object[] boxes)
        {
            if (boxes == null)
                return 0;

            int maxPadding = 0;
            foreach (var item in boxes)
            {
                if (item is Box box)
                {
                    if (box.Padding > maxPadding)
                        maxPadding = box.Padding;
                    continue;
                }

                // Allow objects that are not Box instances but still expose a Padding property.
                var paddingProp = item?.GetType().GetProperty("Padding");
                if (paddingProp != null && paddingProp.PropertyType == typeof(int))
                {
                    var value = paddingProp.GetValue(item);
                    if (value is int paddingValue && paddingValue > maxPadding)
                        maxPadding = paddingValue;
                }
            }

            return maxPadding;
        }

        protected virtual string EncloseWithBorders(string[] texts)
        {
            string res = string.Empty;
            res += HorizontalLine(texts);
            for (int i = 0; i < texts.Length; i++)
            {
                res += VerticalLine(texts[i]);
                res += HorizontalLine(texts);
            }
            return res;
        }
        protected virtual string VerticalLine(string text)
        {
            var safeText = TH.SplitSafe(text);
            string result = string.Empty;

            foreach (var line in safeText)
                result += $"|{line}|\n";

            return result;
        }
        protected virtual string HorizontalLine(string[] text)
        {
            int width = BH.GetWidth(false, text);
            string res = string.Empty;
            res += '+';
            res += new string('-', width);
            res += "+\n";
            return res;
        }

        public virtual string[] GetPaddedTexts()
        {
            return BH.PadTexts(Texts, Padding);
        }
        public virtual string GetText()
        {
            return EncloseWithBorders(GetPaddedTexts());
        }
        public override string ToString()
        {
            return GetText();
        }
    }
}