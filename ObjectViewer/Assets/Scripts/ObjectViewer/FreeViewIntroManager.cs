using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeViewIntroManager : MonoBehaviour
{
    public GameObject introManager, canvas, hiddenLineDrawing, objects, objectManager, axes, xAxis, yAxis, zAxis, pressAnyKeyText, UI;
	
	public GameObject pageNumberObject, outOf;
	private TextMeshProUGUI pageNumberText;
	
	public GameObject[] text;
	
	private int time = 0;
	private Transform t;
	private Color32 blank = new Color32(0, 0, 0, 0);
	public bool isRun = false;
	public string name;

	void Start()
    {
       
		pageNumberText = pageNumberObject.GetComponent<TextMeshProUGUI>();
		
		pageNumberObject.SetActive(true);
		outOf.SetActive(true);
		UI.SetActive(false);
        objects.SetActive(false);
		axes.SetActive(false);
		hiddenLineDrawing.SetActive(false);
		
		t = objects.GetComponent<Transform>();

		GameObject collect = GameObject.Find("CollectData");
		if (collect != null)
		{
			CollectData data = collect.GetComponent<CollectData>() as CollectData;
			name = data.playerName;
		}
        else
        {
			name = "";
        }
	}
	
	private int skipTimer = -1, pageNumber = 1;
	private bool pressed = false;
	
	
	void Update() {
		
		if (!Input.anyKey && pressed){
			pageNumber++;
			
		}
		pressed = Input.anyKey;
		
		
		/*
		if(!Input.anyKey && pressed){	
			if(!pressAnyKeyText.activeInHierarchy) {
				pressAnyKeyText.SetActive(true);
				skipTimer = 0;
			}
			else {
				canvas.SetActive(true);
				objects.SetActive(true);
				objectManager.GetComponent<ObjectViewerRig>().reset();
				axes.SetActive(true);
				hiddenLineDrawing.SetActive(true);
				introManager.SetActive(false);
			}
		}
		if(skipTimer >= 0) 
			if(skipTimer++ > 500)
				pressAnyKeyText.SetActive(false);
		
		pressed = Input.anyKey;
		
		
		*/
	}
	public GameObject explanationText;

    void FixedUpdate() // Manages the animation playing and deactivates the intro manager when done.
    {
		blink();

		/*GameObject[] objs = GameObject.FindGameObjectsWithTag("Intro Manager");
		Debug.Log(objs.Length);
		if (objs.Length > 1)
		{
			isRun = true;
			pageNumberObject.SetActive(false);
			outOf.SetActive(false);
			UI.SetActive(true);
			objects.SetActive(true);
			axes.SetActive(true);
			hiddenLineDrawing.SetActive(true);
			t.eulerAngles = new Vector3(0, 0, 0);
			explanationText.SetActive(false);
			GameObject.Destroy(this.gameObject);
			DontDestroyOnLoad(objs[1]);
		}*/

		string name = "";
		if (name.Equals(""))
		{
			GameObject collect = GameObject.Find("CollectData");
			if (collect != null)
			{
				CollectData data = collect.GetComponent<CollectData>() as CollectData;
				name = data.playerName;
			}
		}

		if (PlayerPrefs.GetInt("RanIntro" + name) == 1)
		{
			isRun = true;
			pageNumberObject.SetActive(false);
			outOf.SetActive(false);
			UI.SetActive(true);
			objects.SetActive(true);
			axes.SetActive(true);
			hiddenLineDrawing.SetActive(true);
			t.eulerAngles = new Vector3(0, 0, 0);
			text[7].SetActive(false);
			introManager.SetActive(false);
			explanationText.SetActive(false);
		}
		else
		{
			if (pageNumber == 1)
			{
				pageNumberText.text = "1";

				text[0].SetActive(true);
			}
			else if (pageNumber == 2)
			{
				pageNumberText.text = "2";
				objects.SetActive(true);
				time++;

				if (time == 100)
					objectManager.GetComponent<ObjectManager>().SetActive(2);
				else if (time == 200)
					objectManager.GetComponent<ObjectManager>().SetActive(3);
				else if (time == 300)
				{
					objectManager.GetComponent<ObjectManager>().SetActive(4);
					time = 0;
				}

				text[0].SetActive(false);
				text[1].SetActive(true);
			}
			else if (pageNumber == 3)
			{
				pageNumberText.text = "3";
				objectManager.GetComponent<ObjectManager>().SetActive(4);
				axes.SetActive(true);
				text[1].SetActive(false);
				text[2].SetActive(true);
			}
			else if (pageNumber == 4)
			{
				pageNumberText.text = "4";
				text[2].SetActive(false);
				text[3].SetActive(true);
			}
			else if (pageNumber == 5)
			{
				pageNumberText.text = "5";
				text[4].SetActive(true);
				blinkObject = xAxis;

				t.Rotate(new Vector3(1f, 0, 0), Space.World);
			}
			else if (pageNumber == 6)
			{
				pageNumberText.text = "6";
				text[4].SetActive(false);
				t.eulerAngles = new Vector3(0, 0, 0);
				xAxis.SetActive(true);
				pageNumber++;
			}
			else if (pageNumber == 7)
			{
				text[5].SetActive(true);
				blinkObject = yAxis;

				t.Rotate(new Vector3(0, 0, 1f), Space.World);
			}
			else if (pageNumber == 8)
			{
				pageNumberText.text = "7";
				text[5].SetActive(false);
				t.eulerAngles = new Vector3(0, 0, 0);
				yAxis.SetActive(true);
				pageNumber++;
			}
			else if (pageNumber == 9)
			{
				text[6].SetActive(true);
				blinkObject = zAxis;

				t.Rotate(new Vector3(0, 1f, 0), Space.World);
			}
			else if (pageNumber == 10)
			{
				pageNumberText.text = "8";
				zAxis.SetActive(true);
				text[3].SetActive(false);
				text[6].SetActive(false);
				text[7].SetActive(true);
				objects.SetActive(false);
				axes.SetActive(false);
			}

			else
			{
				isRun = true;
				pageNumberObject.SetActive(false);
				outOf.SetActive(false);
				UI.SetActive(true);
				objects.SetActive(true);
				axes.SetActive(true);
				hiddenLineDrawing.SetActive(true);
				t.eulerAngles = new Vector3(0, 0, 0);

				text[7].SetActive(false);
				introManager.SetActive(true);

				PlayerPrefs.SetInt("RanIntro" + name, 1);
			}
		}
		
		
		/*
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
		*/
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
			else if(blinkTimer == 30){
				blinkObject.SetActive(true);
				blinkObject = null;
				blinkTimer = 0;
			}
		}
	}
	
}





