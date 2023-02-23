using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDGC : MonoBehaviour
{
    public static TDGC instance;
    private Box boxAmountController;
    public int boxesOrganized;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(boxesOrganized);
    }
}
