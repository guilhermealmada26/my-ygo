using BBG.Animations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BBG.Campaign
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] GameObject dialoguePanel, headerParent;
        [SerializeField] TextMeshProUGUI dialogueText, headerText;
        [SerializeField] Image dialogueImage;
        [SerializeField] float showLineSpeed = 20f;
        [SerializeField] UnityEvent onStateChanged;

        private ShowStringAnimation stringAnimation;
        private Action onDisable;
        private Queue<string> lines = new(3);

        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            stringAnimation = new ShowStringAnimation(dialogueText, showLineSpeed);
        }

        public void ShowLine(string line, string header, Sprite sprite, Action onDisable = null)
        {
            dialoguePanel.SetActive(true);
            headerText.text = header;
            headerParent.SetActive(header != null);
            dialogueImage.sprite = sprite;
            dialogueImage.gameObject.SetActive(sprite != null);
            this.onDisable = onDisable;
            onStateChanged.Invoke();
            StartCoroutine(stringAnimation.ShowLineAnimation(line));
        }

        public void ShowLines(string[] lines, string header, Sprite sprite, Action onDisable = null)
        {
            this.lines = new Queue<string>(lines);
            headerText.text = header;
            dialogueImage.sprite = sprite;
            this.onDisable = onDisable;
            NextLine();
        }

        private bool NextLine()
        {
            if (lines.Count == 0)
                return false;
            ShowLine(lines.Dequeue(), headerText.text, dialogueImage.sprite, onDisable);
            return true;
        }

        public bool Close()
        {
            if (stringAnimation.IsAnimating)
            {
                stringAnimation.Skip(this);
                return false;
            }
            if (NextLine())
                return false;

            dialoguePanel.SetActive(false);
            onStateChanged.Invoke();
            onDisable?.Invoke();
            return true;
        }
    }
}