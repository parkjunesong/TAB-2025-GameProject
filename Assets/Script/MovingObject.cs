using NUnit.Framework.Interfaces;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovingObject : MonoBehaviour {


    public BoxCollider2D boxCollider;
    public LayerMask layerMask; // 충돌했을때 통과가 불가능한 레이어를 설정해주는 것

    public float Speed;
    protected Vector3 vector; // 부모 자식 관계만 접근 가능
    public int WalkCount;
    protected int CurrentWalkCount;
    public Animator animator;

    protected bool npcCanMove = true;

    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        npcCanMove = false;
        vector.Set(0, 0, vector.z);
        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
        }
        
        animator.SetFloat("MoveX", vector.x);
        animator.SetFloat("MoveY", vector.y);

        while (true)
        {
            bool CheckColisionFlag = CheckColision();
            if (CheckColisionFlag)
            {
                animator.SetBool("IsMoving", false);
                yield return new WaitForSeconds(1f); // 플레이어가 앞에 있으면 멈춤
            }
            else
            {
                break; // 플레이어가 없으면 움직임
            }
        }

        animator.SetBool("IsMoving", true);

        boxCollider.offset = new Vector2(vector.x * 0.01f * Speed * WalkCount, vector.y * 0.01f * Speed * WalkCount);

        while (CurrentWalkCount < WalkCount)
        {

            // PlayerManager에 있는 while 문을 가져왔는데 run과 관련된것과 if를 지움
            // if를 지운 이유는 위의 case때문에 어차피 x와 y 둘중 하나는 0이 되기에 대각선 이동이 안되기 때문
            transform.Translate(vector.x * Speed, vector.y * Speed, 0);
            CurrentWalkCount++;
            if (CurrentWalkCount == 12)
            {
                boxCollider.offset = Vector2.zero;
            }
            yield return new WaitForSeconds(0.01f);
        }
        CurrentWalkCount = 0;
        if(_frequency != 5)
        {
            animator.SetBool("IsMoving", false);
        }
        animator.SetBool("IsMoving", false);
        npcCanMove = true;
    }
    protected bool CheckColision()
    {
        RaycastHit2D hit;
        // A지점과 B지점이 있을때
        // A지점에서 B지점을 향해 레이저를 쏘았을때
        // 무사히 B지점에 도달한다면 hit == Null
        // 방해물에 막힌다면 hit == 방해물

        Vector2 start = transform.position; // A지점. 캐릭터의 현재 위치 값
        Vector2 end = start + new Vector2(vector.x * Speed * WalkCount, vector.y * Speed * WalkCount); // B지점. 캐릭터가 이동하고자 하는 위치 값
                                                                                                       // vector.x = 1, speed = 2.4로 설정함, walkcount는 20으로 설정했으므로 결과적으로 우리가 이동하고자 했던 위치인 48픽셀

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        // hit에서 캐릭터가 자신이 가진 박스에 충돌하게 되므로 잠깐 플레이어의 boxCollider를 끄고 레이저를 쏘고 다시 박스를 킴

        if (hit.transform != null)
        {
            return true;
        }
        return false;
        // 만약 플레이어 앞에 물체가 없으면 break가 포함되어 있는 반복문에서 빠져나옴
    }
}
