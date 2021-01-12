using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ComplexityCalculator : MonoBehaviour
{
	public Camera[] cameras;
	public RenderTexture[] renderTextures;
	
	private Texture2D[] images = new Texture2D[6];
	private const int number_of_cameras = 6;
	
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < number_of_cameras; i++){ // Disable automatic rendering of all the extra cameras.
			cameras[i].enabled = false;
		}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void calculateComplexity(){
		for(int i = 0; i < number_of_cameras; i++){ // Render the six views of the object to the Render Textures
			cameras[i].Render();
		}
		
		for(int i = 0; i < number_of_cameras; i++){ // Convert the render textures to Texture2Ds.
			RenderTexture rTex = renderTextures[i];
			Texture2D tex = new Texture2D(500, 500, TextureFormat.R8, false);
			RenderTexture.active = rTex;
			tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
			tex.Apply();
		
			images[i] = tex;
		}
		
		float temp = 0f, total = 0f;
		
		// Calculate the euclidian distance between every possible pair of all six images.
		for(int i = 0; i < number_of_cameras; i++){
			for(int j = i + 1; j < number_of_cameras; j++){ 
			
				// Loop through each pixel of the images.
				for(int x = 0; x < images[i].width; x++){
					for(int y = 0; y < images[i].height; y++){
						//Debug.Log(images[i].GetPixel(x, y).r);
						//test += images[i].GetPixel(x, y).r;
						temp += Mathf.Pow((images[i].GetPixel(x, y).r - images[j].GetPixel(x, y).r), 2f);
					}
				}
				temp = Mathf.Sqrt(temp);
				total += temp;
				temp = 0f;
			}
		}
		
		// Write the final output to the screen and to a file.
		Debug.Log(total);	
		WriteString(total.ToString());
	}
	
	// Converts a rendertexture to a Tecture2D
	private Texture2D toTexture2D(RenderTexture rTex){
		Texture2D tex = new Texture2D(500, 500, TextureFormat.R8, false);
		RenderTexture.active = rTex;
		tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
		tex.Apply();
		return tex;
	}
	
	// Writes 'str' to a file.
	private void WriteString(string str)
    {
        string path = "Assets/Resources/output.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(str);
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path); 
        //TextAsset asset = Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
    }
}















