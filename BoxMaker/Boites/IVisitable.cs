namespace Boites
{
    // interface for objects that can accept a visitor
    public interface IVisitable<T>
    {
        void Accepter(IVisiteur<T> viz);
    }
}
