using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TicTacToe.UI
{
    [HideMonoScript]
    [RequireComponent(typeof(UIDocument))]
    public class MainMenu : MonoBehaviour
    {
        [Scene, SerializeField] private string _matchScene;

        private VisualElement _root;
        private Button _playButton, _quitButton;
        private const string QuitButtonName = "Quit", PlayButtonName = "Play";

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _playButton = _root.Q<Button>(PlayButtonName);
            _quitButton = _root.Q<Button>(QuitButtonName);

            _playButton.RegisterCallback<ClickEvent>(Play);
            _quitButton.RegisterCallback<ClickEvent>(Quit);
        }

        private void OnDisable()
        {
            _playButton.UnregisterCallback<ClickEvent>(Play);
            _quitButton.UnregisterCallback<ClickEvent>(Quit);
        }

        private void Play(ClickEvent clickEvent) => SceneManager.LoadScene(_matchScene, LoadSceneMode.Single);

        private void Quit(ClickEvent clickEvent)
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}