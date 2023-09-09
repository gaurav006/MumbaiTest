using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IncreaseDecreases
{
    public GameObject Positive, Nagetive_Obj, Number_Obj, Bet_Obj;
    public int Number;
    public int Interval = 10;
}

[System.Serializable]
public class StartDetails
{
    public GameObject  Back_Obj;
    public GameObject Title_Obj;
    public GameObject Start_Obj;
}

[System.Serializable]
public class ClockDetails
{
    public int Number;
    public int Interval;
    public GameObject Clock_Obj;
}
public class Number : MonoBehaviour
{
    public StartDetails StartDetail;
    public IncreaseDecreases IncreaseDecrease;
    public ClockDetails ClockDetail;

    public GameObject Redeem;


    // Start is called before the first frame update
    void Start()
    {
        IncreaseDecrease.Number_Obj.SetActive(false);
        IncreaseDecrease.Positive.SetActive(false);
        IncreaseDecrease.Nagetive_Obj.SetActive(false);
        IncreaseDecrease.Bet_Obj.SetActive(false);
        Redeem.SetActive(false);
    }


    public void OnInit()
    { 
        //StartDetail.Player_Obj.SetActive(true);
        StartDetail.Title_Obj.SetActive(true);
        StartDetail.Start_Obj.SetActive(false);
        
        IncreaseDecrease.Number_Obj.SetActive(true);
        IncreaseDecrease.Positive.SetActive(true);
        IncreaseDecrease.Nagetive_Obj.SetActive(true);
        IncreaseDecrease.Bet_Obj.SetActive(true);

    }


    private void Update()
    {
        // Check for input based on platform
#if UNITY_STANDALONE || UNITY_EDITOR
        HandleDesktopInput();
#elif UNITY_ANDROID
        HandleAndroidInput();
#endif      
    }

    private void HandleDesktopInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseNumber();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseNumber();
        }
    }

    private void HandleAndroidInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    DecreaseNumber();
                }
                else
                {
                    IncreaseNumber();
                }
            }
        }
    }


    public void IncreaseNumber()
    {
        if (IncreaseDecrease.Number < 150)
        {
            IncreaseDecrease.Number += IncreaseDecrease.Interval;
            Debug.Log("Increased Number: " + IncreaseDecrease.Number);
            IncreaseDecrease.Number_Obj.transform.GetChild(0).GetComponent<Text>().text = IncreaseDecrease.Number.ToString();
        }
    }

    public void DecreaseNumber()
    {
        if (IncreaseDecrease.Number > 0)
        {
            IncreaseDecrease.Number -= IncreaseDecrease.Interval;
            Debug.Log("Decreased Number: " + IncreaseDecrease.Number);
            IncreaseDecrease.Number_Obj.transform.GetChild(0).GetComponent<Text>().text = IncreaseDecrease.Number.ToString();
        }
    }

    
    public void OnBet()
    {       
        StartCoroutine(OnClock());
    }

    IEnumerator OnClock()
    {
        yield return new WaitForSeconds(1);
        StartDetail.Title_Obj.GetComponent<Text>().text = "Next round will be start";
        ClockDetail.Clock_Obj.SetActive(true);
        IncreaseDecrease.Bet_Obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Waits";
        IncreaseDecrease.Bet_Obj.GetComponent<Button>().interactable = false;
        while (ClockDetail.Number > -1)
        {
            ClockDetail.Clock_Obj.transform.GetChild(0).GetComponent<Text>().text = ClockDetail.Number.ToString();
            yield return new WaitForSeconds(1);
            ClockDetail.Number--;

            if (ClockDetail.Number == 0)
            {               
                IncreaseDecrease.Bet_Obj.SetActive(false);
                // StartDetail.Player_Obj.SetActive(false);
                MovPlayer.Instance.OnGamePlay(); // game Play Activity start 
                StartDetail.Title_Obj.SetActive(false);
                ClockDetail.Clock_Obj.SetActive(false);
                Redeem.SetActive(true);
                IncreaseDecrease.Bet_Obj.GetComponent<Button>().interactable = true;
                IncreaseDecrease.Positive.GetComponent<Button>().interactable=false;
                IncreaseDecrease.Nagetive_Obj.GetComponent<Button>().interactable = false;

            }
        }
    }

    public void OnRedeem()
    {
    }

     
}
