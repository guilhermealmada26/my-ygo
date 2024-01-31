using System.Collections.Generic;
using UnityEngine;

namespace BBG.CustomLayouts
{
    public abstract class CustomLayout : MonoBehaviour
    {
        [SerializeField] bool readjustOnUpdate;

        public void AddChild(Transform child)
        {
            if (child == null)
                return;

            if (child.transform.parent != null && child.parent.TryGetComponent<CustomLayout>(out var layout))
                layout.RemoveChild(child);
            child.SetParent(transform);
            AdjustChildren();
        }

        public bool RemoveChild(Transform child)
        {
            if (child == null)
                return false;

            if (child.IsChildOf(transform))
            {
                child.SetParent(null);
                AdjustChildren(); 
                return true;
            }
            return false;
        }

        protected abstract void AdjustChildren(Transform[] activeChildren);

        public void AdjustChildren()
        {
            var activeChildren = GetActive();
            AdjustChildren(activeChildren);
        }

        protected Transform[] GetActive()
        {
            var active = new List<Transform>();
            foreach (Transform child in transform)
                if (child.gameObject.activeInHierarchy)
                    active.Add(child);
            return active.ToArray();
        }

        [ExecuteInEditMode]
        private void Update()
        {
            if (readjustOnUpdate)
                AdjustChildren();
        }
    }
}