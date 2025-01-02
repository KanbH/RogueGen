using UnityEngine;

[CreateAssetMenu(fileName = "NewSFXSO", menuName = "Audio/Sound Effect Metadata")]
public class SoundFXSO : ScriptableObject
{
    public AudioClip AudioClip;

    [Range(0f, 1f)] 
    public float Volume = 1f;

    [Range(-3f, 3f)] 
    public float pitch = 1f;

    public bool loop = false;
}
