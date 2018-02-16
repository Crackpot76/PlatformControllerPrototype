using System.Collections;
using System.Collections.Generic;

public class AttackDetails {
    

    public enum AttackType { Blunt, Sharp };
    public enum SideEffects {None, Stun, Bleed, Toxic, Fire, Ice};

    public bool criticalHit = false;
    public float damage = 1f;
    public bool pushOnDamage = true;
    public float pushSpeed = 5f;
    public bool blockable = true;
    
    public AttackType type = AttackType.Blunt;
    
    public SideEffects effect = SideEffects.None;
    public float damageSideEffect;
    public float effectTime;

}
