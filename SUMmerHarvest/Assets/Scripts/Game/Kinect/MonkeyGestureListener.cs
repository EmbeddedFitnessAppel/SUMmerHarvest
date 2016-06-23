using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Kinect
{
    internal class MonkeyGestureListener : Singleton<MonkeyGestureListener>, KinectGestures.GestureListenerInterface
    {
        private readonly Dictionary<long, PlayerGestureState> states = new Dictionary<long, PlayerGestureState>(4);

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
            if (!manager)
                return;

            // detect these user specific gestures
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        }

        /// <summary>
        ///     Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="userIndex">User index</param>
        public void UserLost(long userId, int userIndex)
        {
            var manager = KinectManager.Instance;
            if (!manager)
                return;

            states.Remove(userId);
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
            var manager = KinectManager.Instance;
            if (!manager)
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
            var manager = KinectManager.Instance;
            if (!manager)
                return false;

            var state = GetStateOrAdd(userId);

            if (gesture == KinectGestures.Gestures.SwipeLeft)
                state.SwipeLeft = true;
            else if (gesture == KinectGestures.Gestures.SwipeRight)
                state.SwipeRight = true;
            else if (gesture == KinectGestures.Gestures.SwipeUp)
                state.SwipeUp = true;
            else if (gesture == KinectGestures.Gestures.SwipeDown)
                state.SwipeDown = true;

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
            var manager = KinectManager.Instance;
            if (!manager)
                return false;

            return true;
        }

        public PlayerGestureState GetState(long userId)
        {
            if (!states.ContainsKey(userId)) return null;
            return states[userId];
        }

        private PlayerGestureState GetStateOrAdd(long userId)
        {
            if (!states.ContainsKey(userId)) states.Add(userId, new PlayerGestureState());
            return states[userId];
        }

        private void Update()
        {
        }

        public class PlayerGestureState
        {
            // whether the needed gesture has been detected or not
            public bool SwipeLeft { private get; set; }
            public bool SwipeRight { private get; set; }
            public bool SwipeUp { private get; set; }
            public bool SwipeDown { private get; set; }

            /// <summary>
            ///     Determines whether swipe left is detected.
            /// </summary>
            /// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
            public bool IsSwipeLeft()
            {
                if (SwipeLeft)
                {
                    SwipeLeft = false;
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
                if (SwipeRight)
                {
                    SwipeRight = false;
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
                if (SwipeUp)
                {
                    SwipeUp = false;
                    return true;
                }

                return false;
            }

            public bool IsSwipeDown()
            {
                if (SwipeDown)
                {
                    SwipeDown = false;
                    return true;
                }

                return false;
            }
        }
    }
}