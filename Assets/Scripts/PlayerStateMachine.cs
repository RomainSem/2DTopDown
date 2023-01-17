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

    [Header("Timer")]
    [SerializeField] float _rollDuration = 0.25f;
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
                _animator.SetBool("isRolling", true);
                _endRollTime = Time.timeSinceLevelLoad + _rollDuration;
                break;
            case PlayerStateMode.SPRINT:
                _animator.SetBool("isSprinting", true);

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
                if (Input.GetButtonDown("Fire3"))
                {
                    TransitionToState(PlayerStateMode.ROLL);
                }
                break;
            case PlayerStateMode.ROLL:
                if (Time.timeSinceLevelLoad > _endRollTime)
                {
                    if (Input.GetButton("Fire3"))
                    {
                        TransitionToState(PlayerStateMode.SPRINT);
                    }
                    else
                    {
                        TransitionToState(PlayerStateMode.LOCOMOTION);
                    }

                }
                break;
            case PlayerStateMode.SPRINT:
                _animator.SetFloat("DirectionX", Input.GetAxis("Horizontal"));
                _animator.SetFloat("DirectionY", Input.GetAxis("Vertical"));
                if (Input.GetButtonUp("Fire3"))
                {
                    TransitionToState(PlayerStateMode.LOCOMOTION);
                }
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
                _animator.SetBool("isRolling", false);
                break;
            case PlayerStateMode.SPRINT:
                _animator.SetBool("isSprinting", false);
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
    private float _endRollTime;

    #endregion
}
