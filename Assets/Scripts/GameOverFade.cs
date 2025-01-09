using System.Diagnostics;

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverFade : MonoBehaviour
    {

        public static EventManager eventManager;


        [Header("References")]

        [SerializeField, Tooltip("The canvas group that contains the fade image.")]
        public CanvasGroup m_CanvasGroup = default;

        [Header("Fade")]

        [SerializeField, Tooltip("The delay in seconds before fading starts when winning.")]
        public float m_WinDelay = 4.0f;

        [SerializeField, Tooltip("The delay in seconds before fading starts when losing.")]
        public float m_LoseDelay = 2.0f;

        [SerializeField, Tooltip("The duration in seconds of the fade.")]
        public float m_Duration = 1.0f;

        float m_Time;

        [SerializeField, Tooltip("Is game over?")]
        public bool m_GameOver = false;
        [SerializeField, Tooltip("Game is won.")]
        public bool m_Won = false;

        void Start()
        {
            EventManager.AddListener<GameOverEvent>(OnGameOver);
        }

        void Update()
        {
            if (m_GameOver)
            {
                // Update time.
                m_Time += Time.deltaTime;

                // Fade.
                if (m_Won)
                    m_CanvasGroup.alpha = Mathf.Clamp01((m_Time - m_WinDelay) / m_Duration);
                else
                    m_CanvasGroup.alpha = Mathf.Clamp01((m_Time - m_LoseDelay) / m_Duration);
            }



        }

        void OnGameOver(GameOverEvent evt)
        {
            UnityEngine.Debug.Log("GameOver" + m_GameOver);
            
            if (!m_GameOver)
            {
                m_CanvasGroup.gameObject.SetActive(true);
                m_GameOver = true;
                m_Won = evt.Win;
            }
        }

        // Event
        public void OnGameOverEvent()
        {

            m_CanvasGroup.gameObject.SetActive(true);

            m_GameOver = true;

        }

        


        void OnDestroy()
            {
                EventManager.RemoveListener<GameOverEvent>(OnGameOver);
            }
}

