using UnityEngine;

namespace InGame.Player
{
    public class PlayerJumpSound : MonoBehaviour
    {
        [Header("Audio sources")]
        [SerializeField] private AudioSource jumpSound;
        [Header("Sound effect properties")]
        [SerializeField] private float chargeMaxTime = 2;
        [SerializeField] private float emptyChargeVolume = 0.1f;
        [SerializeField] private float emptyChargePitch = 0.5f;
        [SerializeField] private float fullChargeVolume = 1;
        [SerializeField] private float fullChargePitch = 1;

        private float chargeStartTime;

        public void StartCharging()
        {
            //Begin charging jump
            chargeStartTime = Time.time;

            //chargeSound.Play();
        }

        public void Jump()
        {
            //Jump!
            var ilerp = Mathf.InverseLerp(chargeStartTime, chargeStartTime + chargeMaxTime, Time.time);

            jumpSound.pitch = Mathf.Lerp(emptyChargePitch, fullChargePitch, ilerp);
            jumpSound.volume = Mathf.Lerp(emptyChargeVolume, fullChargeVolume, ilerp);

            jumpSound.Play();
        }
    }
}
