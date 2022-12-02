using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int CurrentLevel;
    public ArrayList ActiveAudioList;
    public Camera MainCc;
    public Camera SavedLevel1Cc;
    public Camera Level2Cc;
    public Camera Level3Cc;
    public GameObject FadeInOverlay;
    private bool isGameFailed;
    private bool isGameEnd;


    struct CAMERA_MOVE_PARAMETER
    {
        public float lerpValue;
        public bool startMove;
        public int fromLevel;
        public int targetLevel;
        public float timer;
    };

    private CAMERA_MOVE_PARAMETER ccParameter;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        CurrentLevel = 0;
    }

    public void HandleZoneOneTwoSwitch()
    {
        ccParameter.startMove = true;
        ccParameter.fromLevel = CurrentLevel;
        CurrentLevel = CurrentLevel == 0 ? 1 : 0;
        ccParameter.targetLevel = CurrentLevel;
        ccParameter.lerpValue = 0;
        ccParameter.timer = 0;
    }

    public void HandleZoneTwoThreeSwitch()
    {
        ccParameter.startMove = true;
        ccParameter.fromLevel = CurrentLevel;
        CurrentLevel = CurrentLevel == 1 ? 2 : 1;
        ccParameter.targetLevel = CurrentLevel;
        ccParameter.lerpValue = 0;
        ccParameter.timer = 0;
       
    }

    private Camera GetCameraBaseOnLevel(int level)
    {
        switch (level)
        {
            case 0:
                return SavedLevel1Cc;
            case 1:
                return Level2Cc;
            case 2:
                return Level3Cc;
            default:
                return SavedLevel1Cc;
        }

    }

    public bool HandleCameraMovement(int targetLevel, int fromLevel)
    {
        Vector3 targetPos = GetCameraBaseOnLevel(targetLevel).transform.position;
        Vector3 vel = Vector3.zero;
        MainCc.transform.position = Vector3.MoveTowards(MainCc.transform.position, targetPos, 50 *Time.deltaTime);
        if(targetLevel == 2)
        {
            MainCc.orthographic = false;
            MainCc.fieldOfView = 60;
        }
        else
        {
            MainCc.orthographic = true;
            float targetCcSize = GetCameraBaseOnLevel(targetLevel).orthographicSize;
            float fromCcSize = GetCameraBaseOnLevel(fromLevel).orthographicSize;
            float currentSize = Mathf.Lerp(Mathf.Min(targetCcSize, fromCcSize), Mathf.Max(targetCcSize, fromCcSize), ccParameter.lerpValue);
            MainCc.orthographicSize = currentSize;
        }
        MainCc.transform.eulerAngles = Vector3.RotateTowards(MainCc.transform.eulerAngles, GetCameraBaseOnLevel(targetLevel).transform.eulerAngles, 50 * Time.deltaTime, 50 * Time.deltaTime);
        ccParameter.lerpValue += Time.deltaTime * 0.5f;
        ccParameter.timer += Time.deltaTime;
        
        if (Vector3.Distance(targetPos,MainCc.transform.position)<=0.1f) return false;
        else return true;
    }

    //Audio source dynamic list helper function
    public bool IsAudioActive(GameObject obj)
    {
        for(int i = 0;i<ActiveAudioList.Count;i++)
        {
            if ((GameObject)ActiveAudioList[i] == obj) return true;
        }
        return false;
    }

    public void ActiveAudio(GameObject obj)
    {
        if(!IsAudioActive(obj))
        {
            ActiveAudioList.Add(obj);
            obj.GetComponent<AudioSource>().Play();
        }
    }

    public void HandleGameEnd()
    {
        isGameEnd = true;
        StartCoroutine(HandleLoadScene(0));
    }
    public void HandleGameFailed()
    {
        isGameFailed = true;
        isGameEnd = true;
        StartCoroutine(HandleLoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator HandleLoadScene(int buildIndex)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(buildIndex);
    }

    public bool GetIsGameFailed()
    {
        return isGameFailed;
    }

    public void DeActiveAudio(GameObject obj)
    {
        if(IsAudioActive(obj))
        {
            ActiveAudioList.Remove(obj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ActiveAudioList = new ArrayList();
        isGameFailed = false;
        isGameEnd = false;
        FadeInOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ccParameter.startMove)
        {
            if(!HandleCameraMovement(ccParameter.targetLevel,ccParameter.fromLevel))
            {
                if(ccParameter.targetLevel == 2)
                {
                    MainCc.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
                    Vector3 localPos = MainCc.transform.localPosition;
                    localPos.x = 0;
                    MainCc.transform.localPosition = localPos;
                }
                else
                {
                    MainCc.transform.parent = null;
                }
                ccParameter.startMove = false;
            }
        }
        if(isGameEnd)
        {
            FadeInOverlay.SetActive(true);
            Color color = FadeInOverlay.GetComponentInChildren<Image>().color;
            color.a += 0.5f * Time.deltaTime;
            FadeInOverlay.GetComponentInChildren<Image>().color = color;
        }
       
    }
}
