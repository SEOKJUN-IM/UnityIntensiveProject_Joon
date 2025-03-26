using System;

public class DeadState : IState
{
    private Unit owner;

    public DeadState(Unit unit)
    {
        owner = unit;
    }

    public void Enter()
    {        
        // 실패 창은 플레이어 죽은 후 3초 후에 뜨고, 그 5초 뒤에 다음으로 
        if (UIManager.Instance.stageResultWindow != null)
        {
            UIManager.Instance.stageResultWindow.SlowOnResultWindow();            
        }

        // 플레이어 Dead 애니메이션
        if (owner.isCreep) GameManager.Instance.Player.Controller.OnDeadAnimation();

        // Unit Dead 애니메이션     
        if (!owner.isCreep && owner.unitAnimator != null) owner.unitAnimator.SetBool("Dead", true);

        if (!owner.isCreep) GameManager.Instance.deadCounts++;

        // 플레이어 경험치 얻기 = 몬스터가 Dead에 들어갈 때
        PlayerGetExp();
    }

    public void Stay()
    {
        // 메인메뉴씬으로 가면 다시 Idle 상태
        if (GameManager.Instance.inMainMenuScene) owner.state = Unit.State.Idle;
    }

    public void Exit()
    {
        // 플레이어 Dead 애니메이션 종료
        if (owner.isCreep) GameManager.Instance.Player.Controller.OffDeadAnimation();

        // 플레이어 체력 full로 복귀, isDead false
        if (owner.isCreep && CharacterManager.Instance.Character.charHealthValue == 0)
        {            
            CharacterManager.Instance.Character.charHealthValue = CharacterManager.Instance.Character.charMaxHealthValue;
            owner.isDead = false;
        }            

        // 몬스터 Dead 애니메이션 종료, isDead false
        if (!owner.isCreep && owner.unitAnimator != null)
        {
            owner.unitAnimator.SetBool("Dead", false);
            owner.isDead = false;
        }
    }

    // 플레이어 경험치 얻기 = 몬스터가 Dead에 들어갈 때
    public void PlayerGetExp()
    {
        if (!owner.isCreep)
        {
            CharacterManager.Instance.Character.charCurExp += owner.data.unitExp;

            // 경험치를 얻었는데 원래의 maxExp보다 많으면
            if (CharacterManager.Instance.Character.charCurExp >= CharacterManager.Instance.Character.charMaxExp)
            {
                // 플레이어 레벨업
                PlayerLevelUp();
            }
        }
    }

    public void PlayerLevelUp()
    {
        CharacterManager.Instance.Character.charLevel++;
        CharacterManager.Instance.Character.charCurExp -= CharacterManager.Instance.Character.charMaxExp;
        CharacterManager.Instance.Character.charMaxExp += 20;
        CharacterManager.Instance.Character.charMaxHealthValue += 20;
        CharacterManager.Instance.Character.charMaxManaValue += 20;
        CharacterManager.Instance.Character.charAttackValue += 2;
        CharacterManager.Instance.Character.charDefenseValue += 1;
    }
}
