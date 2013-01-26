using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Colorize")]
public class Colorize : ImageEffectBase {
	public Color  color;

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
        material.SetColor("_Color", color);
		Graphics.Blit (source, destination, material);
	}
}