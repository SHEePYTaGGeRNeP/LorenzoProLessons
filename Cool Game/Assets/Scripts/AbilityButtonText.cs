using Unit.MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AbilityButtonText : MonoBehaviour
{
    [SerializeField]
    private CreatureMono _creature;

    [SerializeField]
    private int _index;

    [SerializeField]
    private string _prefix;

    private void Start()
    {
        this.GetComponent<Text>().text = $"{this._prefix}{this._creature.Creature.Abilities[this._index].Name}";
    }
}