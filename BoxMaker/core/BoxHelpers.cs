namespace BoxMaker.core
{
    static internal class BoxHelpers
    {
        public static int GetHeight(bool additive = false, params string[] boxes)
        {
            if (additive)
            {
                int totalHeight = 0;
                foreach (string box in boxes)
                {
                    totalHeight += TextHelpers.GetStringHeight(box);
                }
                return totalHeight;
            }

            int maxHeight = 0;
            foreach (string box in boxes)
            {
                int height = TextHelpers.GetStringHeight(box);
                if (height > maxHeight)
                    maxHeight = height;
            }
            return maxHeight;
        }

        public static int GetWidth(bool additive = false, params string[] boxes)
        {
            if (additive)
            {
                int totalWidth = 0;
                foreach (string box in boxes)
                {
                    totalWidth += TextHelpers.GetStringWidth(box);
                }
                return totalWidth;
            }

            int maxWidth = 0;
            foreach (string box in boxes)
            {
                int width = TextHelpers.GetStringWidth(box);
                if (width > maxWidth)
                    maxWidth = width;
            }
            return maxWidth;
        }

        /// <summary>
        /// Converts a multi-line string into a 2D char array.
        /// Each row corresponds to a line; shorter lines are padded with spaces.
        /// </summary>
        public static char[,] ToCharMatrix(string text)
        {
            if (text == null)
                return new char[0, 0];

            var lines = TextHelpers.SplitSafe(text);
            if (lines.Length == 0)
                return new char[0, 0];

            int height = lines.Length;
            int width = 0;
            foreach (var line in lines)
            {
                if (line.Length > width)
                    width = line.Length;
            }

            var matrix = new char[height, width];
            for (int r = 0; r < height; r++)
            {
                var line = lines[r];
                for (int c = 0; c < width; c++)
                {
                    matrix[r, c] = c < line.Length ? line[c] : ' ';
                }
            }

            return matrix;
        }

        /// <summary>
        /// Converts a 2D char array back into a multi-line string.
        /// Each row becomes a line separated by '\n'.
        /// </summary>
        public static string FromCharMatrix(char[,] matrix, bool trimEndOfLine = true)
        {
            if (matrix == null || matrix.Length == 0)
                return string.Empty;

            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            var lines = new string[height];

            for (int r = 0; r < height; r++)
            {
                var rowChars = new char[width];
                for (int c = 0; c < width; c++)
                {
                    rowChars[c] = matrix[r, c];
                }

                var line = new string(rowChars);
                if (trimEndOfLine)
                    line = line.TrimEnd();

                lines[r] = line;
            }

            return string.Join("\n", lines);
        }

        public static char[,] SuperImposeMatrices(char[,] baseMatrix, char[,] overlayMatrix, int offsetX = 0, int offsetY = 0)
        {
            if (baseMatrix == null || baseMatrix.Length == 0)
                return overlayMatrix ?? new char[0, 0];
            if (overlayMatrix == null || overlayMatrix.Length == 0)
                return baseMatrix;

            int baseHeight = baseMatrix.GetLength(0);
            int baseWidth = baseMatrix.GetLength(1);
            int overlayHeight = overlayMatrix.GetLength(0);
            int overlayWidth = overlayMatrix.GetLength(1);

            var result = new char[baseHeight, baseWidth];
            for (int r = 0; r < baseHeight; r++)
            {
                for (int c = 0; c < baseWidth; c++)
                {
                    result[r, c] = baseMatrix[r, c];
                }
            }

            for (int r = 0; r < overlayHeight; r++)
            {
                for (int c = 0; c < overlayWidth; c++)
                {
                    char overlayChar = overlayMatrix[r, c];
                    if (overlayChar != ' ')
                    {
                        int targetR = r + offsetY;
                        int targetC = c + offsetX;
                        if (targetR >= 0 && targetR < baseHeight && targetC >= 0 && targetC < baseWidth)
                        {
                            result[targetR, targetC] = overlayChar;
                        }
                    }
                }
            }

            return result;
        }
    }
}