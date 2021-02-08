using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void TimeControl(float timeSpeed)
    {
        Time.timeScale = timeSpeed;
    }
}
