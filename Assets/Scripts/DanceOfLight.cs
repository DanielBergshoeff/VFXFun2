using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class DanceOfLight : MonoBehaviour
{
    public static DanceOfLight Instance;

    public List<EffectVariables> vfxs;

    public float minTimer = 0f;
    public float maxTimer = 10f;
    public float direction = 0f;

    public float effectBoostMultiplier = 1.0f;
    
    public AudioClip[] clips;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        for (int i = 0; i < vfxs.Count; i++) {
            vfxs[i].vfxs = new VisualEffect[vfxs[i].vfxObjects.Length];
            for (int j = 0; j < vfxs[i].vfxObjects.Length; j++) {
                vfxs[i].vfxs[j] = vfxs[i].vfxObjects[j].GetComponent<VisualEffect>();
                vfxs[i].activated = true;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            menu.SetActive(!menu.activeSelf);

        if (AudioManager.Average> 3f) {
            direction = 1f;
        }

        for (int i = 0; i < vfxs.Count; i++) {
            if (!vfxs[i].activated)
                continue;

            if (vfxs[i].bar == 0) {
                foreach (VisualEffect vfx in vfxs[i].vfxs) {
                    vfx.SetFloat(vfxs[i].effectReference, vfxs[i].startValue + Mathf.Clamp((AudioManager.Average) * effectBoostMultiplier / (maxTimer - minTimer), 0f, 1f) * (vfxs[i].endValue - vfxs[i].startValue));
                }
            }
            else {
                foreach (VisualEffect vfx in vfxs[i].vfxs) {
                    vfx.SetInt(vfxs[i].effectReference, Mathf.RoundToInt(vfxs[i].startValue + Mathf.Clamp((AudioManager.FrequencyBands[vfxs[i - 1].bar]) * effectBoostMultiplier / (maxTimer - minTimer), 0f, 1f) * (vfxs[i].endValue - vfxs[i].startValue)));
                }
            }
        }
    }

    public void ChangeClip(int index) {
        AudioManager.myAudioSource.Stop();
        AudioManager.myAudioSource.clip = clips[index];
        AudioManager.myAudioSource.Play();
        menu.SetActive(false);
    }

    public void SetEffectBoostMultiplier(float value) {
        effectBoostMultiplier = value;
    }

    public void ExitApplication() {
        Application.Quit();
    }
}

[System.Serializable]
public class EffectVariables {
    public GameObject[] vfxObjects;
    [System.NonSerialized] public VisualEffect[] vfxs;
    [System.NonSerialized] public bool activated;
    [System.NonSerialized] public float timer;
    public int bar;
    public string effectReference;
    public float startValue;
    public float endValue;
}
