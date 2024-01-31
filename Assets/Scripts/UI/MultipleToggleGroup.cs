using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleToggleGroup : MonoBehaviour
{
    private Toggle[] toggles;

    private Toggle[] Toggles()
    {
        if (toggles == null || toggles.Length == 0)
            toggles = transform.GetComponentsInChildren<Toggle>();
        return toggles;
    }

    public int[] GetActiveIndexes()
    {
        var indexes = new List<int>();
        var toggles = Toggles();
        for (int i = 0; i < toggles.Length; i++)
            if (toggles[i].isOn)
                indexes.Add(i);
        return indexes.ToArray();
    }

    public string[] GetActiveNames()
    {
        var names = new List<string>();
        var toggles = Toggles();
        foreach (var toggle in toggles)
            if (toggle.isOn)
                names.Add(toggle.GetComponentInChildren<TextMeshProUGUI>().text);
        return names.ToArray();
    }

    public void ToggleAll(bool on)
    {
        var toggles = Toggles();
        foreach (var toggle in toggles)
            toggle.isOn = on;
    }
}