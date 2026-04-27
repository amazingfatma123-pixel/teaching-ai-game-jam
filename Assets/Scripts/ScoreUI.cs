using UnityEngine;
using TMPro;
using Unity.Profiling.LowLevel;
using System;
using JetBrains.Annotations;

public class ScoreUI : MonoBehaviour
{

    public float companyValue = 0;
    public TextMeshProUGUI valueText;

    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;

    

    void Start()
    {

        UpdateUI();


    }

    //Move the OnTriggerEnter2D code into an AI-center script
    private void OnTriggerEnter2D(Collider2D other)
    {
        //These if checks aren't needed anymore.

        /*if (other.transform.tag == "Enemy")
        {
            companyValue -= 10;
            valueText.text = "Company Value = " + companyValue.ToString("F2");

            //SoundFXManager.Instance.PlaySoundFXClip(positiveValueSound, transform, 0.2f);
            UpdateUI();
            Destroy(other.gameObject);

        }

        if (other.transform.tag == "GoodEnemy")
        {
            companyValue += 10;
            valueText.text = "Company Value = " + companyValue.ToString("F2");

            //SoundFXManager.Instance.PlaySoundFXClip(negativeValueSound, transform, 0.2f);

            UpdateUI();

            Destroy(other.gameObject);
            Debug.Log(companyValue.ToString("F2"));
        }*/ 

        if (other.TryGetComponent(out Prompt prompt))
        {
            int severity = prompt.GetSeverity();

            float severityScore = CalculateSeverityScore(severity);

            //Instead of this, create an UpdateScore(float change) method in this script and call that from the AI-center script, passing in the score change.
            companyValue += severityScore;
            UpdateUI();

            Debug.Log("Severity: " + severity + ", Score: " + severityScore + ", Company Value: " + companyValue.ToString("F2"));
        }


    }

    //Also move this to the AI-center script.
    private float CalculateSeverityScore(int severity) 
    {
        severity = Math.Clamp(severity, 0, 3);

        return (-12.5f * severity) + 7.5f;    
    }


   
    void UpdateUI()
    {
        if (valueText != null) { 
        valueText.text = "Company Value = " + companyValue.ToString("F2");

    }


    }
}
