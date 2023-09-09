using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParabolicProjectile : MonoBehaviour
{
    public static ParabolicProjectile Instance;
    public List<float> Number_Y;
    public List<Text> Number_Txt;
    public float NumberFloat;
    public List<float> oldDatanumber;

    private void Start()
    {
        Instance = this;
        oldDatanumber = new List<float>();
        for (int i = 0; i < Number_Y.Count; i++)
        {
            Number_Txt[i].text = Number_Y[i].ToString() + "s";
            oldDatanumber.Add(Number_Y[i]);
        }
        InvokeRepeating("Increasenumber", 0.1f, 0.5f);
    }

    void Increasenumber()
    {
        for (int i = 0; i < Number_Y.Count; i++)
        {
            Number_Y[i] = Number_Y[i] + NumberFloat;
            Number_Txt[i].text = Number_Y[i].ToString("F1") + "s";
        }

    }

    private void OnDisable()
    {
        for (int i = 0; i < Number_Y.Count; i++)
        {
            Number_Y[i] = oldDatanumber[i];
        }
    }
}
