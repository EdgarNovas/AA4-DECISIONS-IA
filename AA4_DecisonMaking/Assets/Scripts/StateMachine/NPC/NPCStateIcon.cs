using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStateIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    [System.Serializable]
    public struct StateIcon
    {
        public string stateTypeName;
        public Sprite icon;
    }

    [SerializeField] private StateIcon[] stateIcons;

    private Dictionary<string, Sprite> iconMap;
    private StateMachine fsm;

    private void Awake()
    {
        fsm = GetComponent<StateMachine>();

        iconMap = new Dictionary<string, Sprite>();
        foreach (var entry in stateIcons)
        {
            iconMap.Add(entry.stateTypeName, entry.icon);
        }
    }

    private void OnEnable()
    {
        fsm.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        fsm.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(Type newStateType)
    {
        if (iconMap.TryGetValue(newStateType.Name, out Sprite sprite))
        {
            iconImage.sprite = sprite;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.enabled = false;
        }
    }
}
