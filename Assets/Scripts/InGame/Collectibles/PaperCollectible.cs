using InGame.UI;

namespace InGame.Collectibles
{
    public class PaperCollectible : BaseCollectible
    {
        override protected void OnCollected()
        {
            base.OnCollected();

            CollectedPapersCounter.AddToCounter();
        }
    }
}
