using UnityEditor;
using UnityEditor.SceneManagement;

public class OpenScene
{
    [MenuItem("Open Scene/Start Screen %1")]
    private static void StartScreen()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/StartScreen.unity");
    }
    
    [MenuItem("Open Scene/Level %2")]
    private static void Level()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Level.unity");
    }
}
