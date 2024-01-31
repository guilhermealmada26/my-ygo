using System.Collections;
using TMPro;
using UnityEngine;

namespace BBG.Animations
{
    public class ShowStringAnimation
    {
        readonly TextMeshProUGUI text;
        readonly float speed;

        private string currentLine;

        public bool IsAnimating { private set; get; }

        public ShowStringAnimation(TextMeshProUGUI text, float speed)
        {
            this.text = text;
            this.speed = Mathf.Clamp(speed, 4, 44444);
        }

        public IEnumerator ShowLineAnimation(string line)
        {
            currentLine = line;
            IsAnimating = true;
            text.text = line;
            text.maxVisibleCharacters = 0;

            foreach (char c in line)
            {
                text.maxVisibleCharacters++;
                yield return new WaitForSeconds(1/speed);
            }
            IsAnimating = false;
        }

        public void Skip(MonoBehaviour caller)
        {
            IsAnimating = false;
            caller.StopAllCoroutines();
            text.maxVisibleCharacters = currentLine.Length;
        }
    }
}