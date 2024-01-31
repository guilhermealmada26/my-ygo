using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace BBG.CustomLayouts
{
    public class GridLayout2D : CustomLayout
    {
        public int rows, columns;
        public Vector2 size, spacing;
        public Type type;

        public enum Type
        {
            LeftToRight, MiddleCenter
        }

        protected override void AdjustChildren(Transform[] activeChildren)
        {
            int childCount = 0, x = 0, y = 0;

            //y increase until it equals rows, when this happens > layout is done
            //x increases until it equals columns, when this happens > y +=1 and x reset to 0
            while (y < rows && childCount < activeChildren.Length)
            {
                var child = activeChildren[childCount];
                childCount++;
                AdjustChild(child, activeChildren.Length, x, y);

                if (x < columns-1)
                {
                    x++;
                }
                else
                {
                    y++;
                    x = 0;
                }
            }
        }

        protected virtual void AdjustChild(Transform child, int activeChildCount, int x, int y)
        {
            if (type == Type.MiddleCenter)
                x -= (activeChildCount -1) / 2;
            child.localPosition = new Vector3(x * spacing.x, -y * spacing.y, 0);
            child.localScale = new Vector3(size.x, size.y, 1);
        }
    }
}