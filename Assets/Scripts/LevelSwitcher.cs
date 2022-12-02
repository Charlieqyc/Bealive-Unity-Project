using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public bool IsLevelOneAndTwoSwitch;
    public bool IsLevelTwoAndThreeSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(IsLevelOneAndTwoSwitch && other.tag == "Player")
        {
            GameManager.Instance.HandleZoneOneTwoSwitch();
        }
        if (IsLevelTwoAndThreeSwitch && other.tag == "Player")
        {
            GameManager.Instance.HandleZoneTwoThreeSwitch();
        }
    }
}
