using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.UI.InWorld
{
    public class ScoreApple : MonoBehaviour
    {
        public float YAxisOffset;
        private Apple appleScript;
        private Text text;
        private RectTransform rect;

        private void Start()
        {
            text = gameObject.GetComponent<Text>();
            rect = text.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (appleScript != null)
            {
                text.text = appleScript.GetNumber().ToString();
                rect.position = new Vector3(appleScript.transform.position.x,
                    appleScript.transform.position.y + YAxisOffset,
                    transform.position.z);
            }
        }

        public void SetApple(Apple other)
        {
            appleScript = other;
            appleScript.SetAppleUI(this);
            gameObject.name = "appletext: " + appleScript.GetNumber();
        }
    }
}