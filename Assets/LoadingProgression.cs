using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingProgression : MonoBehaviour
{
    public Image progressBar;
    public TextMeshProUGUI loadingText;
    public int currentAmount;
    public int maxAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmount > 0)
        {
            loadingText.text = "Loading (" + currentAmount + "/" + maxAmount + ")";
            progressBar.fillAmount = ((float)currentAmount / (float)maxAmount);
        }
    }
}
