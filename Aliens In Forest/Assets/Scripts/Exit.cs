using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public GameObject eKey;
    private SceneController sc;
    [SerializeField] private float radius;
    public LayerMask playerLayer;
    public Animator crossfade;
    [SerializeField] private float waitingTime;
    void Start()
    {
        sc = FindObjectOfType<SceneController>().GetComponent<SceneController>();
    }
    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, radius, playerLayer);

        if (hit != null)
        {
            eKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(NextScene());
            }
        }
        else
        {
            eKey.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

    IEnumerator NextScene()
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(waitingTime);

        sc.LoadScene(sceneName);
    }

}
