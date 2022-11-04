using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;


public class EnableByOtherMemValAttribute : PropertyAttribute
{

    public string SwitcherMemName { get; private set; }
    public int ValueWhenEnable { get; private set; }
    public EnableByOtherMemValAttribute(string memberName,int valueWhenEnable)
    {
        ValueWhenEnable = valueWhenEnable;
        SwitcherMemName = memberName;
    }

}




