using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BFAL 
{
    private BattleField battleField;

}

public class BattleUnitAL
{
    public int ATK;

}
public class SkillDataForUI
{
    public string Name;
    public int skillIndex;
    public SkillElement skillElement;
    public SkillCategory skillCategory;
    public SkillOUnitType skillOUnitType;
    public int power;
    public float accuracy;

    



    public enum SkillElement
    {
        None,
        Fire,
        Ice,
        Wind,
        Exception
    }
    public enum SkillCategory
    {
        None,
        Phisical,
        Magical,
        Status,
        Exception
    }
    public enum SkillOUnitType
    {
        User,
        OneAlly,
        OneFoe,
        OneUnit,
        AllAlly,
        AllFoe,
        AllUnit,
        RandomAlly,
        RandomFoe,
        RandomUnit,
        Exception
    }
}
