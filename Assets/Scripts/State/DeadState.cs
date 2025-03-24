using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private Unit owner;

    public DeadState(Unit unit)
    {
        owner = unit;
    }

    public void Enter()
    {

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
