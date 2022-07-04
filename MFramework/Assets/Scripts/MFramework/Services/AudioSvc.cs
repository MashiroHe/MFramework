/********************************************************************
    created: 2021/09/08
    filename: GameRoot.cs
    author:	 Mashiro Shiina
	e-mail address:1967407707@qq.com 
	date: 8:9:2021   19:36
	purpose: 音效服务管理类
*********************************************************************/

using MFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework 
{
    public class AudioSvc : MonoSingleton<AudioSvc>
    {
        private AudioSource bgSource;
        private AudioSource effectsUISource;
        private AudioSource UISource;
        private AudioSource playerSource;
        public void InitMethod()
        {
            bgSource = gameObject.AddComponent<AudioSource>();
            bgSource.playOnAwake = false;
            bgSource.loop = true;
            effectsUISource = gameObject.AddComponent<AudioSource>();
            UISource = gameObject.AddComponent<AudioSource>();
            playerSource = gameObject.AddComponent<AudioSource>();
            gameObject.AddComponent<AudioListener>();
        }
        //播放背景音乐
        public void PlayBGAudio(string name, bool isLoop = true)
        {
            AudioClip clip = LoaderManager.Instance.LoadAudioSource("ResAudio/" + name, true);
            if (bgSource.clip == null || bgSource.clip.name != clip.name)
            {
                bgSource.clip = clip;
                bgSource.loop = isLoop;
                bgSource.Play();
                bgSource.volume = 0.3f;
                if (name == Const.bgCarbon)
                {
                    bgSource.volume = 0.1f;
                }
            }
        }
        public void StopBGAduio()
        {
            if (bgSource.isPlaying)
            {
                bgSource.Stop();
            }
        }
        //播放音效
        public void PlayUIAudio(string name)
        {
            AudioClip clip = LoaderManager.Instance.LoadAudioSource("ResAudio/" + name, true);
            effectsUISource.clip = clip;
            effectsUISource.Play();
        }
        public void PlayBattleAudio(string name)
        {
            AudioClip clip = LoaderManager.Instance.LoadAudioSource("ResAudio/" + name, true);
            UISource.clip = clip;
            UISource.Play();
        }
        public void PlayBattleAudio(string name, AudioSource source)
        {
            if (source.gameObject.activeSelf == false) return;
            AudioClip clip = LoaderManager.Instance.LoadAudioSource("ResAudio/" + name, true);
            source.clip = clip;
            source.Play();
        }
        public void PlayStepAudio(string name)
        {
            AudioClip clip = LoaderManager.Instance.LoadAudioSource("ResAudio/" + name, true);
            playerSource.clip = clip;
            if (playerSource.isPlaying)
            {
                return;
            }
            else
            {
                playerSource.Play();
            }
        }
        public void StopStepAduio()
        {
            if (playerSource.isPlaying)
            {
                playerSource.Stop();
            }
        }
    }
}

