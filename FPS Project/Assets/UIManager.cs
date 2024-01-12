using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI checkpointText;

    bool checkpointTextBool = false;

    float timeElapsed;
    [SerializeField] float checkpointTextTime;

    private void Update()
    {
        if (checkpointTextBool)
        {
            if (timeElapsed < checkpointTextTime)
            {
                checkpointText.alpha = Mathf.Lerp(1, 0, timeElapsed / checkpointTextTime);
                timeElapsed += Time.deltaTime;
            }

            else
                checkpointTextBool = false;
        }
    }

    public void CheckpointReached()
    {
        checkpointTextBool = true;
        timeElapsed = 1;
    }
}
