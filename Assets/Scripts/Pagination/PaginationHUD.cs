using System.Collections;
using TMPro;
using UnityEngine;

namespace BBG.Pagination
{
    public class PaginationHUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI pagesTxt;
        [SerializeField]
        [Tooltip("Show number of items instead of pages.")]
        private bool showNumberOfItems;

        private IPaginationController controller;

        private IEnumerator Start()
        {
            //wait for pagination object initialization
            yield return new WaitForSeconds(.1f);
            controller = GetComponent<IPaginationController>();
            controller.Pagination.OnPageChanged += OnPageChanged;
            OnPageChanged();
        }

        private void OnPageChanged()
        {
            int currentPage = controller.Pagination.CurrentPage;
            int numberPages = controller.Pagination.NumberPages;
            if(showNumberOfItems)
            {
                currentPage *= controller.Pagination.ElementsPerPage;
                currentPage = Mathf.Clamp(currentPage, 0, controller.Pagination.ItemsCount);
                numberPages *= controller.Pagination.ElementsPerPage;
                numberPages = Mathf.Clamp(numberPages, 0, controller.Pagination.ItemsCount);
            }
            pagesTxt.text = $"{currentPage}/{numberPages}";
        }
    }
}