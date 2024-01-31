using BBG.Dueling.Actions;

namespace BBG.Dueling
{
    public class SynchrousEventsSetup : DuelComponent
    {
        private ActionProcessor actionProcessor;

        private void Awake()
        {
            var cq = gameObject.AddComponent<CoroutineQueue>();
            actionProcessor = gameObject.AddComponent<ActionProcessor>();
            actionProcessor.Wait = () => cq.Executing;
        }

        internal override void Setup(Duel duel)
        {
            base.Setup(duel);
            duel.PerformHandler = ProcessActionPerform;
        }

        private void ProcessActionPerform(DuelAction action)
        {
            actionProcessor.Process(action.ProcessPerform, (int) action.priority);
        }
    }
}