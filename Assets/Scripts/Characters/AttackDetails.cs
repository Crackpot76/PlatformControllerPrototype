using System.Collections;
using System.Collections.Generic;

public class AttackDetails {
    

    public enum AttackType { Blunt, Sharp };
    public enum SideEffects {None, Stun, Bleed, Toxic, Fire, Ice};

    public float criticalHitPercent = 0f;
    public float damage = 1f;
    public bool pushOnDamage = true;
    public float pushSpeed = 8f;
    public bool blockable = true;
    
    public AttackType type = AttackType.Blunt;
    
    public SideEffects effect = SideEffects.None;
    public float damageSideEffect;
    public float effectTime;

}
