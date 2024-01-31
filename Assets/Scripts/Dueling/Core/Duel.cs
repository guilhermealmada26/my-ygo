using BBG.Dueling.Actions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling
{
    public class Duel : MonoBehaviour
    {
        [SerializeField] Player player, opponent;

        public System.Random Random { private set; get; }
        public int LastSeed { private set; get; }
        public bool IsPlaying { get; internal set; }
        public DuelPhase Phase { set; get; } = DuelPhase.Main;
        public int TurnCount { set; get; } = 1;
        public Player CurrentPlayer { set; get; }
        public string Tag { set; get; }

        internal Dictionary<string, Card> Cards { private set; get; } = new();
        internal List<DuelAction> StartingActions { private set; get; } = new();
        internal Action<DuelAction> PerformHandler { set; get; }
        public static Duel Current { private set; get; }

        public bool IsNetworked => opponent.Control == ControlMode.Remote;

        private void Awake()
        {
            Current = this;
            PerformHandler = (a) => a.ProcessPerform();
            SetSeed(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
        }

        internal void StartDuel()
        {
            IsPlaying = true;
            foreach (var action in StartingActions)
                action.Perform(Priority.AboveAll);
        }

        internal void SetSeed(int seed)
        {
            LastSeed = seed;
            Random = new System.Random(seed);
        }

        public Player LocalPlayer => player;

        public Player LocalOpponent => opponent;

        public Player GetPlayer(int id)
        {
            if (id < 0 || id > 1)
                throw new Exception("Invalid player id: " + id);
            return (player.ID == id) ? player : opponent;
        }

        public Player GetOpponent(Player p) => (p.ID == player.ID) ? opponent : player;
    }
}