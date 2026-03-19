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
    }
}