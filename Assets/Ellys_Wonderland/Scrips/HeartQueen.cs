using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartQueen : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;

    private enum State { Normal, Groggy }
    private State currentState = State.Normal;

    public float groggyDuration = 10f;
    private bool isDead = false;

    public float skillDelayAfterGroggy = 2f;
    private bool canUseSkill = true;

    public GameObject warningPrefab;
    public GameObject notePrefab;
    public GameObject soldierPrefab1;
    public GameObject soldierPrefab2;

    private Animator anim;

    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private void Start()
    {
        currentHP = maxHP;
        StartCoroutine(SkillRoutine());

        anim = GetComponent<Animator>();

    }


    public void TakeStompDamage()
    {
        if (currentState == State.Groggy && !isDead)
        {
            currentHP--;
            Debug.Log($"하트여왕이 피해를 입었다! 남은 체력: {currentHP}");

            if (currentHP <= 0)
            {
                Die();
            }
            else
            {
                ExitGroggy();
            }

        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("하트여왕 사망!");
        Destroy(gameObject);
        Debug.Log($"[TimelineSceneLoader] Loading scene: {nextSceneName}" );
        SceneManager.LoadScene(nextSceneName);
    }

    public string nextSceneName;

    public void LoadNextScene()
    {
        
    }

    private void EnterGroggy()
    {
        currentState = State.Groggy;
        canUseSkill = false;
        Debug.Log("그로기 상태 진입");
        StartCoroutine(GroggyTimer());
    }
    private void ExitGroggy()
    {
        currentState = State.Normal;
        Debug.Log("그로기 해제");
        StartCoroutine(SkillCooldownDelay());
    }

    private IEnumerator GroggyTimer()
    {
        yield return new WaitForSeconds(groggyDuration);
        ExitGroggy();
    }

    private IEnumerator SkillCooldownDelay()
    {
        yield return new WaitForSeconds(skillDelayAfterGroggy);
        canUseSkill = true;
    }

    private IEnumerator SkillRoutine()
    {
        while (!isDead)
        {
            if (currentState == State.Normal && canUseSkill)
            {
                yield return new WaitForSeconds(1f);

                int skillIndex = Random.Range(0, 2);
                if (skillIndex == 0)
                    yield return StartCoroutine(UseNoteSkill());
                else
                    yield return StartCoroutine(UseSummonSkill());
            }
            yield return null;
        }
    }

    private IEnumerator UseNoteSkill()
    {
        Debug.Log("스킬1: 음표");

        anim.SetTrigger("IsSing");

        yield return new WaitForSeconds(1f);

        GameObject ground = GameObject.FindWithTag("Ground");
        if (ground == null)
        {
            Debug.LogError("Ground 오브젝트를 찾을 수 없습니다.");
            yield break;
        }

        float groundMinX = ground.transform.position.x - ground.transform.localScale.x / 2f;
        float groundMaxX = ground.transform.position.x + ground.transform.localScale.x / 2f;
        float groundY = ground.transform.position.y;

        GameObject[] warnings = new GameObject[4];
        Vector3[] positions = new Vector3[4];

        for (int i = 0; i < 4; i++)
        {
            float randX = Random.Range(groundMinX, groundMaxX);
            Vector3 pos = new Vector3(randX, groundY, 0);
            positions[i] = pos;
            warnings[i] = Instantiate(warningPrefab, pos, Quaternion.identity);
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 4; i++)
        {
            GameObject note = Instantiate(notePrefab, positions[i] + Vector3.up * 5f, Quaternion.identity);

            Note noteScript = note.GetComponent<Note>();
            if (noteScript != null && warnings[i] != null)
            {
                noteScript.warning = warnings[i];
            }
        }

        EnterGroggy();
    }

    private IEnumerator UseSummonSkill()
    {
        Debug.Log("스킬2: 카드병정 소환");

        anim.SetTrigger("IsSummon");
        yield return new WaitForSeconds(1f);

        GameObject leftSoldier = Instantiate(soldierPrefab1, leftSpawnPoint.position, Quaternion.identity);
        GameObject rightSoldier = Instantiate(soldierPrefab2, rightSpawnPoint.position, Quaternion.identity);

        while (true)
        {
            bool leftDead = leftSoldier == null || !leftSoldier;
            bool rightDead = rightSoldier == null || !rightSoldier;

            if (leftDead && rightDead)
                break;

            yield return null;
        }

        EnterGroggy();
    }

    public bool IsGroggy() => currentState == State.Groggy;


}