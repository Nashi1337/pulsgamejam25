using System;
using Player.Arms;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Sound
{
    static class SoundPitcher
    {
        public static void PitchRandom(AudioSource audioSource, float minrange, float maxrange)
        {
            float pitch = UnityEngine.Random.Range(minrange, maxrange);
            audioSource.pitch = pitch;
        }
    }
}