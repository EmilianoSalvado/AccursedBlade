public interface IObservable
{
    void Subscribe(IObserver obs);
    void NotifyToSubscribers();
    void Unsubscribe(IObserver obs);
}