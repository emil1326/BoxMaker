namespace BoxMaker.core
{
    ///box that wraps another box, adding an outer border around it.
    public class MonoCombo : Box
    {
        public MonoCombo(Box innerBox, int padding = 0) : base([innerBox], padding)
        {
        }
        
        public MonoCombo(object box, int padding = 0) : base([box], padding)
        {
        }
    }
}
