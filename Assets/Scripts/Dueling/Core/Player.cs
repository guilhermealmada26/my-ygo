using BBG.Dueling.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling
{
    public class Player : MonoBehaviour
    {
        [SerializeField] int id;

        readonly List<Card>[] lists = new List<Card>[8]
        {
            new(30), new(12), new(10), new(20), new(10), new(5), new(5), new(1),
        };

        public PlayerData Data { set; get; }
        public int LP { internal set; get; } = 8000;
        public int FieldMaxCards { set; get; } = 5;
        public int MaxSummonsPerTurn { set; get; } = 3;

        public int ID => id;
        public ControlMode Control => Data.control;
        public Player Opponent => Duel.Current.GetOpponent(this);
        public bool IsTurnPlayer => Duel.Current.CurrentPlayer == this;

        public void SetId(bool isDuelHost)
        {
            id = isDuelHost ? 0 : 1;
        }

        public List<Card> this[Location location] => lists[(int)location];

        public bool IsFull(Location location, int amountAdded = 0)
        {
            if (location == Location.MonsterZone || location == Location.STZone)
                return this[location].Count + amountAdded > FieldMaxCards;
            return false;
        }
    }
}