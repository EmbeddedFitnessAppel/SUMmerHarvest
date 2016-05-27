using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.UI.Menus
{
    public class MenuMain : MonoBehaviour
    {
        /// <summary>
        /// Loads the 'Game' scene.
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        /// <summary>
        /// Closes the game (or exists play-mode).
        /// </summary>
        public void CloseGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
        }
    }
}