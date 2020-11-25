using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelButton : MonoBehaviour
{
	public UnityEvent buttonIsPressed;
	private Renderer m_renderer;
	public GameObject visibleButton;
	private bool pressed = false;
	private Transform tButtonPosition;
	
	private int exitCount;
	
    // Start is called before the first frame update
    void Start()
    {
		exitCount = 0;
        m_renderer = visibleButton.GetComponent<Renderer>();
		tButtonPosition = visibleButton.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if(pressed)
			buttonIsPressed.Invoke();
		
		if(exitCount++ > 5){
			m_renderer.material.color = Color.white;
			pressed = false;
			tButtonPosition.localPosition = new Vector3(0, 0, 0);
			exitCount = 0;
		}
    }
	
	private void OnTriggerStay(Collider other){
		m_renderer.material.color = Color.grey;
		tButtonPosition.localPosition = new Vector3(0, 0, 0.2f);
		pressed = true;
		
		exitCount = 0;
	}
	
	/*
    private void OnTriggerExit(Collider other)
    {
        m_renderer.material.color = Color.white;
		pressed = false;
		tButtonPosition.localPosition = new Vector3(0, 0, 0);
    }
	
	private void OnTriggerEnter(Collider other)
    {
		m_renderer.material.color = Color.grey;
		tButtonPosition.localPosition = new Vector3(0, 0, 0.2f);
		pressed = true;
    }
	*/
	
}
