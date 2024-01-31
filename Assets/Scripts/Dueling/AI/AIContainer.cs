using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using UnityEngine;

namespace BBG.Dueling.AI
{
    [CreateAssetMenu(menuName = "Duel/AI/Container")]
    public class AIContainer : ScriptableObject
    {
        public AIDeclareAspect[] declareAspects;

        internal void DeclareAction(Player player)
        {
            GetAction(player).Declare();
        }

        private DuelAction GetAction(Player player)
        {
            foreach (var aspect in declareAspects)
            {
                var action = aspect.GetAction(player);
                if(action != null)
                    return action;
            }

            return new ChangePhaseAction(DuelPhase.End);
        }

        internal void HandleSelection(SelectionAction selection)
        {
            //run all aspects, if selection requirements is done, confirm selection and return
            if(selection is CardsSelection cardsSelection)
            {
                while(!cardsSelection.RequirementsAreDone())
                {
                    var card = cardsSelection.range.GetRandom();
                    cardsSelection.range.Remove(card);
                    cardsSelection.Selected.Add(card);
                }
            }
            else if(selection is YesNoSelection yesNoSelection)
            {
                yesNoSelection.choice = true;
            }
            else if(selection is PositionSelection positionSelection)
            {
                positionSelection.position = CardPosition.AtkFaceUp;
            }
            else if(selection is OptionSelection optionSelection)
            {
                optionSelection.choice = Random.Range(0, optionSelection.Options.Length);
            }
        }
    }
}