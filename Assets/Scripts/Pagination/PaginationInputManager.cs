using UnityEngine;
using UnityEngine.EventSystems;

namespace BBG.Pagination
{
    public class PaginationInputManager : MonoBehaviour
    {
        [SerializeField] KeyCode nextPage;
        [SerializeField] KeyCode previousPage;
        [SerializeField] bool changePageOnScroll;

        private IPaginationController controller;

        private void Awake()
        {
            controller = GetComponent<IPaginationController>();
        }

        private void Update()
        {
            HandleKeyDown();
            HandleScrollwheel();
        }

        private void HandleKeyDown()
        {
            if (Input.GetKeyDown(nextPage))
                Next();
            else if (Input.GetKeyDown(previousPage))
                Previous();
        }

        private void HandleScrollwheel()
        {
            if (!changePageOnScroll)
                return;
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
                Next();
            else if (scroll < 0f)
                Previous();
        }

        public void Next() => controller.Pagination.Next();
        public void Previous() => controller.Pagination.Previous();
        public void First() => controller.Pagination.First();
        public void Last() => controller.Pagination.Last();
    }
}