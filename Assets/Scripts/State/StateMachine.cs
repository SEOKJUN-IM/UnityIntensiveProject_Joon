using UnityEngine;

// 각 상태들 이벤트 관리 위한 인터페이스
public interface IState
{
    void Enter(); // 상태 진입, 한번 실행
    void Stay(); // 상태 유지, Update 문에서 매 프레임 실행
    void Exit(); // 상태 탈출, 한번 실행
}

public class StateMachine : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
