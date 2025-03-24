using UnityEngine;

public class AttackState : IState
{
    private Unit owner;

    public AttackState(Unit unit)
    {
        owner = unit;
    }

    public void Enter()
    {
        
    }

    public void Stay()
    {        
        GameManager.Instance.Player.Controller.isAttacking = true;
        GameManager.Instance.Player.Controller.OnAttackAnimation();

        Unit target = owner.target;
        owner.transform.LookAt(target.transform);
    }

    public void Exit()
    {
        GameManager.Instance.Player.Controller.isAttacking = false;
    }
}
