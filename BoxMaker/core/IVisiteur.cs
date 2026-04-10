namespace BoxMaker.core
{
    // interface for implementing visitor pattern.
    public interface IVisiteur<T>
    {
        /// Called when entering a level deeper in the structure
        void Entrer();
        
        /// Called when exiting back up one level in the structure
        void Sortir();
        
        /// Called to visit an element
        void Visiter(T elem, Action opt);
    }
}
