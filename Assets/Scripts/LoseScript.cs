using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _LoseText = null;

    void Start()
    {
        Gate.OnLose += StartLosing;
    }

    private void OnDestroy()
    {
        Gate.OnLose -= StartLosing;
    }

    private void StartLosing()
    {
        _LoseText.SetActive( true );

        StartCoroutine( LoseCoroutine( 2.0f ) );
    }

    private IEnumerator LoseCoroutine( float t )
    {
        yield return new WaitForSeconds( t );

        SceneManager.LoadScene( 0 );
    }

}
