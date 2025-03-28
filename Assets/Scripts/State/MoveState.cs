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
        // 이동 중 타겟 없거나 죽으면 Idle 상태로 변화, 다시 가장 가까운 적 검색
        if (owner.target == null || owner.target.isDead)
        {
            owner.state = Unit.State.Idle;
            return;
        }

        // Unit이 Walk 상태로 타겟으로 이동
        GameManager.Instance.Player.Controller.OnMoveAnimation();

        LookTarget(); // target 위치로 바라보게

        owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.target.transform.position, Time.deltaTime * owner.data.moveSpeed); // owner 점점 다가감

        // 플레이어는 타겟이 있어도 더 가까운 유닛 있으면 타겟 변경
        if (owner.isCreep && owner.target != null) FindNearestTarget();

        // 타겟이 공격 범위 안이면 이동 멈추고 공격, Attack 상태로 변화
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

    public void FindNearestTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, owner.findTargetRange, 1 << LayerMask.NameToLayer("Unit"));

        // 범위 안에 target이 없다
        if (colliders.Length <= 0) return;

        Unit nearTarget = null; // 가까운 적 저장하기 위한 변수
        float minDistance = Mathf.Infinity; // 가까운 적과 거리 저장하기 위한 변수

        for (int i = 0; i < colliders.Length; i++)
        {
            Unit temp = colliders[i].GetComponent<Unit>();

            if (!temp.isDead && owner.isCreep != temp.isCreep) // 죽지 않은 상대편 유닛이라면(isCreep 서로 다르게 설정)
            {
                float distance = Vector3.Distance(owner.transform.position, temp.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearTarget = temp;
                }
            }
        }

        if (nearTarget != null) // 가까운 적 있다면
        {
            owner.target = nearTarget; // 가장 가까운 적으로 타겟 설정           
        }
    }
}
