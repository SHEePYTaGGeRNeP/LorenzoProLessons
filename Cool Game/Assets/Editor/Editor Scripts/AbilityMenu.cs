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
    class CreateAbilityPopup : EditorWindow
    {
        private int _selectedIndex;
        static Dictionary<string, Type> optionsDic = new Dictionary<string, Type>();

        [MenuItem("Abilities/CreateAsset")]
        static void CreateAbility()
        {
            UpdateAbilityOptions();
            EditorWindow window = GetWindowWithRect<CreateAbilityPopup>(
                new Rect(Screen.width / 2, Screen.height / 2, 250, 100));
            window.Show();
        }

        private static void UpdateAbilityOptions()
        {
            optionsDic.Clear();
            foreach (Type type in Utils.GetSubclassesOfAbility())
            {
                optionsDic.Add(type.Name, type);
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
            Type type = optionsDic[name];
            string className = type.Name;
            Ability data = (Ability)CreateInstance(className);
            Debug.Log($"data: {data}");
            AssetDatabase.CreateAsset(data, $"Assets/Data/Abilities/_{name}.asset");
        }
    }
}
