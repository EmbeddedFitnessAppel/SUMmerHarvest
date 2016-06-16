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
            float expressionChance = chars.Count / 4f;
            float chanceRange = expressionChance;
            float randomVal = Random.value * chanceRange;

            print(string.Format("Chance: {0}, Val: {1}, Range: {2}", expressionChance, randomVal, chanceRange));

            if (randomVal < expressionChance * 1)
            {
                print(chars[0]);
            }
            else if (randomVal < expressionChance * 2)
            {
                print(chars[1]);
            }
            else if (randomVal < expressionChance * 3)
            {
                print(chars[2]);
            }
            else if (randomVal < expressionChance * 4)
            {
                print(chars[3]);
            }

            return number.ToString();
        }
    }
}