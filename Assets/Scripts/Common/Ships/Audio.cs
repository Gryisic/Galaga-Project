using System.Collections.Generic;
using UnityEngine;

namespace Common.Ships
{
    public class Audio
    {
        private readonly AudioSource _source;
        private IReadOnlyList<AudioClip> _clips;

        public Audio(AudioSource source)
        {
            _source = source;
        }

        public void UpdateClips(IReadOnlyList<AudioClip> clips) => _clips = clips;

        public void PlayRandom()
        {
            AudioClip clip = _clips[Random.Range(0, _clips.Count)];

            _source.clip = clip;
            
            _source.Play();
        }
    }
}