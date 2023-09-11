using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [Header("Button")]
    public GameObject selectImage1;
    public GameObject selectImage2;
    public GameObject selectImage3;
    [Header("Animation")]
    public Animator anim_startButton;
    public Animator anim_optionButton;
    public Animator anim_exitButton;


    public GameObject SceneMask;

    private int selectButton = 0;
    public void Start()
    {
        anim_startButton = GetComponentInChildren<Animator>();
    }
    public void startGame()
    {


    }


    public void option()
    {
        Debug.Log("OPTION");

    }
    public void quitGame()
    {
        Debug.Log("QUIT");
    }
    void Update()
    {
        if(selectButton == 0)
        {
            selectImage2.SetActive(false);
            selectImage3.SetActive(false);
            StartButton();
        }

        if (selectButton == 1)
        {
            selectImage1.SetActive(false);
            selectImage3.SetActive(false);
            anim_startButton.SetTrigger("Normal");
            optionButton();
        }

        if (selectButton == 2)
        {
            selectImage1.SetActive(false);
            selectImage2.SetActive(false);
            anim_startButton.SetTrigger("Normal");
            exitButton();
        }

        if (Input.GetButtonDown("Attack") && selectImage1 == true)
        {
            anim_startButton.SetTrigger("Pressed");

        }
        if (Input.GetKeyDown("down"))
        {
            selectButton += 1;
        }
        if (Input.GetKeyDown("up"))
        {
            selectButton -= 1;
        }
    }

    public void StartButton()
    {
        if (!selectImage1.activeInHierarchy)
        {
            selectImage1.SetActive(true);

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(selectImage1);
            anim_startButton.SetTrigger("Selected");

        }
        

    }

    public void optionButton()
    {
        if (!selectImage2.activeInHierarchy)
        {
            selectImage2.SetActive(true);

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(selectImage2);
            anim_optionButton.SetTrigger("Selected");

        }

    }

    public void exitButton()
    {
        if (!selectImage3.activeInHierarchy)
        {
            selectImage3.SetActive(true);

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(selectImage3);
            anim_exitButton.SetTrigger("Selected");

        }

    }
    public void changeScene()
    {
        SceneMask.SetActive(true);
        StartCoroutine(nextScene());
    }
    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
