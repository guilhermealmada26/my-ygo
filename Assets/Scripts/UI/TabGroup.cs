using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tabs;
    [SerializeField]
    private Button[] tabButtons;
    
    private int activeIndex;

    private void Start()
    {
        SetView(0, true);
    }

    public void SetActive(Button tab)
    {
        var index = 0;

        for (int i = 0; i < tabButtons.Length; i++)
        {
            if (tabButtons[i] == tab)
                index = i;
        }

        SetView(activeIndex, false);
        SetView(index, true);
        activeIndex = index;
    }

    private void SetView(int tabIndex, bool selected)
    {
        tabButtons[tabIndex].transform.GetChild(0).gameObject.SetActive(selected);
        tabs[tabIndex].SetActive(selected);
    }
}
