using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class LogInAPIDetails
{
    public bool status;
    public string error;
}
//git link https://github.com/gaurav006/MumbaiTest.git
public class LogIn : MonoBehaviour
{
    // public LogInAPIDetails LogInAPIDetail;
    public GameObject Password, LoginID;
    private string Url;
    public Button OnLogIn;
    public GameObject Mgs;

    private void Start()
    {
        //// Get a reference to the Button component attached to the GameObject
        //OnLogIn = GetComponent<Button>();

        //// Check if the Button component exists
        //if (OnLogIn != null)
        //{
        //    // Add a click event listener to the button
        //    OnLogIn.onClick.AddListener(OnClickLogIn);
        //}
        //else
        //{
        //    Debug.LogError("Button component not found.");
        //}
        /* OnLogIn.GetComponent<Button>().onClick.AddListener(OnClickLogIn);*/
        Debug.Log("Start");
    }
    // Start is called before the first frame update
    public void OnClickLogIn()
    {

        string pass = Password.GetComponent<InputField>().text;
        string Id = LoginID.GetComponent<InputField>().text;
        //  string pass = "1234";
        // string Id = "8005007226";

        string Token = "c7d3965d49d4a59b0da80e90646aee77548458b3377ba3c0fb43d5ff91d54ea28833080e3de6ebd4fde36e2fb7175cddaf5d8d018ac1467c3d15db21c11b6909";
        Url = "http://68.183.92.60/leo_digital_media/api/User/UserLogin";
        Debug.Log("url : " + Url + ":ID:" + Id + " : :pass: :" + pass + ": :Token ::" + Token);

        if ((pass == string.Empty && Id == string.Empty) || (pass != string.Empty && Id == string.Empty) || (pass == string.Empty && Id != string.Empty))
        {
            Mgs.SetActive(true);
            Mgs.GetComponent<Text>().text = "Please the filled the detial.";
            Mgs.GetComponent<Text>().color = Color.red;
            Invoke("OnMgsClear", 2);
            return;
        }
        else if (pass != string.Empty && Id != string.Empty)
        {
            if ((pass.Length == 4) && (Id.Length == 10))
            {
                Debug.Log("Count");
                StartCoroutine(LogInId(Url, pass, Id, Token));
            }
            else
            {
                Mgs.SetActive(true);
                Mgs.GetComponent<Text>().text = "Please the filled the complete detial.";
                Mgs.GetComponent<Text>().color = Color.red; Invoke("OnMgsClear", 2);
            }
        }


    }
    void OnMgsClear()
    {
        Mgs.SetActive(false);
        Mgs.GetComponent<Text>().text = "";
    }

    IEnumerator LogInId(string url, string Password, string Id, string Token)
    {
        LogInOutputs loginData = new LogInOutputs();
        WWWForm form = new WWWForm();
        form.AddField("mobile", Id);
        form.AddField("password", Password);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SetRequestHeader("Token", Token);
        // www.downloadHandler = new DownloadHandlerBuffer();
        // request.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var responseText = www.downloadHandler.text;
            Debug.Log("Response: " + responseText);

            ManagerAPI.Instance.LogInOutput = new LogInOutputs();
            ManagerAPI.Instance.LogInOutput = JsonUtility.FromJson<LogInOutputs>(responseText.ToString());          
            Invoke("OnResponseAPICode", 1);
        }
        else
        {
            Debug.Log("error" + www.error);
            Mgs.GetComponent<Text>().text = www.error;
            Mgs.GetComponent<Text>().color = Color.red; Invoke("OnMgsClear", 2);
        }
    }
    void OnResponseAPICode()
    {
        Mgs.SetActive(true);
        if (ManagerAPI.Instance.LogInOutput.code == 200)
        {
            Mgs.GetComponent<Text>().color = Color.green;
            Mgs.GetComponent<Text>().text = ManagerAPI.Instance.LogInOutput.message;
            Invoke("OnLogin", 3);
            Invoke("OnMgsClear", 2);
        }
        else
        {
            Mgs.GetComponent<Text>().color = Color.red;
            Mgs.GetComponent<Text>().text = ManagerAPI.Instance.LogInOutput.message;
            Invoke("OnMgsClear", 2);
        }
    }

    public void OnLogin()
    {
        ManagerAPI.Instance.StartPanel.SetActive(true);
        ManagerAPI.Instance.LogInPanel.SetActive(false);
    }
}

