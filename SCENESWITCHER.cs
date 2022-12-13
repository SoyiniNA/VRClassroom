using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENESWITCHER : MonoBehaviour
{
    public void playlesson()
    {
        SceneManager.LoadScene("Classroom 1");
    }

    public void Next1()
     {
      SceneManager.LoadScene("Classroom 2");
    }

    public void Next2()
    {
       SceneManager.LoadScene("Classroom 3");
     }
    public void Next3()
     {
       SceneManager.LoadScene("Classroom 4");
     }

    public void Next()
        { 
      SceneManager.LoadScene("Classroom");
    }

  
    public void back()
    { 
      SceneManager.LoadScene("SampleScene");
    } 

    
}
