namespace Boites
{
    // visitor that measures boxes and tracks the smallest and largest
    public class Mesureur : IVisiteur<Box>
    {
        private Box? _plusPetite;
        private Box? _plusGrande;
        private int _superficiePlusPetite = int.MaxValue;
        private int _superficiePlusGrande = int.MinValue;

        // gets the box with the smallest area visited so far.
        public Box? PlusPetite => _plusPetite;

        // gets the box with the largest area visited so far.
        public Box? PlusGrande => _plusGrande;

        // gets the smallest area found.
        public int SuperficiePlusPetite => _superficiePlusPetite == int.MaxValue ? 0 : _superficiePlusPetite;

        // gets the largest area found.
        public int SuperficiePlusGrande => _superficiePlusGrande == int.MinValue ? 0 : _superficiePlusGrande;

        public void Entrer()
        {
        }

        public void Sortir()
        {
        }

        public void Visiter(Box elem, Action opt)
        {
            int superficie = elem.Width * elem.Height;

            // track smallest box
            if (superficie < _superficiePlusPetite)
            {
                _superficiePlusPetite = superficie;
                _plusPetite = elem;
            }

            // track biggest box
            if (superficie > _superficiePlusGrande)
            {
                _superficiePlusGrande = superficie;
                _plusGrande = elem;
            }

            // continue visiting nested boxes
            opt();
        }
    }
}
