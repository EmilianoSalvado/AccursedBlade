public interface IObservable
{
    void Subscribe(IObserver obs);
    void NotifyToSubscribers(string action);
    void Unsubscribe(IObserver obs);
}