using BBG.Dueling.Effects;
using BBG.Dueling.Selection;
using BBG.Ygo;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Actions
{
    public class AncientFusionSummon : ExtraSummon
    {
        readonly bool isOptional;
        public Card[] ObrigatoryMaterials;

        public ExtraMonsterData Data => summoned.data as ExtraMonsterData;
        public MaterialData[] Materials => Data.materials;

        public AncientFusionSummon(Monster summoned, Card caller, List<Card> range, bool isOptional)
            : base(summoned, caller, range, DefaultAction)
        {
            this.isOptional = isOptional;
        }

        public AncientFusionSummon(Monster summoned)
            : this(summoned, summoned, Range(summoned, Location.Graveyard), true)
        {
        }

        static List<Card> Range(Card card, Location location) => new(card.Controller[location]);

        static DuelAction DefaultAction(ExtraSummon summon, Card target)
        {
            return new MaterialDiscard(target, summon.summoned, summon.Caller);
        }

        protected override bool CardsAreValid(List<Card> cards) => CardsAreValid(cards, selection.min);
        protected override bool MaxCardsSelected(List<Card> cards) => CardsAreValid(cards, selection.max);

        private bool CardsAreValid(List<Card> cards, uint min)
        {
            var allValid = new HashSet<Card>();
            foreach (var mat in Materials)
            {
                var valid = cards.Where(c => IsValid(c, mat));
                valid.Foreach(c => allValid.Add(c));
                if (valid.Count() < mat.min)
                    return false;
            }

            if (ObrigatoryMaterials != null)
            {
                foreach (var card in ObrigatoryMaterials)
                    if (!allValid.Contains(card))
                        return false;
            }
            //if the player's monster field is full, at least one valid material in range must be in monster zone
            if (player[Location.MonsterZone].Count >= player.FieldMaxCards)
            {
                var onField = cards.Count(c => c.Location == Location.MonsterZone);
                if (onField == 0)
                    return false;
            }
            return cards.Count >= min;
        }

        protected override CardsSelection GetSelection()
        {
            var data = summoned.data as ExtraMonsterData;
            var min = (uint)Materials.Sum(m => m.min);
            var max = (uint)Materials.Sum(m => m.max);
            return new CardsSelection(player, range, min, max)
            {
                message = data.SelectionMessage,
            };
        }

        protected override bool IsValid(Card card)
        {
            return Materials.Any(m => IsValid(card, m));
        }

        bool IsValid(Card card, MaterialData material)
        {
            foreach (var eff in card.effects)
                if (eff is MaterialSubstitute substitute && substitute.Applies(this, material))
                    return true;
            return material.filter.IsValid(card);
        }

        protected override void BeforePerform()
        {
            selection.isOptional = isOptional;
            base.BeforePerform();
        }
    }
}