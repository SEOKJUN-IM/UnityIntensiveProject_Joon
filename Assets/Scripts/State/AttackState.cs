using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Attack 상태 진입");
    }

    public void Stay()
    {

    }

    public void Exit()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }
}
