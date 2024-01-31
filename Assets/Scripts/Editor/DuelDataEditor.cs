using BBG.Conditions;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Effects;
using BBG.ValueGetters;
using BBG.Ygo;
using BBG.Ygo.DeckEditing;
using System.Linq;
using UnityEditor;

public class DuelDataEditor : DataEditorBase
{
    [MenuItem("Editors/Data Editor")]
    private static void Init()
    {
        GetWindow(typeof(DuelDataEditor)).Show();
    }

    protected override DataCategory[] GetCategories()
    {
        var ec = new DataCategory(typeof(Effect), "Assets/SO/Effects");
        ec.Types = ec.Types.Where(t => !t.IsSubclassOf(typeof(Modifier))).ToArray();

        return new DataCategory[]
        {
            new DataCategory(typeof(Deck), "Assets/SO/Decks"),
            new DataCategory(typeof(CardData), "Assets/Resources/Cards"){ name = "Card" },
            ec,
            new DataCategory(typeof(Effect), "Assets/SO/Costs") { name = "Cost", Types = ec.Types },
            new DataCategory(typeof(Modifier), "Assets/SO/Modifiers"){ name = "Modifier" },
            new DataCategory(typeof(Condition), "Assets/SO/Conditions"),
            new DataCategory(typeof(Feature), "Assets/SO/Features"),
            new DataCategory(typeof(CardsGetter), "Assets/SO/CardsGetters"),
            new DataCategory(typeof(IntGetter), "Assets/SO/IntGetters"),
        };
    }
}
