using InGame.UI;
using UnityEngine;

namespace InGame.Collectibles
{
    public class PaperCollectible : BaseCollectible
    {
        [SerializeField] private ParticleSystem vfx;

        override protected void OnCollected()
        {
            base.OnCollected();
            CollectedPapersCounter.AddToCounter();
            vfx.Play();
        }
    }
}
