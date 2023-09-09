using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class GamePlayDetails
{
    public GameObject GamePlay;
    public GameObject WavePoint;
    public GameObject PointLineRenderer;
    public GameObject Reset_Btn;
    public GameObject Result;
    public GameObject NumberPanel;

}

public class MovPlayer : MonoBehaviour
{
    public static MovPlayer Instance;
    public GameObject Player;
    private GameObject Go;
    public GameObject Linerenderer;
    public GameObject Linerenderer_M;
    public Transform startPoint; // Assign the starting point in the Inspector
    public Transform MiddlePoint; // Assign the Middle point in the Inspector
    public Transform endPoint;   // Assign the ending point in the Inspector
    public Transform Parent;

    public GamePlayDetails GamePlayDetail;

    [Tooltip("path passage speed")]
    public float speed;
    [Tooltip("points of the path. delete or add elements to the list if you want to change the number of the points")]
    public Transform[] pathPoints;
    [Tooltip("whether 'Enemy' rotates in path passage direction")]
    public bool rotationByPath;
    [Tooltip("color of the path in the Editor")]
    public Color pathColor = Color.yellow;
    public Shooting shooting;
    [Tooltip("time between emerging of the enemies in the wave")]
    public float timeBetween;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
    }


    public void OnGamePlay()
    {
        StartCoroutine(CreateEnemyWave());
        Invoke("Late", .5f);
    }
    private void Late()
    {
        Parent.transform.rotation = Quaternion.Euler(0, 0, -60);
    }

    IEnumerator CreateEnemyWave() //depending on chosed parameters generating enemies and defining their parameters
    {

        Go = Instantiate(Player, Parent, true);
        // endPoint = Go.transform;


        FollowThePath followComponent = Go.GetComponent<FollowThePath>();
        followComponent.path = pathPoints;
        followComponent.speed = speed;
        // followComponent.rotationByPath = rotationByPath;     

        followComponent.SetPath();
        Enemy enemyComponent = Go.GetComponent<Enemy>();
        enemyComponent.shotChance = shooting.shotChance;
        enemyComponent.shotTimeMin = shooting.shotTimeMin;
        enemyComponent.shotTimeMax = shooting.shotTimeMax;


        // yield return new WaitForSeconds(timeBetween);
        // Set the positions of the line's start and end points       
        //Go.GetComponent<LineRenderer>().SetPosition(0, startPoint.position);
        // Go.GetComponent<LineRenderer>().SetPosition(1, Go.transform.position);

        yield return new WaitForSeconds(timeBetween);
        Go.SetActive(true);
        yield return new WaitForSeconds(.1f);

        OnGamePlay(true); trueplayer = true;

        yield return new WaitForSeconds(21);

        Go.SetActive(false);
        GamePlayDetail.Result.SetActive(true);
        GamePlayDetail.Result.transform.GetChild(0).gameObject.GetComponent<Text>().text = ParabolicProjectile.Instance.Number_Txt[5].text;
        yield return new WaitForSeconds(5);
        StopEvent();
        GamePlayDetail.Result.SetActive(false);
    }

    public void OnGamePlay(bool isBool)
    {
        GamePlayDetail.GamePlay.SetActive(isBool);
        GamePlayDetail.WavePoint.SetActive(isBool);
        GamePlayDetail.PointLineRenderer.SetActive(isBool);
    }

    public bool trueplayer = false;
    private void Update()
    {
        if (Go == null) return;

        if (Go != null)
        {
            if (trueplayer)
            {
                Linerenderer.GetComponent<LineRenderer>().SetPosition(0, Linerenderer_M.transform.position);
                Linerenderer.GetComponent<LineRenderer>().SetPosition(1, Go.transform.position);
            }
        }
        //else
        //{
        //    trueplayer = false;
        //    Debug.Log("false");
        //}
    }

    void StopEvent()
    {
        OnGamePlay(false);
        GamePlayDetail.Reset_Btn.SetActive(true);
        GamePlayDetail.NumberPanel.SetActive(false);
    }

    public void OnReset()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
