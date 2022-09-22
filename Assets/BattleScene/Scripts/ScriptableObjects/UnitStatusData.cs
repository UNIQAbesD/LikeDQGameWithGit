using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/UnitStatus/UnitStatusData", fileName = "UnitStatusData")]
public class UnitStatusData : ScriptableObject
{
    [SerializeField] private UnitStatus _unitStatus;
    public UnitStatus unitStatus { get { return _unitStatus; } }
}
