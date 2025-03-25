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
        if (owner.target == null || owner.target.isDead) // target 없거나 죽었으면 Idle 상태로 변화
        {
            owner.state = Unit.State.Idle;
            return;
        }
        else // target 있으면 Attack
        {
            LookTarget(); // target 위치로 바라보게
                        
            GameManager.Instance.Player.Controller.isAttacking = true;
            GameManager.Instance.Player.Controller.OnAttackAnimation(); // 플레이어 Attack 애니메이션           
            if (owner.unitAnimator != null) owner.unitAnimator.SetTrigger("Attack"); // 몬스터 Attack 애니메이션
        }

        // owner 체력 0 됐을 때 Dead 상태로 변화
        // 플레이어 체력 0
        if (owner.isCreep && CharacterManager.Instance.Character.charHealthValue == 0)
        {
            GameManager.Instance.Player.Controller.isAttacking = false;
            owner.isDead = true;
            owner.state = Unit.State.Dead;
        }
        // 몬스터 체력 0
        else if (!owner.isCreep && owner.health == 0)
        {
            //if (owner.unitAnimator != null) owner.unitAnimator.ResetTrigger("Attack");
            owner.isDead = true;
            if (!owner.isCreep) owner.state = Unit.State.Dead;
        }

        // target 체력 0 됐을 때 다시 Idle 상태로 변화
        if (owner.target.health == 0)
        {                   
            owner.state = Unit.State.Idle;
        }
    }

    public void Exit()
    {
        GameManager.Instance.Player.Controller.isAttacking = false;
    }

    void LookTarget()
    {
        Vector3 dir = owner.target.transform.position - owner.transform.position;
        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
    }
}
