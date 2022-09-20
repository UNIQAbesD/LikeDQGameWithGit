using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIdentity : ScriptableObject
{
    [SerializeField]protected string _Name;
    public string Name { get{ return _Name; } }

    public virtual SkillSubst skillSubst { get { return new SkillSubst(); } }
    
 
}
