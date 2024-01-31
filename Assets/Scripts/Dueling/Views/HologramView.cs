using UnityEngine;

namespace BBG.Dueling.View
{
    public class HologramView : MonoBehaviour
    {
        [SerializeField]
        GameObject hologram;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = hologram.transform.GetChild(0).GetComponent<SpriteRenderer>();
            hologram.SetActive(false);
        }

        public void SetActive(Card card, bool active)
        {
            var monster = card as Monster;
            if (active)
                _renderer.sprite = monster.Hologram;
            else if (_renderer.sprite != monster.Hologram)
                return;

            hologram.SetActive(active);
        }
    }
}
