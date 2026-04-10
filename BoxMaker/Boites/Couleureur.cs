using System.Linq;

namespace Boites
{
    /// Visitor that displays the structure of visited boxes with indentation.
    public class Couleureur : IVisiteur<Box>
    {
        private int _profondeur = -1;
        private readonly Stack<(string Kind, int Width, int Height)> _contexteParents = new();

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
            Console.WriteLine($"{indent}Boite");

            var parent = _contexteParents.Count > 0 ? _contexteParents.Peek() : default;
            bool aParent = _contexteParents.Count > 0;
            bool pousserContexte = elem is ComboHorizontal || elem is ComboVertical;
            if (pousserContexte)
            {
                _contexteParents.Push((elem.GetType().Name, elem.Width, elem.Height));
            }

            if (elem is MonoCombo)
            {
                Console.WriteLine($"{indent}  MonoCombo");
            }
            else if (elem is ComboHorizontal)
            {
                Console.WriteLine($"{indent}  ComboHorizontal");
            }
            else if (elem is ComboVertical)
            {
                Console.WriteLine($"{indent}  ComboVertical");
            }
            else if (elem is Box box)
            {
                if (box.Boxes == null || box.Boxes.Length == 0 || 
                    (box.Boxes.Length > 0 && box.Boxes.All(b => b is string)))
                {
                    int hauteurAffichee = aParent && parent.Kind == nameof(ComboHorizontal) ? parent.Height : elem.Height;
                    int largeurAffichee = aParent && parent.Kind == nameof(ComboVertical) ? parent.Width : elem.Width;
                    Console.WriteLine($"{indent}  Mono {hauteurAffichee} x {largeurAffichee}");
                }
            }

            // Continue visiting nested boxes
            opt();

            if (pousserContexte)
            {
                _contexteParents.Pop();
            }
        }
    }
}
