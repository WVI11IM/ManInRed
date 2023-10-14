using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Item Data")]
public class ItemData : ScriptableObject
{
    public int id;
    public string displayName;
    public Sprite sprite;
}
