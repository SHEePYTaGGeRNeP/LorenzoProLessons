using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Abilities;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Editor_Scripts
{
    class AbilityPopupWindow : EditorWindow
    {
        private const string _ABILITIES_ASSETS_PATH = "Assets/Data/Abilities";
        private int _selectedIndex;
        static Dictionary<string, Type> optionsDic = new Dictionary<string, Type>();

        public static void ShowWindow()
        {
            UpdateAbilityOptions();
            EditorWindow window = GetWindow<AbilityPopupWindow>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 300, 100);
            window.minSize = new Vector2(250, 100);
            window.Show();
        }

        private static void UpdateAbilityOptions()
        {
            optionsDic.Clear();
            foreach (Type type in Utils.GetSubclassesOfAbility())
                optionsDic.Add(type.Name, type);
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
            Type type = optionsDic[name];
            string className = type.Name;
            Ability data = (Ability)CreateInstance(className);
            Debug.Log($"data: {data}");
            string prefix = "_" + className;
            string fileName = prefix;
            int nr = 0;
            while (AssetDatabase.FindAssets(fileName, new[] { _ABILITIES_ASSETS_PATH }).Length > 0)
                fileName = prefix + ++nr;
            AssetDatabase.CreateAsset(data, $"{_ABILITIES_ASSETS_PATH }/{fileName}.asset");
        }
    }
}
