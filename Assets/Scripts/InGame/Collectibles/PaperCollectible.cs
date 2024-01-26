using InGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
