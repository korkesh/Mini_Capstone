﻿using UnityEngine;
using System.Collections;
using System;

// provides +1atk/speed for every unit of distance between combatants
public class CloseRangeExpertUnitSpecial : UnitSpecial
{
    public CloseRangeExpertUnitSpecial(Unit u) : base(u)
    {
        condition = new CombatCondition(u);
    }

    //the effect this special grants
    public override void effect()
    {
        // increases accuracy by 10% and final damage by 1
        if (CombatSequence.Instance.attacker == unit)
        {
            CombatSequence.Instance.attacker.buffs.Add(new CloseRangeExpertBuff(CombatSequence.Instance.attacker));           
        }
        else if (CombatSequence.Instance.defender == unit)
        {
            CombatSequence.Instance.defender.buffs.Add(new CloseRangeExpertBuff(CombatSequence.Instance.defender));
        }
    }
}
