using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DataEditorBase : EditorWindow
{
    public VisualTreeAsset uxml, item_button;

    TextField searchText, fileName;
    DropdownField typesDD;
    ScrollView itemsScroll;
    VisualElement contentScroll, upperTab, buttonsTab;
    Label filesLabel;
    Toggle typeToggle;
    Button newButton;
    List<Button> itemsButtons = new(50);
    List<Button> activeButtons = new();
    Button selectedItemButton;
    List<Button> tabButtons = new();
    Type[] concreteTypes;
    List<ScriptableObject> filtered;
    DataCategory[] categories;
    int categoryIndex = -1;

    Type SelectedType => concreteTypes[typesDD.index];
    DataCategory Category => categories[categoryIndex];

    protected abstract DataCategory[] GetCategories();

    public class DataCategory
    {
        public Type[] Types { internal set; get; }
        public ScriptableObject[] Objects { private set; get; }

        public readonly string path;
        public string name;
        public ScriptableObject selected;
        public Type selectedType;

        public DataCategory(Type baseType, string path)
        {
            this.path = path;
            name = baseType.Name;
            Types = TypeUtility.GetSubtypes(baseType);
        }

        public void ResetObjects()
        {
            Objects = SOUtility.GetAll<ScriptableObject>(path).OrderBy(o => o.name).ToArray();
        }
    }

    protected virtual void OnEnable()
    {
        categories = GetCategories();
        uxml.CloneTree(rootVisualElement);
        //set fields
        searchText = rootVisualElement.Q<TextField>("SearchInput");
        itemsScroll = rootVisualElement.Q<ScrollView>("FilesScroll");
        contentScroll = rootVisualElement.Q("ContentScroll");
        fileName = rootVisualElement.Q<TextField>("FileNameInput");
        filesLabel = rootVisualElement.Q<Label>("FilesLabel");
        typeToggle = rootVisualElement.Q<Toggle>("TypeToggle");
        typesDD = rootVisualElement.Q<DropdownField>("TypeDD");
        upperTab = rootVisualElement.Q("UpperTab");
        buttonsTab = rootVisualElement.Q("Buttons");
        newButton = rootVisualElement.Q<Button>("NewButton");
        //set categories
        var tabs = rootVisualElement.Q<VisualElement>("Tab");
        tabs.Children().Foreach(t =>
        {
            tabButtons.Add(t as Button);
        });
        for (int i = 0; i < tabButtons.Count; i++)
        {
            var button = tabButtons[i];
            if (i < categories.Length)
            {
                var x = i;
                button.text = categories[i].name + "s";
                button.clicked += () => SetCategory(x);
            }
            button.visible = (i < categories.Length);
        }
        //callbacks
        rootVisualElement.Q<Button>("ClearButton").clicked += ClearSearchText;
        rootVisualElement.Q<Button>("NewButton").clicked += NewItem;
        rootVisualElement.Q<Button>("Rename").clicked += Rename;
        rootVisualElement.Q<Button>("Duplicate").clicked += Duplicate;
        rootVisualElement.Q<Button>("Delete").clicked += Delete;
        typeToggle.RegisterValueChangedCallback((e) => SetItemsButtons());
        typesDD.RegisterValueChangedCallback((e) => TypeDDChanged());
        searchText.RegisterValueChangedCallback((e) => SetItemsButtons());
        //initial setup
        rootVisualElement.RegisterCallback<KeyDownEvent>(OnKeyDown);
        SetCategory(0);
    }

    void SetCategory(int index)
    {
        if (index == categoryIndex)
            return;
        categoryIndex = index;
        SetTypes();
        SetObjects();
        if (Selected == null)
        {
            if (activeButtons.Count > 0)
                SetSelected(0);
        }
        else
        {
            selectedItemButton?.RemoveFromClassList("selected");
            SetSelected(Selected, true);
        }
    }

    void SetTypes()
    {
        concreteTypes = Category.Types;
        var typesNames = new List<string>();
        foreach (var type in concreteTypes)
            typesNames.Add(type.Name);
        typesDD.choices = typesNames;
        var i = Category.Types.IndexOf(Category.selectedType);
        if (typesDD == null)
            return;
        typesDD.index = i >= 0 ? i : 0;
        newButton.SetEnabled(concreteTypes.Length > 0);
    }

    void SetObjects()
    {
        Category.ResetObjects();
        var difference = Category.Objects.Length - itemsButtons.Count;
        for (int i = 0; i < difference; i++)
        {
            var bt = new Button();
            bt.style.display = DisplayStyle.None;
            bt.style.height = 28;
            bt.style.fontSize = 15;
            item_button.CloneTree(bt);
            bt.clicked += () => SetSelected(bt);
            itemsScroll.Add(bt);
            itemsButtons.Add(bt);
        }
        SetItemsButtons();
    }

    void TypeDDChanged()
    {
        Category.selectedType = Category.Types[typesDD.index];
        SetItemsButtons();
    }

    void SetItemsButtons()
    {
        filtered = new List<ScriptableObject>();
        foreach (var o in Category.Objects)
        {
            if (typeToggle.value && o.GetType() != SelectedType)
                continue;
            if (!string.IsNullOrEmpty(searchText.value) && !o.name.ToLower().Contains(searchText.value.ToLower()))
                continue;
            filtered.Add(o);
        }
        activeButtons.Clear();
        for (int i = 0; i < itemsButtons.Count; i++)
        {
            itemsButtons[i].style.display = i < filtered.Count ? DisplayStyle.Flex : DisplayStyle.None;
            if (i < filtered.Count)
            {
                itemsButtons[i].Q<Label>().text = filtered[i].name;
                itemsButtons[i].Q<IMGUIContainer>("Icon").style.backgroundImage = AssetPreview.GetMiniThumbnail(filtered[i]);
                activeButtons.Add(itemsButtons[i]);
            }
        }
        SetSelectedButton();
        filesLabel.text = $"{Category.name}s ({filtered.Count})";
    }

    void ClearSearchText()
    {
        searchText.value = string.Empty;
    }

    void NewItem()
    {
        var newItem = CreateInstance(SelectedType);
        newItem.name = "New" + Category.name;
        var path = $"{Category.path}/{newItem.name}.asset";
        AssetDatabase.CreateAsset(newItem, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        ClearSearchText();
        SetObjects();
        SetSelected(newItem);
    }

    ScriptableObject Selected { set { Category.selected = value; } get { return Category.selected; } }

    void Duplicate()
    {
        if (Selected == null)
            return;

        var duplicate = SOUtility.DuplicateAsset(Selected);
        SetObjects();
        SetSelected(duplicate);
    }

    void Delete()
    {
        if (Selected == null)
            return;

        SOUtility.DeleteAsset(Selected);
        SetObjects();
        SetSelectedNull();
    }

    void Rename()
    {
        if (Selected == null)
            return;

        if (!string.IsNullOrEmpty(fileName.value) && fileName.value != Selected.name)
        {
            SOUtility.Rename(Selected, fileName.value);
            SetObjects();
        }
    }

    void SetSelectedNull()
    {
        Selected = null;
        upperTab.style.display = DisplayStyle.None;
        buttonsTab.style.display = DisplayStyle.None;
        contentScroll.Clear();
        selectedItemButton?.RemoveFromClassList("selected");
        if (activeButtons.Count > 0)
            itemsScroll.ScrollTo(activeButtons[0]);
    }

    void SetSelected(Button button)
    {
        var index = activeButtons.IndexOf(button);
        SetSelected(filtered[index]);
    }

    void SetSelected(ScriptableObject obj, bool allowReset = false)
    {
        if (obj == null || (Selected == obj && !allowReset))
            return;

        Selected = obj;
        SetSelectedButton();
        ResetContent();
    }

    void SetSelectedButton()
    {
        if (Selected == null)
            return;
        selectedItemButton?.RemoveFromClassList("selected");
        int index = filtered.IndexOf(Selected);
        if (index < 0 || index > activeButtons.Count)
            return;
        selectedItemButton = activeButtons[index];
        selectedItemButton.AddToClassList("selected");
        itemsScroll.ScrollTo(selectedItemButton);
    }

    void ResetContent()
    {
        contentScroll.Clear();
        if (Selected == null)
            return;

        var inspector = new InspectorElement(Selected);
        contentScroll.Add(inspector);
        upperTab.style.display = DisplayStyle.Flex;
        buttonsTab.style.display = DisplayStyle.Flex;
        fileName.value = Selected.name;
    }

    void SetSelected(int index)
    {
        if (index >= 0 && index < filtered.Count)
            SetSelected(filtered[index]);
    }

    void OnKeyDown(KeyDownEvent evt)
    {
        //UnityEngine.Debug.Log(Input.GetKeyDown(KeyCode.UpArrow));
        if (evt.keyCode == KeyCode.UpArrow)
            SetSelected(filtered.IndexOf(Selected) - 1);
        if (evt.keyCode == KeyCode.DownArrow)
            SetSelected(filtered.IndexOf(Selected) + 1);
    }

    bool b;

    private void OnGUI()
    {
        if (!b && activeButtons.Count > 0 && activeButtons[0].canGrabFocus)
        {
            b = true;
            activeButtons[0].Focus();
        }
    }
}
