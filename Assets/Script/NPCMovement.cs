using UnityEngine;
using System.Collections;

public enum NPCMoveFrequency { stop, sometimes, normal, often }

public class NPCMovement : MovingObject
{   
    public NPCMoveFrequency frequency;
    private Coroutine moveStepCoroutine;

    void Start()
    {    
        StartCoroutine(MoveRoutine());
    }
    IEnumerator MoveRoutine()
    {
        if (!isMoving) moveStepCoroutine = StartCoroutine(MoveStep(ChooseDirection()));

        switch (frequency)
        {
            case NPCMoveFrequency.stop:
                StopCoroutine(MoveRoutine());
                break;
            case NPCMoveFrequency.sometimes:
                yield return new WaitForSeconds(6f);
                break;
            case NPCMoveFrequency.normal:
                yield return new WaitForSeconds(4f);
                break;
            case NPCMoveFrequency.often:
                yield return new WaitForSeconds(2f);
                break;
        }
        StartCoroutine(MoveRoutine());
    }   

    Vector2 ChooseDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: return Vector2.up; 
            case 1: return Vector2.down; 
            case 2: return Vector2.left; 
            case 3: return Vector2.right; 
        }
        return Vector2.zero;
    }   
}