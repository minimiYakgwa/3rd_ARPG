using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playableDirector.gameObject.SetActive(true);
            playableDirector.Play();
        }
    }
}
