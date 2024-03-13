using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{

    [SerializeField] GameObject goPopStageUI = null;

    [SerializeField] GameObject goNurseryUI = null;

    [SerializeField] GameObject goMeditationUI = null;
    [SerializeField] GameObject goCCMUI = null;

    // Start is called before the first frame update
    public void PopButton()
    {
        goPopStageUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void NurseryButton()
    {
        goNurseryUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void MeditationButton()
    {
        goMeditationUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CCMButton()
    {
        goCCMUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
