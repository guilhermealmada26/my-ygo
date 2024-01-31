using System;
using System.Collections.Generic;

namespace BBG.Pagination
{
    public class PaginationCollection
    {
        private List<object> items;
        private int elementsPerPage;
        private int currentIndex;

        public event Action OnPageChanged;

        public void SetItems(int elementsPerPage, IEnumerable<object> items)
        {
            if (items == null)
                return;
            this.items = new List<object>(items);
            this.elementsPerPage = elementsPerPage;
            SetPage(currentIndex);
        }

        public int CurrentPage => currentIndex + 1;

        public int ElementsPerPage => elementsPerPage;

        public int ItemsCount
        {
            get
            {
                if (items == null)
                    return 0;
                return items.Count;
            }
        }

        public int NumberPages
        {
            get
            {
                if (items == null || items.Count == 0)
                    return 0;
                return (int)Math.Ceiling(items.Count / (double)elementsPerPage);
            }
        }

        public List<T> GetPage<T>()
        {
            int pageCount = (int)Math.Ceiling(items.Count / (double)elementsPerPage);
            currentIndex = Math.Min(currentIndex, pageCount - 1);
            currentIndex = currentIndex < 0 ? 0 : currentIndex;

            int startIndex = elementsPerPage * currentIndex;
            int endIndex = Math.Min(startIndex + elementsPerPage, items.Count);
            var page = new List<T>();
            if (items.Count == 0)
                return page;

            for (int i = startIndex; i < endIndex; i++)
            {
                page.Add((T)items[i]);
            }
            return page;
        }

        public void SetPage(int index)
        {
            int pageCount = (int)Math.Ceiling(items.Count / (double)elementsPerPage);
            currentIndex = Math.Min(index, pageCount - 1);
            OnPageChanged?.Invoke();
        }

        public void Next()
        {
            int pageCount = (int)Math.Ceiling(items.Count / (double)elementsPerPage);
            SetPage(Math.Min(currentIndex + 1, pageCount - 1));
        }

        public void Previous()
        {
            SetPage(Math.Max(currentIndex - 1, 0));
        }

        public void First()
        {
            SetPage(0);
        }

        public void Last()
        {
            int pageCount = (int)Math.Ceiling(items.Count / (double)elementsPerPage);
            SetPage(pageCount - 1);
        }
    }
}