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
    public Animator unitAnimator;
    public Unit target;
    public float findTargetRange;
    public bool isCreep = false; // Player는 true로, Monster는 false로 해줄것임(서로 달라야 IdleState에서 FindTarget할 수 있음)
    public bool isDead = false;
    public static bool onPlayerDamaging; // 너무 자주 damage를 받는 것 막는 static 변수
    public static bool onMonsterDamaging; // 너무 자주 damage를 받는 것 막는 static 변수

    public int health;
    public int exp;   

    private void Awake()
    {
        onPlayerDamaging = false;
        onMonsterDamaging = false;

        IStates = new IState[System.Enum.GetValues(typeof(State)).Length];
        IStates[(int)State.Idle] = new IdleState(this);
        IStates[(int)State.Move] = new MoveState(this);
        IStates[(int)State.Attack] = new AttackState(this);
        IStates[(int)State.Dead] = new DeadState(this);

        if (!isCreep) unitAnimator = GetComponentInChildren<Animator>();        
        
        health = data.UnitHp;
        exp = data.unitExp;        
    }   

    private void Update()
    {
        // Stay Update에서 매 프레임 호출
        IStates[(int)_state].Stay();

        // Idle 상태에선 시간 갈수록 검색 범위 증가
        if (GameManager.Instance.inGameScene && this._state == State.Idle)
        {
            findTargetRange += Time.deltaTime * 2f;
            findTargetRange = Mathf.Clamp(findTargetRange, 30f, 500f);
        }

        if (GameManager.Instance.inMainMenuScene)
        {
            onPlayerDamaging = false;
            onMonsterDamaging = false;
        }        
    }

    // Unit이 플레이어일 때 (owner : 플레이어, target : 몬스터)
    public void PlayerHit()
    {
        // Unit의 target(monster)에 데미지를 준다
        if (isCreep && target != null && target._state == State.Attack) MonsterDamaged(target);
    }

    // Unit이 몬스터일 때 (owner : 몬스터, target : 플레이어)
    public void MonsterHit()
    {
        // Unit의 target(player)에 데미지를 준다
        if (!isCreep && target != null && target._state == State.Attack) PlayerDamaged(this);
    }

    // 플레이어가 몬스터한테 데미지를 입을 때
    public void PlayerDamaged(Unit monster)
    {
        if (onPlayerDamaging) return;

        // 플레이어 : 플레이어 캐릭터 체력에서 타겟 몬스터 UnitData의 공격력만큼 빼고, 플레이어 방어력의 0.1만큼 덜 받기
        int damage = Mathf.Max(monster.data.unitAttackPower - (int)(CharacterManager.Instance.Character.charDefenseValue * 0.1f), 0);

        if (damage == 0) return; // 방어력 강해서 damage 0이면 return

        CharacterManager.Instance.Character.charHealthValue = Mathf.Max(CharacterManager.Instance.Character.charHealthValue - damage, 0);

        // GetHit 애니메이션
        GameManager.Instance.Player.Controller.OnGetHitAnimation();

        onPlayerDamaging = true;
        Invoke("OffPlayerDamaging", 3f);
    }

    // 몬스터가 플레이어한테 데미지를 입을 때
    public void MonsterDamaged(Unit monster)
    {
        if (onMonsterDamaging) return;

        // 몬스터 : 몬스터 UnitData 체력에서 플레이어 캐릭터 공격력만큼 빼기
        monster.health = Mathf.Max(monster.health - CharacterManager.Instance.Character.charAttackValue, 0);

        // GetHit 애니메이션
        if (monster.unitAnimator != null) monster.unitAnimator.SetTrigger("GetHit");// 몬스터 : 몬스터 UnitData 체력에서 플레이어 

        // GetHit 애니메이션
        if (monster.unitAnimator != null) monster.unitAnimator.SetTrigger("GetHit");

        onMonsterDamaging = true;
        Invoke("OffMonsterDamaging", 1f);
    }

    void OffPlayerDamaging()
    {
        onPlayerDamaging = false;
    }

    void OffMonsterDamaging()
    {
        onMonsterDamaging = false;
    }
}
