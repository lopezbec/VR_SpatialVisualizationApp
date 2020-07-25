using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeViewIntroManager : MonoBehaviour
{
    public GameObject introManager, canvas, hiddenLineDrawing, objects, objectManager, axes, xAxis, yAxis, zAxis;
	
	public GameObject[] text;
	
	private int time = 0;
	private Transform t;
	private Color32 blank = new Color32(0, 0, 0, 0);
	
    void Start()
    {
		canvas.SetActive(false);
        objects.SetActive(false);
		axes.SetActive(false);
		hiddenLineDrawing.SetActive(false);
		
		t = objects.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
		time++;
		
		if(time == 50)
			fadeInText = text[0];
		else if(time == 350)
			fadeOutText = text[0];
		
		else if(time == 450) {
			fadeInText = text[1];
			objects.SetActive(true);
		}
		
		else if(time == 550) 
			objectManager.GetComponent<ObjectManager>().SetActive(2);
		else if(time == 650) 
			objectManager.GetComponent<ObjectManager>().SetActive(3);
		else if(time == 750) {
			objectManager.GetComponent<ObjectManager>().SetActive(4);
			fadeOutText = text[1];
		}
		
		else if(time == 900){
			fadeInText = text[2];
			axes.SetActive(true);
		}
		
		else if(time == 1150)
			fadeOutText = text[2];
		
		else if(time == 1250)
			fadeInText = text[3];
		
		else if(time == 1320){
			fadeInText = text[4];		
			blinkObject = xAxis;
		}
		
		else if(time > 1420 && time < 1420 + 360)
			t.Rotate(new Vector3(1f, 0, 0), Space.World);
			
		else if(time == 1780)
			fadeOutText = text[4];
		
		else if(time == 1850){
			fadeInText = text[5];
			blinkObject = yAxis;
		}
		
		else if(time > 1950 && time < 1950 + 180)
			t.Rotate(new Vector3(0, 0, 2f), Space.World);
	
		else if(time == 2130){
			fadeOutText = text[5];
			t.eulerAngles = new Vector3(0, 0, 0);
		}
		
		else if(time == 2250){
			fadeInText = text[6];
			blinkObject = zAxis;
		}
		
		else if(time > 2350 && time < 2350 + 180)
			t.Rotate(new Vector3(0, 2f, 0), Space.World);
		
		else if(time == 2530){
			fadeOutText = text[6];
			t.eulerAngles = new Vector3(0, 0, 0);
		}

		else if(time == 2600)
			fadeOutText = text[3];
		
		else if(time == 2650){
			fadeInText = text[7];
			objects.SetActive(false);
			axes.SetActive(false);
		}
		
		else if(time == 2950)
			fadeOutText = text[7];
		
		if(time > 3000) {
			canvas.SetActive(true);
			objects.SetActive(true);
			axes.SetActive(true);
			hiddenLineDrawing.SetActive(true);
			introManager.SetActive(false);
		}
		
		FadeText();
		blink();
    }
	
	private GameObject fadeInText = null, fadeOutText = null; 
	private void FadeText(){
		if(fadeInText != null) {
			if(fadeInText.activeInHierarchy){
				if(fadeInText.GetComponent<TextMeshPro>().color.a < 3.9f)
					fadeInText.GetComponent<TextMeshPro>().color += new Color32(0, 0, 0, 10);
				else
					fadeInText = null;
			}
			else {
				fadeInText.SetActive(true);
				fadeInText.GetComponent<TextMeshPro>().color = blank;
			}
		}
		
		if(fadeOutText != null) {
			if(fadeOutText.GetComponent<TextMeshPro>().color.a > 0.1f)
				fadeOutText.GetComponent<TextMeshPro>().color -= new Color32(0, 0, 0, 20);
			else {
				fadeOutText.GetComponent<TextMeshPro>().color = blank;
				fadeOutText.SetActive(false);
				fadeOutText = null;
			}
		}
	}
	
	private GameObject blinkObject = null;
	private int blinkTimer = 0;
	private void blink(){
		if(blinkObject != null){
			blinkTimer++;
			if(blinkTimer == 15)
				blinkObject.SetActive(false);
			else if(blinkTimer == 30)
				blinkObject.SetActive(true);
			else if(blinkTimer == 45)
				blinkObject.SetActive(false);
			else if(blinkTimer == 60)
				blinkObject.SetActive(true);
			else if(blinkTimer == 75)
				blinkObject.SetActive(false);
			else if(blinkTimer == 90){
				blinkObject.SetActive(true);
				blinkObject = null;
				blinkTimer = 0;
			}
		}
	}
	
}





