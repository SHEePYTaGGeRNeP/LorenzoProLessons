using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Items;
using System.Linq;

public class CreateItemWindow : EditorWindow
{
    private const string _ITEMS_ASSETS_PATH = "Assets/Data/Items";
    private const string _ITEM_BEHAVIORS_ASSETS_PATH = "Assets/Data/ItemBehaviors";
    private int _selectedIndex;
    static Dictionary<string, ItemBehavior> optionsDic = new Dictionary<string, ItemBehavior>();
    public static void ShowWindow()
    {
        UpdateAbilityOptions();
        EditorWindow window = GetWindow<CreateItemWindow>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 300, 100);
        window.minSize = new Vector2(250, 100);
        window.Show();
    }
    private static void UpdateAbilityOptions()
    {
        optionsDic.Clear();
        string[] behaviors = AssetDatabase.FindAssets("*.asset", new[] { _ITEM_BEHAVIORS_ASSETS_PATH });
        foreach (string behavior in behaviors)
        {
            ItemBehavior ib = AssetDatabase.LoadAssetAtPath<ItemBehavior>(behavior);            
            optionsDic.Add(behavior, ib);
        }
    }
    private void OnGUI()
    {
        string[] optionsArray = optionsDic.Keys.ToArray();
        _selectedIndex = EditorGUILayout.Popup(_selectedIndex, optionsArray);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update options"))
            UpdateAbilityOptions();
        if (GUILayout.Button("Create"))
            CreateAsset(optionsArray[_selectedIndex]);
        GUILayout.EndHorizontal();
    }
    private void CreateAsset(string name)
    {
        ItemBehavior ib = optionsDic[name];
        ItemSO data = CreateInstance<ItemSO>();
        data.SetBehaviors(new[] { ib });
        Debug.Log($"data: {data}");
        string prefix = "_item";
        string fileName = prefix;
        int nr = 0;
        while (AssetDatabase.FindAssets(fileName, new[] { _ITEMS_ASSETS_PATH }).Length > 0)
            fileName = prefix + ++nr;
        AssetDatabase.CreateAsset(data, $"{_ITEMS_ASSETS_PATH }/{fileName}.asset");
    }
}
