using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private List<Sprite> parallaxLayers = new List<Sprite>();
    [SerializeField] private GameObject camera;

    [SerializeField] private Vector3 bgCameraOffset = new Vector3(0, 0, 0.25f);
    [SerializeField] private float layerCameraOffset = 10f;

    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        // creating background go
        GameObject background = new GameObject("Background");
        background.transform.SetParent(camera.transform);
        background.transform.localPosition = bgCameraOffset;

        // creating parallax layers
        for (int i = 1; i <= parallaxLayers.Count; i++)
        {
            Transform savedTransform = background.transform;
            for (int j = 0; j < 3; j++)
            {
                GameObject newLayer = new GameObject("ParallaxLayer" + i);

                switch (j)
                {
                    case 0:
                        newLayer.transform.SetParent(background.transform);
                        savedTransform = newLayer.transform;
                        newLayer.transform.localPosition = new Vector3(0, 0, layerCameraOffset);
                        Parallax parallaxScript = newLayer.AddComponent<Parallax>();
                        parallaxScript.parallaxEffect = GetParallaxEffectValue(i);
                        parallaxScript.camera = camera;
                        break;
                    case 1:
                        newLayer.transform.SetParent(savedTransform);
                        newLayer.transform.localPosition = new Vector3(-1f * parallaxLayers[i - 1].bounds.size.x, 0, 0);
                        break;
                    case 2:
                        newLayer.transform.SetParent(savedTransform);
                        newLayer.transform.localPosition = new Vector3(parallaxLayers[i - 1].bounds.size.x, 0, 0);
                        break;
                    default:
                        Debug.Log("smth wrong with parallax layers");
                        break;
                }
                SpriteRenderer sprite = newLayer.AddComponent<SpriteRenderer>();
                sprite.sprite = parallaxLayers[i - 1];
                sprite.sortingOrder = i;
            }
        }
    }

    private float GetParallaxEffectValue(int layerNumber)
    {
        float value = 0;

        if (layerNumber == 1) value = 1;
        else if (layerNumber == parallaxLayers.Count) value = 0;
        else value = (1.000f / (parallaxLayers.Count - 1)) * (parallaxLayers.Count - layerNumber);

        return value;
    }
}