using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OVExplanation : MonoBehaviour
{
   public void ContinueToOV()
   {
		SceneManager.LoadScene("OVFreeView");
   }
   
   public void GoBack()
   {
		SceneManager.LoadScene("Menu");
   }
}
