using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAPI : MonoBehaviour
{
    public static ManagerAPI Instance;
    public GameObject LogInPanel;
    public GameObject StartPanel;
    public LogInOutputs LogInOutput;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LogInPanel.SetActive(true);
        StartPanel.SetActive(false);
    }

    void OnClickLogIn() { }

}
[System.Serializable]
public class LogInOutputs
{
    public string message;
    public List<UserDatum> user_data;
    public int code;
}

[System.Serializable]
public class UserDatum
{
    public string id;
    public string name;
    public string mobile;
    public string email;
    public string profession;
    public string password;
    public string sw_password;
    public string created_date;
    public string updated_date;
    public string isDeleted;
}
