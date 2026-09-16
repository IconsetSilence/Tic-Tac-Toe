using System.IO;
using TicTacToe.Core;
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
        private Button _playButton, _resetButton, _quitButton;
        private const string QuitButtonName = "quit-button", ResetButtonName = "reset-button", PlayButtonName = "play-button";
        private static string CountDataPath => Path.Combine(Application.persistentDataPath, "count_save.json");

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _playButton = _root.Q<Button>(PlayButtonName);
            _resetButton = _root.Q<Button>(ResetButtonName);
            _quitButton = _root.Q<Button>(QuitButtonName);

            _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
            _resetButton.RegisterCallback<ClickEvent>(OnResetButtonClick);
            _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        }

        private void OnDisable()
        {
            _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
            _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        }

        private void OnPlayButtonClick(ClickEvent clickEvent) => SceneManager.LoadScene(_matchScene, LoadSceneMode.Single);

        private void OnResetButtonClick(ClickEvent clickEvent) => DataSaveSystem.Instance.Delete(CountDataPath);

        private void OnQuitButtonClick(ClickEvent clickEvent)
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}