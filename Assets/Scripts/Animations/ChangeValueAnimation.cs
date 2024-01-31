using System.Collections;
using TMPro;
using UnityEngine;

namespace BBG.Animations
{
    public class ChangeValueAnimation
    {
        private readonly TextMeshProUGUI txAmount;

        public ChangeValueAnimation(TextMeshProUGUI txAmount)
        {
            this.txAmount = txAmount;
        }

        public IEnumerator ChangeValueOverTime(int currentValue, int previousValue, float animationDuration)
        {
            float duration = 0;
            var amountChanged = currentValue - previousValue;

            while (duration < animationDuration)
            {
                //value equals it current value plus the amount changed percentage of the animation time
                //ex: value = 4000 -> duration = .5 (half animation), value = 4000 + 4000 * .5
                int value = previousValue + (int)(amountChanged * duration);
                txAmount.text = value.ToString();

                duration += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            txAmount.text = currentValue.ToString();
        }
    }
}