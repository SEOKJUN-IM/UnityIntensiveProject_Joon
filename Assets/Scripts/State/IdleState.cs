using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
