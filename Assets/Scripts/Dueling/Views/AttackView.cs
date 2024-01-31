using BBG.Animations;
using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using System.Collections;
using UnityEngine;

namespace BBG.Dueling.View
{
    public class AttackView : DuelComponent
    {
        [SerializeField] LineAnimation lineAnimation;
        [SerializeField] AudioClip sfxAttack;

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            Events.Observe<AttackAction>((a) => Execute(OnAttack(a as AttackAction)));
            Events.Observe<AttackSelection>((a) => Execute(OnAttackSelection(a as AttackSelection)));
            Events.Observe<BattleAction>((a) => Execute(OnBattle(a as BattleAction)));
        }

        private IEnumerator OnAttack(AttackAction action)
        {
            if (!action.IsDirect)
                yield break;
            var attacker = action.Attacker.GetView().transform;
            var opponent = action.Attacker.Controller.Opponent.transform;
            PlaySfx(sfxAttack);
            yield return lineAnimation.Play(attacker, opponent.position);
            new DelegateAction(lineAnimation.Disable).Perform(Priority.AfterChain);
        }

        private IEnumerator OnAttackSelection(AttackSelection action)
        {
            var attacker = action.Attacker.GetView().transform;
            var attacked = action.Attacked.GetView().transform;
            PlaySfx(sfxAttack);
            yield return lineAnimation.Play(attacker, attacked.position);
            new DelegateAction(lineAnimation.Disable).Perform(Priority.AfterChain);
        }

        private IEnumerator OnBattle(BattleAction action)
        {
            lineAnimation.Disable();
            var attackedTransform = action.IsDirectAttack ?
                action.DamagedPlayer.transform : action.attacked.GetView().transform;
            yield return AtkParticle(action.attacker, attackedTransform);
            //attacked animation if attacker is in attack and its .atk is >= attacker.atk
            if (!action.IsDirectAttack && action.attacked.InAtkPosition())
            {
                if (action.attacker.Atk > action.attacked.Atk)
                    yield break;
                var attackerTransform = action.attacker.GetView().transform;
                yield return AtkParticle(action.attacked, attackerTransform);
            }
        }

        private IEnumerator AtkParticle(Monster monster, Transform point)
        {
            var scaleMultiplier = 1f;
            if (monster.PointsByPosition >= 4000)
                scaleMultiplier = 2f;
            else if (monster.PointsByPosition >= 3000)
                scaleMultiplier = 1.75f;
            else if (monster.PointsByPosition >= 2000)
                scaleMultiplier = 1.5f;
            else if (monster.PointsByPosition >= 1000)
                scaleMultiplier = 1.25f;

            yield return monster.AtkAnimation.Play(point.position, null, scaleMultiplier);
        }
    }
}