using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {

	public Camera cameraI;
	public Camera cameraO;

	public Material cameraMatI;
	public Material cameraMatO;

	// Use this for initialization
	void Start () {
		if (cameraI.targetTexture != null)
		{
			cameraI.targetTexture.Release();
		}
		cameraI.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatI.mainTexture = cameraI.targetTexture;

        if (cameraO.targetTexture != null)
        {
            cameraO.targetTexture.Release();
        }
        cameraO.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatO.mainTexture = cameraO.targetTexture;
    }
}