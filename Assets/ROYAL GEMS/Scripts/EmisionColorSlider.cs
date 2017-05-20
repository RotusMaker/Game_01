using UnityEngine;
using System.Collections;

public class EmisionColorSlider : MonoBehaviour {

	public float Color_R = 1F;
	public float Color_G = 0F;
	public float Color_B = 0F;
	public float ColorSelection = 0;

	void Update () {
		Renderer renderer = GetComponent<Renderer> ();
		Material mat = renderer.material;


		if (ColorSelection == 0) { Color_R = 1f; Color_G = 0f; Color_B = 0f;}

		if (ColorSelection == 1) { Color_R = 1f; Color_G = 0.2f; Color_B = 0f;}
		if (ColorSelection == 2) { Color_R = 1f; Color_G = 0.4f; Color_B = 0f;}
		if (ColorSelection == 3) { Color_R = 1f; Color_G = 0.6f; Color_B = 0f;}
		if (ColorSelection == 4) { Color_R = 1f; Color_G = 0.8f; Color_B = 0f;}
		if (ColorSelection == 5) { Color_R = 1f; Color_G = 1f; Color_B = 0f;}

		if (ColorSelection == 6) { Color_R = 0.8f; Color_G = 1f; Color_B = 0f;}
		if (ColorSelection == 7) { Color_R = 0.6f; Color_G = 1f; Color_B = 0f;}
		if (ColorSelection == 8) { Color_R = 0.4f; Color_G = 1f; Color_B = 0f;}
		if (ColorSelection == 9) { Color_R = 0.2f; Color_G = 1f; Color_B = 0f;}
		if (ColorSelection == 10) { Color_R = 0f; Color_G = 1f; Color_B = 0f;}

		if (ColorSelection == 11) { Color_R = 0f; Color_G = 1f; Color_B = 0.2f;}
		if (ColorSelection == 12) { Color_R = 0f; Color_G = 1f; Color_B = 0.4f;}
		if (ColorSelection == 13) { Color_R = 0f; Color_G = 1f; Color_B = 0.6f;}
		if (ColorSelection == 14) { Color_R = 0f; Color_G = 1f; Color_B = 0.8f;}
		if (ColorSelection == 15) { Color_R = 0f; Color_G = 1f; Color_B = 1f;}

		if (ColorSelection == 16) { Color_R = 0f; Color_G = 0.8f; Color_B = 1f;}
		if (ColorSelection == 17) { Color_R = 0f; Color_G = 0.6f; Color_B = 1f;}
		if (ColorSelection == 18) { Color_R = 0f; Color_G = 0.4f; Color_B = 1f;}
		if (ColorSelection == 19) { Color_R = 0f; Color_G = 0.2f; Color_B = 1f;}
		if (ColorSelection == 20) { Color_R = 0f; Color_G = 0f; Color_B = 1f;}

		if (ColorSelection == 21) { Color_R = 0.2f; Color_G = 0f; Color_B = 1f;}
		if (ColorSelection == 22) { Color_R = 0.4f; Color_G = 0f; Color_B = 1f;}
		if (ColorSelection == 23) { Color_R = 0.6f; Color_G = 0f; Color_B = 1f;}
		if (ColorSelection == 24) { Color_R = 0.8f; Color_G = 0f; Color_B = 1f;}
		if (ColorSelection == 25) { Color_R = 1f; Color_G = 0f; Color_B = 1f;}

		if (ColorSelection == 26) { Color_R = 1f; Color_G = 0f; Color_B = 0.8f;}
		if (ColorSelection == 27) { Color_R = 1f; Color_G = 0f; Color_B = 0.6f;}
		if (ColorSelection == 28) { Color_R = 1f; Color_G = 0f; Color_B = 0.4f;}
		if (ColorSelection == 29) { Color_R = 1f; Color_G = 0f; Color_B = 0.2f;}
		if (ColorSelection == 30) { Color_R = 1f; Color_G = 0f; Color_B = 0f;}

		if (ColorSelection == 31) { Color_R = 0.8f; Color_G = 0f; Color_B = 0f;}
		if (ColorSelection == 32) { Color_R = 0.6f; Color_G = 0f; Color_B = 0f;}
		if (ColorSelection == 33) { Color_R = 0.4f; Color_G = 0f; Color_B = 0f;}
		if (ColorSelection == 34) { Color_R = 0.2f; Color_G = 0f; Color_B = 0f;}
		if (ColorSelection == 35) { Color_R = 0f; Color_G = 0f; Color_B = 0f;}

		if (ColorSelection == 36) { Color_R = 0.4f; Color_G = 0.2f; Color_B = 0.2f;}
		if (ColorSelection == 37) { Color_R = 0.2f; Color_G = 0.4f; Color_B = 0.2f;}
		if (ColorSelection == 38) { Color_R = 0.2f; Color_G = 0.2f; Color_B = 0.4f;}
		if (ColorSelection == 39) { Color_R = 0.4f; Color_G = 0.2f; Color_B = 0.4f;}
		if (ColorSelection == 40) { Color_R = 0.4f; Color_G = 0.4f; Color_B = 0.2f;}
		if (ColorSelection == 41) { Color_R = 0.2f; Color_G = 0.4f; Color_B = 0.4f;}

		if (ColorSelection == 42) { Color_R = 0.2f; Color_G = 0.2f; Color_B = 0.2f;}
		if (ColorSelection == 43) { Color_R = 0.4f; Color_G = 0.4f; Color_B = 0.4f;}
		if (ColorSelection == 44) { Color_R = 0.6f; Color_G = 0.6f; Color_B = 0.6f;}
		if (ColorSelection == 45) { Color_R = 0.8f; Color_G = 0.8f; Color_B = 0.8f;}
		if (ColorSelection == 46) { Color_R = 1f; Color_G = 1f; Color_B = 1f;}

			Color baseColor = new Color (Color_R, Color_G, Color_B);
			Color finalColor = baseColor /* * Mathf.LinearToGammaSpace (emission)*/;
			mat.SetColor ("_EmissionColor", finalColor);
		
	}
	public void AdjustColor(float newColorSelection) {
		ColorSelection = newColorSelection;
	}
}
