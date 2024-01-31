using System;

namespace BBG.Campaign
{
    [Serializable, SubclassPath("Campaign Actions", "Load Scene")]
    public class LoadSceneAction : CampaignAction
    {
        public SceneLoader sceneLoader;
        public string sceneName;

        protected override void OnInvoke(IActionContainer container)
        {
            sceneLoader.LoadScene(sceneName);
        }
    }
}