
using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void OnStartGame(string sceneName)

    {

        Application.LoadLevel(sceneName);

    }

}
