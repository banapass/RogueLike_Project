using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveTarget
{

    public float CurrentHp { get; set; }
    public float MaxHp { get; set; }
    public float Atk { get; set; }
    public float Damage { get; set; }
    public float IncreaceAtk { get; set; }
    public float Def { get; set; }
    public float AtkSpeed { get; set; }


}
