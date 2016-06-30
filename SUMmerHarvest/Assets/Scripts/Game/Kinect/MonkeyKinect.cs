using Assets;
using Assets.Scripts.Game.Kinect;
using UnityEngine;

[RequireComponent(typeof(Monkey))]
public class MonkeyKinect : MonoBehaviour
{
    private Monkey monkey;
    private MonkeyGestureListener listener;
    private KinectManager manager;

    public int PlayerIndex;

    private void Start()
    {
        monkey = GetComponent<Monkey>();
        manager = KinectManager.Instance;
        listener = MonkeyGestureListener.Instance;
    }

    private void Update()
    {
        if (!manager)
        {
            Debug.LogWarning("Monkey " + name + " cannot use Kinect because the SDK is not initialized.");
            gameObject.SetActive(false);
            return;
        }

        // dont run Update() if there is no gesture listener
        if (!listener)
        {
            Debug.LogError("No gesture listener for monkey!");
            return;
        }

        var userId = manager.GetUserIdByIndex(PlayerIndex);
        var state = listener.GetState(userId);
        if (state == null) return;

        if (state.IsSwipeLeft() || state.IsSwipeRight())
        {
            print("Monkey " + PlayerIndex + ": Slam up");
        }
        else if (state.IsSwipeUp() || state.IsSwipeDown())
        {
            print("Monkey " + PlayerIndex + " Slam down");
        }
    }
}