using System.Collections.Generic;
using UnityEngine;

public enum Type { None, Normal, Fire, Water, Grass, Electric, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dragon, Dark, Steel, Fairy };

[CreateAssetMenu(fileName = "TypeData", menuName = "Scriptable Object/TypeData")]
public class TypeData : ScriptableObject
{
    public string Name;
    public Color Color;
    public Type Type;
    public List<Type> Very_Effective_Type;
    public List<Type> Not_Very_Effective_Type;
    public List<Type> Not_Effective_Type;
}