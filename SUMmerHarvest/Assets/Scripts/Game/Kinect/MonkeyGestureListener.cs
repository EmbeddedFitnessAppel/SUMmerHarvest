using UnityEngine;

namespace Assets.Scripts.Game.Kinect
{
    internal class MonkeyGestureListener : Singleton<MonkeyGestureListener>, KinectGestures.GestureListenerInterface
    {
        // internal variables to track if progress message has been displayed
        private bool progressDisplayed;
        private float progressGestureTime;

        // whether the needed gesture has been detected or not
        private bool swipeLeft;
        private bool swipeRight;
        private bool swipeUp;
        private bool swipeDown;


        /// <summary>
        ///     Invoked when a new user is detected. Here you can start gesture tracking by invoking
        ///     KinectManager.DetectGesture()-function.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="userIndex">User index</param>
        public void UserDetected(long userId, int userIndex)
        {
            // the gestures are allowed for the primary user only
            var manager = KinectManager.Instance;
            if (!manager || (userId != manager.GetPrimaryUserID()))
                return;

            // detect these user specific gestures
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);

            //if (gestureInfo != null)
            //{
            //    gestureInfo.GetComponent<GUIText>().text = "Swipe left, right or up to change the slides.";
            //}
        }

        /// <summary>
        ///     Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="userIndex">User index</param>
        public void UserLost(long userId, int userIndex)
        {
            // the gestures are allowed for the primary user only
            var manager = KinectManager.Instance;
            if (!manager || (userId != manager.GetPrimaryUserID()))
                return;

            //if (gestureInfo != null)
            //{
            //    gestureInfo.GetComponent<GUIText>().text = string.Empty;
            //}
        }

        /// <summary>
        ///     Invoked when a gesture is in progress.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="userIndex">User index</param>
        /// <param name="gesture">Gesture type</param>
        /// <param name="progress">Gesture progress [0..1]</param>
        /// <param name="joint">Joint type</param>
        /// <param name="screenPos">Normalized viewport position</param>
        public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
            float progress, KinectInterop.JointType joint, Vector3 screenPos)
        {
            // the gestures are allowed for the primary user only
            var manager = KinectManager.Instance;
            if (!manager || (userId != manager.GetPrimaryUserID()))
                return;
        }

        /// <summary>
        ///     Invoked if a gesture is completed.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="userId">User ID</param>
        /// <param name="userIndex">User index</param>
        /// <param name="gesture">Gesture type</param>
        /// <param name="joint">Joint type</param>
        /// <param name="screenPos">Normalized viewport position</param>
        public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
            KinectInterop.JointType joint, Vector3 screenPos)
        {
            // the gestures are allowed for the primary user only
            var manager = KinectManager.Instance;
            if (!manager || (userId != manager.GetPrimaryUserID()))
                return false;

            if (gesture == KinectGestures.Gestures.SwipeLeft)
                swipeLeft = true;
            else if (gesture == KinectGestures.Gestures.SwipeRight)
                swipeRight = true;
            else if (gesture == KinectGestures.Gestures.SwipeUp)
                swipeUp = true;
            else if (gesture == KinectGestures.Gestures.SwipeDown)
                swipeDown = true;

            return true;
        }

        /// <summary>
        ///     Invoked if a gesture is cancelled.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="userId">User ID</param>
        /// <param name="userIndex">User index</param>
        /// <param name="gesture">Gesture type</param>
        /// <param name="joint">Joint type</param>
        public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
            KinectInterop.JointType joint)
        {
            // the gestures are allowed for the primary user only
            var manager = KinectManager.Instance;
            if (!manager || (userId != manager.GetPrimaryUserID()))
                return false;

            return true;
        }

        /// <summary>
        ///     Determines whether swipe left is detected.
        /// </summary>
        /// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
        public bool IsSwipeLeft()
        {
            if (swipeLeft)
            {
                swipeLeft = false;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines whether swipe right is detected.
        /// </summary>
        /// <returns><c>true</c> if swipe right is detected; otherwise, <c>false</c>.</returns>
        public bool IsSwipeRight()
        {
            if (swipeRight)
            {
                swipeRight = false;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines whether swipe up is detected.
        /// </summary>
        /// <returns><c>true</c> if swipe up is detected; otherwise, <c>false</c>.</returns>
        public bool IsSwipeUp()
        {
            if (swipeUp)
            {
                swipeUp = false;
                return true;
            }

            return false;
        }

        public bool IsSwipeDown()
        {
            if (swipeDown)
            {
                swipeDown = false;
                return true;
            }

            return false;
        }

        private void Update()
        {
        }
    }
}