using UnityEngine;

public class Unit : MonoBehaviour
{
    // Unit이 가질 수 있는 상태를 enum 만들어 State에 Idle, Move, Attack, Dead 지정
    public enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    private IState[] IStates;

    // 현재 상태 탈출 이벤트 실행 >> 상태 변화 >> 새로운 상태 진입 이벤트
    private State _state;
    public State state
    {
        get => _state;
        set
        {
            IStates[(int)_state].Exit();
            _state = value;
            IStates[(int)_state].Enter();
        }
    }
    
    public UnitData data;
    public Unit target;
    public float findTargetRange;
    public bool isCreep = false;
    public bool isDead = false;   

    private void Awake()
    {
        IStates = new IState[System.Enum.GetValues(typeof(State)).Length];
        IStates[(int)State.Idle] = new IdleState(this);
        IStates[(int)State.Move] = new MoveState(this);
        IStates[(int)State.Attack] = new AttackState(this);
        IStates[(int)State.Dead] = new DeadState(this);
    }

    private void Update()
    {
        // Stay Update에서 매 프레임 호출
        IStates[(int)_state].Stay();

        // Idle 상태에선 시간 갈수록 검색 범위 증가
        if (GameManager.Instance.inGameScene && this._state == State.Idle)
        {
            findTargetRange += Time.deltaTime;
            findTargetRange = Mathf.Clamp(findTargetRange, 30f, 500f);
        }        
    }
}
