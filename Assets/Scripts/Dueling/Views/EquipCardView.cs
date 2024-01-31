using BBG.Dueling.Actions;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class EquipCardView : DuelComponent
    {
        [SerializeField] AudioClip sfx;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<EquipAction>((a) => Execute(OnEquipped(a)));
        }

        private IEnumerator OnEquipped(DuelAction a)
        {
            var action = a as EquipAction;
            var hasEquips = action.card.equips.Count > 0;
            action.card.GetView().equippedMark.SetActive(hasEquips);
            if(action.equip.Location == Location.STZone)
                PlaySfx(sfx);
            if(hasEquips)
                yield return new WaitForSeconds(.3f);
        }
    }
}
