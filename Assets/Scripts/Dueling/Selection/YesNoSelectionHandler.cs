using UnityEngine;

namespace BBG.Dueling.Selection.View
{
    public class YesNoSelectionHandler : ManualSelectionHandler<YesNoSelection>
    {        
        public void Choose(bool yes)
        {
            selection.choice = yes;
            Confirm();
        }
    }
}