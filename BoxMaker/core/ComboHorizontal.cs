using System.Linq;

namespace BoxMaker.core
{
    public class ComboHorizontal : Box
    {
        char[,] Text2d = new char[0, 0];

        public ComboHorizontal(int padding = 0, params string[] texts) : base()
        {
            int height = BoxHelpers.GetHeight(false, texts);
            int width = BoxHelpers.GetWidth(true, texts) - 3; // Subtract 3 to account for the borders of the first box.

            Text2d = new char[height, width];

            foreach (var text in texts)
            {
                Text2d = BoxHelpers.SuperImposeMatrices(Text2d, BoxHelpers.ToCharMatrix(text), BoxHelpers.GetWidth(false, text) - 1);
            }
        }

        public override string GetText()
        {
            return BoxHelpers.FromCharMatrix(Text2d);
        }
    }
}