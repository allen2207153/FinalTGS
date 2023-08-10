using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange_Trap : MonoBehaviour
{
    public Animator transiton;

    public void changeScene()
    {
        transiton.SetTrigger("Start");
    }
   
}
