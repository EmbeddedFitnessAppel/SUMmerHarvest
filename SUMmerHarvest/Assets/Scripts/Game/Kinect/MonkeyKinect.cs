using Assets;
using Assets.Scripts.Game.Kinect;
using UnityEngine;

[RequireComponent(typeof(Monkey))]
public class MonkeyKinect : MonoBehaviour
{
    private Monkey monkey;
    private MonkeyGestureListener listener;
    private KinectManager manager;


    private void Start()
    {
        monkey = GetComponent<Monkey>();
        manager = KinectManager.Instance;
        listener = MonkeyGestureListener.Instance;
    }

    private void Update()
    {
        // dont run Update() if there is no gesture listener
        if (!listener)
        {
            Debug.LogError("No gesture listener for monkey!");
            return;
        }

        if (listener.IsSwipeLeft() || listener.IsSwipeRight())
        {
            print("Monkey " + (monkey.PlayerNumber - 1) + ": Slam up");
        }
        else if (listener.IsSwipeUp() || listener.IsSwipeDown())
        {
            print("Monkey " + (monkey.PlayerNumber - 1) + " Slam down");
        }
    }
}