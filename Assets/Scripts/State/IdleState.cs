using UnityEngine;

// 기본 Idle 상태
// 가까운 적 찾기 : 적 찾으면 이동, Move 상태로 변화, 못 찾으면 Idle 상태 유지, 검색 범위 넓히면서 계속 검색
public class IdleState : IState
{
    private Unit owner;

    public IdleState(Unit unit)
    {
        owner = unit;
    }

    public void Enter()
    {
        
    }    

    public void Stay()
    {
        if (GameManager.Instance.inGameScene)
        {
            if (owner.target == null || owner.target.isDead)
            {
                Vector3 dir = Vector3.forward;
                owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);

                //owner.transform.position = Vector3.MoveTowards(owner.transform.position, Vector3.forward + Vector3.right, Time.deltaTime * owner.data.moveSpeed); // owner 점점 앞으로 걸어감
                GameManager.Instance.Player.Controller.OnMoveAnimation();
                FindTarget();
            }
        }
    }

    public void Exit()
    {
        
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, owner.findTargetRange, 1 << LayerMask.NameToLayer("Unit"));
        
        if (colliders.Length <= 0) return; // 범위 안에 target이 없다

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
            owner.state = Unit.State.Move; // 이동 상태로 변화
        }
    }
}
