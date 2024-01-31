using BBG.Dueling.Settings;
using BBG.Ygo.DeckEditing;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BBG.Ygo.Network
{
    public class NetworkDataSetup : MonoBehaviourPunCallbacks
    {
        //set player name, decks and etc
        [SerializeField] DuelConfiguration networkedDuelConfig;
        [SerializeField] TMP_InputField playerNameIF;
        [SerializeField] TMP_Dropdown decksDropdown;
        [SerializeField] ImageSelection playerImage;
        [SerializeField] TMP_InputField initialLP;
        [SerializeField] TMP_InputField initialDrawCount;

        private PlayerData LocalPlayer => networkedDuelConfig.player;

        private void Start()
        {
            //start set ui
            playerNameIF.text = LocalPlayer.name;
            foreach (var deck in DeckValuesManager.Current.decks)
                decksDropdown.options.Add(new TMP_Dropdown.OptionData(deck.name));
            decksDropdown.value = DeckValuesManager.Current.EquippedIndex;
            decksDropdown.RefreshShownValue();
            playerImage.SetSelected(LocalPlayer.imageName);
            initialLP.text = LocalPlayer.initialLP.ToString();
            initialDrawCount.text = LocalPlayer.initialDrawCount.ToString();
            //methods to change name, equipped deck, image
            playerNameIF.onEndEdit.AddListener((s) => LocalPlayer.name = s);
            decksDropdown.onValueChanged.AddListener((v) => DeckValuesManager.Current.SetEquipped(v));
            initialLP.onEndEdit.AddListener(LpInput);
            initialDrawCount.onEndEdit.AddListener(InitialDrawInput);
            playerImage.SelectedChanged += () => LocalPlayer.imageName = playerImage.Selected.name;
        }

        private void LpInput(string text)
        {
            var value = Mathf.Clamp(int.Parse(text), 1000, 20000);
            LocalPlayer.initialLP = value;
            initialLP.text = value.ToString();
        }

        private void InitialDrawInput(string text)
        {
            var value = Mathf.Clamp(int.Parse(text), 0, 7);
            LocalPlayer.initialDrawCount = value;
            initialDrawCount.text = value.ToString();
        }
    }
}