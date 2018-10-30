 using UnityEngine;
 using System.Collections;
 
 public class HiResScreenShots : MonoBehaviour {
     /*public int resWidth; 
     public int resHeight;
 
     private bool takeHiResShot = false;
	 public Camera camera;
 
*/	public static string ScreenShotName(int width, int height) {
         return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
                              Application.dataPath, 
                              width, height, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
     }
     public void TakeHiResShot() {
         takeHiResShot = true;
     }

	 /*void Start(){
		 resWidth = camera.pixelWidth;
		 resHeight = camera.pixelHeight;
	 }
/* 
     void LateUpdate() {
         takeHiResShot |= Input.GetKeyDown("k");
         if (takeHiResShot) {
             RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
             camera.targetTexture = rt;
             Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
             camera.Render();
             RenderTexture.active = rt;
             screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
             camera.targetTexture = null;
             RenderTexture.active = null; // JC: added to avoid errors
             Destroy(rt);
             byte[] bytes = screenShot.EncodeToPNG();
             string filename = ScreenShotName(resWidth, resHeight);
             System.IO.File.WriteAllBytes(filename, bytes);
             Debug.Log(string.Format("Took screenshot to: {0}", filename));
             takeHiResShot = false;
         }
     }*/
	 public Texture2D mapScreen;
     private bool takeHiResShot = false;

     void LateUpdate(){
         takeHiResShot |= Input.GetKeyDown("k");
         if (takeHiResShot) {
             StartCoroutine(MapShot());
         }
     }
	
     public IEnumerator MapShot()
     {
		 yield return new WaitForEndOfFrame();
         mapScreen = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
         mapScreen.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
         mapScreen.Apply();
		 byte[] bytes = mapScreen.EncodeToPNG();
         string filename = ScreenShotName(Screen.width, Screen.height);
         System.IO.File.WriteAllBytes(filename, bytes);
         Debug.Log(string.Format("Took screenshot to: {0}", filename));
         takeHiResShot = false;
         //Took the shot
     }
 }