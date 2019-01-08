using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor.Editor_Scripts
{
    class OtherMenuItems
    {
        private const string _TEST_KEY = "test";

        static OtherMenuItems()
        {
            UnityEngine.Debug.Log("OtherMenuItems constructor");
        }

        // Add a menu item called "Random COlor" to a Image's context menu.
        [MenuItem("CONTEXT/Image/Random Color")]
        static void RandomColor(MenuCommand command)
        {
            Image image = (Image)command.context;
            image.color = Utils.GetRandomColor();
        }
        // Add a menu item called "White Color" to a Image's context menu.
        [MenuItem("CONTEXT/Image/White Color")]
        static void WhiteColor(MenuCommand command)
        {
            Image image = (Image)command.context;
            image.color = new Color(1, 1, 1, 1);
        }

        [MenuItem("Scene/SaveTest")]
        private static void ReloadWithSave()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
        }

        // Validated menu item.
        // We use a second function to validate the menu item
        // so it will only be enabled if we have a transform selected.
        [MenuItem("MyMenu/Log Selected Transform Name")]
        static void LogSelectedTransformName()
        {
            UnityEngine.Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
        }

        // Validate the menu item defined by the function above.
        // The menu item will be disabled if this function returns false.
        [MenuItem("MyMenu/Log Selected Transform Name", true)]
        static bool ValidateLogSelectedTransformName()
        {
            // Return false if no transform is selected.
            return Selection.activeTransform != null;
        }

        private const string _CHECKBOXED = "MyMenu/Checkboxed";
        [MenuItem(_CHECKBOXED)]
        static void CheckboxMenuItem()
        {
            bool current = EditorPrefs.GetBool(_TEST_KEY);
            Menu.SetChecked(_CHECKBOXED, !current);
            EditorPrefs.SetBool(_TEST_KEY, !current);
        }

        [MenuItem("Tools/Open Unity Roadmap")]
        private static void OpenWebsite()
        {
            Process.Start("https://unity3d.com/unity/roadmap");
        }

    }
}
