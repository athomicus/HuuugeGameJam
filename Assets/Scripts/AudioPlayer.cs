using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("ShootArrow")]
    [SerializeField] AudioClip shootArrowClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 1f;

    [Header("ClangSword")]
    [SerializeField] AudioClip ClangSword;
    [SerializeField] [Range(0f,1f)] float ClangSwordVolume = 1f;

    

   
    public void PlayArrowShoot()
    {
        PlayClip(shootArrowClip, shootingVolume);
    }
    

    public void PlaySwordClang()
    {
        PlayClip(ClangSword, ClangSwordVolume);
    }

   

   private void PlayClip(AudioClip clip, float volume)
    {
        var clipPosition = Camera.main.transform.position;
        if(clip!=null)
        {
            AudioSource.PlayClipAtPoint(clip,clipPosition);
        }

    }
}