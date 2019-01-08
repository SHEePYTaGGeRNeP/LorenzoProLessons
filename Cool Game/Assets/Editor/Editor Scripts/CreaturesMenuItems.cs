using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editor.Editor_Scripts
{
    class CreaturesMenuItems
    {
        private const string _CREATURES_ASSETS_PATH = "Assets/Data/Creatures";

        [MenuItem("Creatures/CreateAbilityAsset")]
        private static void CreateAbilityAsset() => AbilityPopupWindow.ShowWindow();

        [MenuItem("Creatures/CreateCreatureAsset", priority = 2)]
        private static void CreateCreatureAsset()
        {
            CreatureSO data = ScriptableObject.CreateInstance<CreatureSO>();
            const string prefix = "_Creature";
            string name = prefix;
            int nr = 0;
            while (AssetDatabase.FindAssets(name, new[] { _CREATURES_ASSETS_PATH }).Length > 0)
                name = prefix + ++nr;
            AssetDatabase.CreateAsset(data, $"{_CREATURES_ASSETS_PATH }/{name}.asset");
        }

    }
}
