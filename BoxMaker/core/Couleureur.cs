using System.Linq;

namespace BoxMaker.core
{
    /// Visitor that displays the structure of visited boxes with indentation.
    public class Couleureur : IVisiteur<Box>
    {
        private int _profondeur = 0;

        public void Entrer()
        {
            _profondeur++;
        }

        public void Sortir()
        {
            _profondeur--;
        }

        public void Visiter(Box elem, Action opt)
        {
            string indent = new string(' ', _profondeur * 2);
            
            // determine box type and color
            string typeName;
            ConsoleColor color;
            
            if (elem is MonoCombo)
            {
                typeName = "MonoCombo";
                color = ConsoleColor.Green;
            }
            else if (elem is ComboHorizontal)
            {
                typeName = "ComboHorizontal";
                color = ConsoleColor.Blue;
            }
            else if (elem is ComboVertical)
            {
                typeName = "ComboVertical";
                color = ConsoleColor.Red;
            }
            else
            {
                typeName = "Boite";
                color = ConsoleColor.Yellow;
            }

            Console.ForegroundColor = color;
            Console.WriteLine($"{indent}{typeName}");

            // if its a mono box display dimensions
            if (elem is Box box && elem is not MonoCombo && elem is not ComboHorizontal && elem is not ComboVertical)
            {
                if (box.Boxes == null || box.Boxes.Length == 0 || 
                    (box.Boxes.Length > 0 && box.Boxes.All(b => b is string)))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{indent}  Mono {elem.Height} x {elem.Width}");
                }
            }

            Console.ResetColor();

            // Continue visiting nested boxes
            opt();
        }
    }
}
