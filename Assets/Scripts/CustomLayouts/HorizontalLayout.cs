using UnityEngine;

namespace BBG.CustomLayouts
{
    public class HorizontalLayout : CustomLayout
    {
        [SerializeField] float cardScale = 0.3f;
        [SerializeField] float spacingX = 1.3f;
        [SerializeField] float spacingY = 0f;
        [SerializeField] float spacingZ = 0.05f;
        [SerializeField] int maxItemsBeforeRecalcultate = 6;

        protected override void AdjustChildren(Transform[] activeChildren)
        {
            float offsetX = 0f;

            for (int i = 0; i < activeChildren.Length; i++)
            {
                var child = activeChildren[i];
                child.localScale = Vector3.one * cardScale;
                var maxWidth = maxItemsBeforeRecalcultate * spacingX;
                var space = Mathf.Min(spacingX, maxWidth / activeChildren.Length);
                var posX = space * (i - (activeChildren.Length -1) / 2f);
                var position = new Vector3(posX - offsetX, spacingY * i, spacingZ * i);    
                child.localPosition = position;
                //set layer
                child.gameObject.SetLayer(gameObject.layer);
            }
        }
    }
}