using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Helpers.Components
{
    [RequireComponent(typeof(Text))]
    public class VersionTextScript : MonoBehaviour
    {
        private void Awake()
        {
            this.UpdateVersionAndQuality();
        }

        public void UpdateVersionAndQuality()
        {
            this.GetComponent<Text>().text = "Version: " + Application.version + "Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
        }
    }
}
