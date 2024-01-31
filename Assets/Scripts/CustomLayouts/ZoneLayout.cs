using TMPro;
using UnityEngine;

namespace BBG.CustomLayouts
{
    public class ZoneLayout : HorizontalLayout
    {
        [SerializeField] TextMeshProUGUI txChildCount;
        [SerializeField] SpriteRenderer zoneRenderer;
        [SerializeField] Sprite normal, highlighted;

        public bool HasChildren => GetActive().Length > 0;

        protected override void AdjustChildren(Transform[] activeChildren)
        {
            base.AdjustChildren(activeChildren);
            if(txChildCount != null)
            {
                txChildCount.text = activeChildren.Length.ToString();
                if(activeChildren.Length > 0)
                {
                    var position = activeChildren[^1].position;
                    txChildCount.transform.position = position;
                }
            }
        }

        private void OnMouseEnter()
        {
            zoneRenderer.sprite = highlighted;
        }

        private void OnMouseExit()
        {
            zoneRenderer.sprite = normal;
        }

        private void OnMouseDown()
        {
            //print("Clicked: " + name);
        }
    }
}