using System.Collections.Generic;
using UnityEngine;

namespace BBG.Campaign
{
    [System.Serializable]
    public class CampaignData
    {
        [SerializeField] int money;
        [SerializeField] int exp;
        [SerializeField][HideInInspector] List<string> strings = new();

        public int Money => money;
        public int Exp => exp;

        public void AddMoney(int value)
        {
            money = Mathf.Clamp(money + value, 0, int.MaxValue);
        }

        public void AddExp(int value)
        {
            exp = Mathf.Clamp(exp + value, 0, int.MaxValue);
        }

        public void AddString(params string[] values) => strings.AddNonContained(values);
        public bool RemoveString(string value) => strings.Remove(value);
        public bool HasString(string value) => strings.Contains(value);
    }
}