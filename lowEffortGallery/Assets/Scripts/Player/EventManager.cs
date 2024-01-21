using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnItemHeld = new UnityEvent();
    public static UnityEvent OnItemDroped = new UnityEvent();
    public static UnityEvent OnPause = new UnityEvent();
    public static UnityEvent OnResumeGame = new UnityEvent();
    public static UnityEvent OnNewGame = new UnityEvent();
}