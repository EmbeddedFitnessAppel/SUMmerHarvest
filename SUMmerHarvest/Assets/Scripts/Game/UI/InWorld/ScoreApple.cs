using System.Collections.Generic;
using Assets.Scripts.Game.GameObjects;
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

        [Header("Possible expressions")]
        public bool Addition;

        public bool Substraction;
        public bool Multiplication;
        public bool Division;


        private void Start()
        {
            text = gameObject.GetComponent<Text>();
            rect = text.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (appleScript == null) return;

            if (string.IsNullOrEmpty(text.text)) text.text = NumberToExpression(appleScript.GetNumber());

            rect.position = new Vector3(appleScript.transform.position.x,
                appleScript.transform.position.y + YAxisOffset,
                transform.position.z);
        }

        public void SetApple(Apple other)
        {
            appleScript = other;
            appleScript.SetAppleUI(this);
            gameObject.name = "appletext: " + appleScript.GetNumber();
        }

        private string NumberToExpression(int number)
        {
            List<char> chars = new List<char>();

            if (Addition) chars.Add('+');
            if (Substraction) chars.Add('-');
            if (Multiplication) chars.Add('*');
            if (Division) chars.Add('/');
            float randomVal = Random.value * (.25f * chars.Count);

            char currentOp = chars[(int)(randomVal * 4 - 1)];
            float rand = Random.value;
            float left;
            float right;

            switch (currentOp)
            {
                case '+':
                    left = number * Random.value;
                    right = number - left;

                    left = Mathf.CeilToInt(left);
                    right = Mathf.FloorToInt(right);

                    // Remove 1 from the other if it is 0.
                    if (left == 0)
                    {
                        left++;
                        right--;
                    }
                    else if (right == 0)
                    {
                        right++;
                        left--;
                    }

                    return left + " + " + right;
                case '-':
                    float newNum = number;

                    // Possibly add this line to not get higher values than maxvalue.
                    //float newNum = number / 2f;

                    left = Mathf.RoundToInt(newNum * (rand + 1));
                    right = Mathf.RoundToInt(newNum * rand);

                    return left + " - " + right;
                case '*':
                    left = 0;
                    right = 0;

                    return left + " * " + right;
                case '/':

                    return "";
            }

            return number.ToString();
        }
    }
}