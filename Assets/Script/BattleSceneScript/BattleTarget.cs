using UnityEngine;

public class BattleTarget
{
    public static Unit getTarget(Unit caster, bool IsSelf = false)
    {
        if (IsSelf) return caster;
        else
        {
            if (caster.Team == "Player") return BattleManager.Instance.EnemyUnits[0];
            if (caster.Team == "Enemy") return BattleManager.Instance.PlayerUnits[0];
            return null;
        }
    }
}
