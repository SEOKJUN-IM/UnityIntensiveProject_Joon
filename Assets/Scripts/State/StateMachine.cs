using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Stay();
    void Exit();
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
