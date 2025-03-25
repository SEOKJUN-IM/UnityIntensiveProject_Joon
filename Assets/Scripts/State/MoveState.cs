using UnityEngine;

public class MoveState : IState
{
    private Unit owner;

    public MoveState(Unit unit)
    {
        owner = unit;
    }

    public void Enter()
    {
        if (owner.unitAnimator != null) owner.unitAnimator.SetBool("Walk", true);
    }

    public void Stay()
    {
        // Unit이 Walk 상태로 타겟으로 이동
        // 타겟이 공격 범위 안이면 이동 멈추고 공격, Attack 상태로 변화
        // 이동 중, 공격 중 타겟 죽으면 Idle 상태로 변화, 다시 가장 가까운 적 검색
        if (owner.target == null || owner.target.isDead) // target 없거나 죽었으면 Idle 상태로 변화
        {
            owner.state = Unit.State.Idle;
            return;
        }

        GameManager.Instance.Player.Controller.OnMoveAnimation();

        LookTarget(); // target 위치로 바라보게

        owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.target.transform.position, Time.deltaTime * owner.data.moveSpeed); // owner 점점 다가감

        if (Vector3.Distance(owner.transform.position, owner.target.transform.position) <= owner.data.attackRange) owner.state = Unit.State.Attack;        
    }

    public void Exit()
    {
        GameManager.Instance.Player.Controller.OffMoveAnimation();
        if (owner.unitAnimator != null) owner.unitAnimator.SetBool("Walk", false);
    }

    void LookTarget()
    {
        Vector3 dir = owner.target.transform.position - owner.transform.position;
        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
    }
}
