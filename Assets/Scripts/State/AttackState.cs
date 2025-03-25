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
        Unit target = owner.target;

        if (target == null || target.isDead) // target 없거나 죽었으면 Idle 상태로 변화
        {
            owner.state = Unit.State.Idle;
            return;
        }

        GameManager.Instance.Player.Controller.isAttacking = true;
        GameManager.Instance.Player.Controller.OnAttackAnimation();                
        
        owner.transform.LookAt(target.transform);
        if (owner.unitAnimator != null) owner.unitAnimator.SetTrigger("Attack");        
    }

    public void Exit()
    {
        GameManager.Instance.Player.Controller.isAttacking = false;
    }    
}
