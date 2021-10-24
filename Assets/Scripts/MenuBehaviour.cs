using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuBehaviour : MonoBehaviour
{
    public void StartGame()
    {
        // "Stage1" is the name of the first scene we created.
        SceneManager.LoadScene("Stage1");
    }
}