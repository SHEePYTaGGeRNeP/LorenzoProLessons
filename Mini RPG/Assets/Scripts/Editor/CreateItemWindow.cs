using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Items;
using System.Linq;
using System;

public class CreateItemWindow : EditorWindow
{
    private const string _ITEMS_ASSETS_PATH = "Assets/Data/Items";
    private const string _ITEM_BEHAVIORS_ASSETS_PATH = "Assets/Data/ItemBehaviors";
    private int _behaviorSelectedIndex;
    private GearSlot _gearSlot = GearSlot.Head;
    private string _itemName;
    private List<ItemBehavior> _selectedItemBehaviors = new List<ItemBehavior>();
    static Dictionary<string, ItemBehavior> optionsDic = new Dictionary<string, ItemBehavior>();

    [MenuItem("Window/Items/Open Create Item Window")]
    public static void ShowWindow()
    {
        UpdateAbilityOptions();
        EditorWindow window = GetWindow<CreateItemWindow>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 300);
        window.minSize = new Vector2(250, 100);
        window.Show();
    }
    private static void UpdateAbilityOptions()
    {
        optionsDic.Clear();
        string[] behaviors = AssetDatabase.FindAssets("", new[] { _ITEM_BEHAVIORS_ASSETS_PATH });
        foreach (string guid in behaviors)
        {
            ItemBehavior ib = AssetDatabase.LoadAssetAtPath<ItemBehavior>(AssetDatabase.GUIDToAssetPath(guid));
            string assetName = System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(guid));
            optionsDic.Add(assetName, ib);
        }
    }
    private void OnGUI()
    {
        this._itemName = EditorGUILayout.TextField("Item name", this._itemName);
        this._gearSlot = (GearSlot)EditorGUILayout.EnumPopup("Slot", this._gearSlot);
        string[] optionsArray = optionsDic.Keys.ToArray();
        this._behaviorSelectedIndex = EditorGUILayout.Popup("Behaviors", this._behaviorSelectedIndex, optionsArray);
        if (GUILayout.Button("Update options"))
            UpdateAbilityOptions();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behavior"))
            this.AddBehaviorIfNotExists(optionsArray[this._behaviorSelectedIndex]);
        if (GUILayout.Button("Remove behavior"))
            this._selectedItemBehaviors.RemoveAll(x => x == optionsDic[optionsArray[this._behaviorSelectedIndex]]);
        if (GUILayout.Button("Clear behaviors"))
            this._selectedItemBehaviors.RemoveAll(x => x);
        GUILayout.EndHorizontal();
        if (this._selectedItemBehaviors.Count == 0)
            return;
        GUILayout.Space(20);
        foreach (ItemBehavior ib in this._selectedItemBehaviors)
        {
            GUILayout.Label(ib.name);
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Create item asset"))
            CreateAsset(optionsArray[this._behaviorSelectedIndex]);
    }
    private void AddBehaviorIfNotExists(string name)
    {
        ItemBehavior ib = optionsDic[name];
        if (!this._selectedItemBehaviors.Contains(ib))
            this._selectedItemBehaviors.Add(ib);
    }

    private void CreateAsset(string name)
    {
        ItemSO data = CreateInstance<ItemSO>();
        data.SetBehaviors(this._selectedItemBehaviors.ToArray());
        data.Name = this._itemName;
        data.Slot = this._gearSlot;
        Debug.Log($"data: {data}");
        string prefix = this._itemName;
        string fileName = prefix;
        int nr = 0;
        while (AssetDatabase.FindAssets(fileName, new[] { _ITEMS_ASSETS_PATH }).Length > 0)
            fileName = prefix + ++nr;
        AssetDatabase.CreateAsset(data, $"{_ITEMS_ASSETS_PATH }/{fileName}.asset");
        this.ResetVariables();
    }
    private void ResetVariables()
    {
        this._selectedItemBehaviors.Clear();
        this._itemName = default;
        this._gearSlot = GearSlot.Head;
    }
}
