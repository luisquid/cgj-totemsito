using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    public ParticleSystem confetti;

    public AudioSource fxSource;
    public AudioSource fxSource2;
    public AudioSource fxSource3;
    public AudioClip fxGameOver;
    public AudioClip fxRotation;
    public AudioClip fxMatch;
    public AudioClip fxTotem;

    [Header("Prefabs")]
    public GameObject RobotPrefab;
    public GameObject TotemPrefab;
    public GameObject TotemGuidePrefab;
    public GameObject SpawnParticles;

    [Header("Placer")]
    public Transform SpawnTotem;
    public Transform SpawnGuide;
    public Transform SpawnRobotLeft;
    public Transform SpawnRobotRight;

    [Header("Currents")]
    public TotemController currentTotemGuide;
    public TotemController currentTotem;

    public UIController uiController;

    public float timer = 0f;
    public bool gameStarted = false;

    [Header("Stats")]
    public int score;
    public int rounds;
    public float totalSeconds;

    public int totalRebooted;

    public CameraShake camShake;

    public void CheckMatch()
    {
        if (currentTotem.activeTotem.eulerAngles == currentTotemGuide.activeTotem.eulerAngles)
        {
            fxSource3.clip = fxMatch;
            fxSource3.volume = 0.3f;
            fxSource3.Play();
            fxSource3.volume = 1f;

            currentTotem.activeTotem.GetComponentInChildren<ChangeColorIfGuide>().ChangeColor();
            if(currentTotem != null)
                currentTotem.GoToNextTotem();

            if(currentTotemGuide != null)
                currentTotemGuide.GoToNextTotem();

            score++;

            Vector3 position = new Vector3(Random.Range(SpawnRobotLeft.position.x, SpawnRobotRight.position.x), SpawnRobotLeft.position.y, Random.Range(SpawnRobotLeft.position.z, SpawnRobotRight.position.z));

            GameObject Robot = Instantiate(RobotPrefab, position, Quaternion.identity);

            Robot.transform.eulerAngles = new Vector3(0f, -180f, 0f);
            Robot.GetComponent<Animator>().SetInteger("DanceId", Random.Range(0, 3));
        }
    }

    public void TotemCompleted()
    {
        camShake.SetCameraShake(0.2f);
        confetti.Play();
        totalSeconds += 10f;
        timer += 10f;
        rounds++;
        //uiController.UpdateScore();

        Destroy(currentTotem.gameObject);
        Destroy(currentTotemGuide.gameObject);

        currentTotem = null;
        currentTotemGuide = null;

        fxSource2.clip = fxTotem;
        fxSource2.volume = 0.5f;
        fxSource2.Play();
        fxSource2.volume = 1f;

        

        StartCoroutine(InstantiateTotems());
    }

    IEnumerator InstantiateTotems()
    {
        GameObject totem = Instantiate(TotemPrefab, SpawnTotem.position, Quaternion.identity);
        GameObject totemGuide = Instantiate(TotemGuidePrefab, SpawnGuide.position, Quaternion.identity);

        yield return new WaitForEndOfFrame();

        currentTotem = totem.GetComponent<TotemController>();
        currentTotemGuide = totemGuide.GetComponent<TotemController>();

        currentTotemGuide.InitTotem();
        currentTotem.InitTotem(currentTotemGuide);
    }

    #region UNITY METHODS
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameStart()
    {
        gameStarted = true;
        if(currentTotem != null)
            Destroy(currentTotem.gameObject);
        if(currentTotemGuide != null)
            Destroy(currentTotemGuide.gameObject);

        currentTotem = null;
        currentTotemGuide = null;

        timer = totalSeconds = 20f;
        score = 0;
        rounds = 0;
        //uiController.UpdateScore();
        StartCoroutine(InstantiateTotems());
    }

    public void GameOver()
    {
        gameStarted = false;
        fxSource.clip = fxGameOver;
        fxSource.Play();
        uiController.SetGameOver();

        for(int i = 0; i < currentTotem.totems.Count; i++)
        {
            currentTotem.totems[i].gameObject.AddComponent<BoxCollider>();
            currentTotemGuide.totems[i].gameObject.AddComponent<BoxCollider>();

            currentTotem.totems[i].gameObject.AddComponent<Rigidbody>();
            currentTotemGuide.totems[i].gameObject.AddComponent<Rigidbody>();
        }

        currentTotem.totems[2].gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position, 50f);
        currentTotemGuide.totems[2].gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position, 50f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f && gameStarted)
        {
            uiController.UpdateTimer();
            timer -= Time.deltaTime;
        }
        else if(gameStarted && timer <= 0f)
        {
            GameOver();
        }
    }

    #endregion
}
