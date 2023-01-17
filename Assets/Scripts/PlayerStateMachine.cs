using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStateMode
{
    LOCOMOTION,
    ROLL,
    SPRINT
    
}
public class PlayerStateMachine : MonoBehaviour
{
    #region Exposed

    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        TransitionToState(PlayerStateMode.LOCOMOTION);
    }

    void Update()
    {
        OnStateUpdate();
    }


    #endregion

    #region Methods

    void OnStateEnter()
    {
        switch (_currentState)
        {
            case PlayerStateMode.LOCOMOTION:
                break;
            case PlayerStateMode.ROLL:
                break;
            case PlayerStateMode.SPRINT:
                break;
            default:
                break;
        }
    }
    
    void OnStateUpdate()
    {
        switch (_currentState)
        {
            case PlayerStateMode.LOCOMOTION:
                _animator.SetFloat("DirectionX", Input.GetAxis("Horizontal"));
                _animator.SetFloat("DirectionY", Input.GetAxis("Vertical"));
                break;
            case PlayerStateMode.ROLL:
                break;
            case PlayerStateMode.SPRINT:
                break;
            default:
                break;
        }
    }
    
    void OnStateExit()
    {
        switch (_currentState)
        {
            case PlayerStateMode.LOCOMOTION:
                break;
            case PlayerStateMode.ROLL:
                break;
            case PlayerStateMode.SPRINT:
                break;
            default:
                break;
        }
    }

    void TransitionToState(PlayerStateMode toState)
    {
        OnStateExit();
        _currentState = toState;
        OnStateEnter();
    }

    #endregion

    #region Private & Protected

    private PlayerStateMode _currentState;
    private Animator _animator;

    #endregion
}
