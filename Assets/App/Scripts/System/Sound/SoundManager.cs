#define ENABLE_UTILITY

using UnityEngine;

namespace App
{
    public class SoundManager : MonoBehaviour
    {

        [System.Serializable]
        public class sBGMChannel
        {
            public AudioSource source;
            public float masterVolume;
        }

        [SerializeField] public sBGMChannel[] bgmChannels = null;
        [SerializeField] AudioSource[] seSources = null;
        [SerializeField] AudioSource voiceSource = null;
        [SerializeField] AudioSource jingleSource = null;

        static SoundManager s_instance = null;

        public static SoundManager Instance {
            get { return s_instance; }
        }

        [SerializeField] private AudioClip[] bgmList = null;
        [SerializeField] private AudioClip[] jingleList = null;
        [SerializeField] private AudioClip[] seList = null;
        [SerializeField] private AudioClip[] voiceList = null;

        public double fadeTime = 1.0;

        double fadeDeltaTime = 0;
        float aimVolume = 0.0f;
        bool isFadeLeap = false;
        int idxSESource = 0;

        App.SoundList.BGM curBGM = App.SoundList.BGM.NONE;


        public int GetMax_BGMList()
        {
            return bgmList.Length;
        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Awake()
        {
            if (s_instance == null) {
                s_instance = this;
                voiceSource.clip = null;
                DontDestroyOnLoad(gameObject);
                foreach (var item in seSources) {
                    item.clip = null;
                }

                foreach (var item in bgmChannels) {
                    item.source.clip = null;
                }
            }
            else if (s_instance != this) {
                Destroy(gameObject);
                return;
            }

            idxSESource = 0;
            fadeDeltaTime = 0.0;
        }

        /// <summary>
        /// ゲームステートによってBGMを変えたい場合...
        /// </summary>
        void Update()
        {
            if (isFadeLeap == true) {
                fadeDeltaTime += Time.deltaTime;
                if (fadeTime > 0.0) {
                    //foreach (var bgmch in bgmChannels) {
                    for (int i = 0; i < bgmChannels.Length; i++) {
                        var bgmch = bgmChannels[i];
                        float rate =
                            ((aimVolume > bgmch.source.volume) ? (float)fadeDeltaTime : (float)(1.0 - fadeDeltaTime)) /
                            (float)fadeTime;
                        float volm = bgmch.masterVolume * rate;
                        if (volm > 1.0f) volm = 1.0f;
                        bgmch.source.volume = volm;
                    }
                }

                if (fadeDeltaTime >= fadeTime || fadeTime <= 0.0) {
                    isFadeLeap = false;
                    fadeDeltaTime = 0.0f;
#if true
                    foreach (var bgmch in bgmChannels) {
                        if (aimVolume <= 0.0f) {
                            bgmch.source.Stop();
                            bgmch.source.clip = null;
                            for (int i = 1; i < bgmChannels.Length; i++) {
                                var chan = bgmChannels[i];
                                chan.masterVolume = 0;
                            }
                        }
                    }
#endif
                }
            }
        }

        public async void FadeInBGM(float time = 1.0f)
        {
            fadeTime = time;
            aimVolume = 1.0f;
            foreach (var bgmch in bgmChannels) {
                bgmch.source.volume = 0.0f;
            }

            isFadeLeap = true;
        }

        public void FadeOutBGM(float time = 1.0f)
        {
            fadeTime = time;
            aimVolume = 0.0f;
            isFadeLeap = true;
            curBGM = App.SoundList.BGM.NONE;
        }

        public async void PlayBGM(SoundList.BGM bgmNo, bool isfadein = false)
        {
            if (curBGM == bgmNo) {
                // すでに鳴っているBGMならばリスタートしないでスキップ 
                return;
            }

            foreach (var bgmch in bgmChannels) {
                if (bgmch.source.isPlaying) {
                    bgmch.source.volume = 0.0f;
                    bgmch.source.Stop();
                    curBGM = App.SoundList.BGM.NONE;
                }
            }

            if (bgmChannels.Length > 0) {
                var bgmch = bgmChannels[0];
                if (bgmList.Length > (int)bgmNo) {
                    var audioItem = bgmList[(int)bgmNo];
                    if (audioItem) {
                        bgmch.source.clip = audioItem;
                        bgmch.source.loop = true;

                        if (isfadein == true || isFadeLeap == true) {
                            FadeInBGM();
                        }
                        else {
                            bgmch.source.volume = bgmch.masterVolume;
                        }

                        bgmch.source.Play();
                        curBGM = bgmNo;
                    }
                }
            }
        }

        public App.SoundList.BGM CurrentBGM {
            get { return curBGM; }
        }

        public async void PlayBGMMulti(params SoundList.BGM[] args)
        {
            foreach (var bgmch in bgmChannels) {
                if (bgmch.source.isPlaying) {
                    bgmch.source.volume = 0.0f;
                    bgmch.source.Stop();
                    curBGM = App.SoundList.BGM.NONE;
                }
            }

            for (int i = 0; i < args.Length; i++) {
                var bgmreq = args[i];
                if (bgmChannels.Length <= 0) {
                    break;
                }

                var bgmch = bgmChannels[i];
                if (bgmList.Length > (int)bgmreq
                    && bgmList[(int)bgmreq] != null
                   ) {
                    var audioItem = bgmList[(int)bgmreq];
                    if (audioItem != null) {
                        bgmch.source.clip = audioItem;
                        bgmch.source.loop = true;
                        bgmch.source.volume = 0;
                        FadeInBGM();
                        bgmch.source.Play();
                    }
                }
            }
        }

        public async void PlayJingle(SoundList.JINGLE no)
        {
            if (jingleSource.isPlaying) {
                jingleSource.volume = 0.0f;
                jingleSource.Stop();
            }

            if (jingleList.Length > 0 && jingleList.Length > (int)no) {
                var audioItem = jingleList[(int)no];
                if (audioItem != null) {
                    jingleSource.clip = audioItem;
                    jingleSource.loop = false;
                    jingleSource.volume = 1.0f;
                    jingleSource.Play();
                }
            }
        }

        public void PlaySE(SoundList.SE no)
        {
            if (++idxSESource >= seSources.Length) {
                idxSESource = 0;
            }

            var curItem = seSources[idxSESource];
            if (curItem.isPlaying) {
                curItem.Stop();
            }

            if (seList.Length > 0 && seList.Length > (int)no) {
                var audioItem = seList[(int)no];
                if (audioItem != null) {
                    curItem.clip = audioItem;
                    curItem.Play();
                }
            }
        }

        public void StopAllSE()
        {
            foreach (var item in seSources) {
                if (item.isPlaying) {
                    item.Stop();
                }
            }
        }

        public async void PlayVoice(SoundList.VOICE no)
        {
            if (voiceSource.isPlaying) {
                voiceSource.volume = 0.0f;
                voiceSource.Stop();
            }

            if (voiceList.Length > 0 && voiceList.Length > (int)no) {
                var audioItem = voiceList[(int)no];
                if (audioItem != null) {
                    voiceSource.clip = audioItem;
                    voiceSource.loop = false;
                    voiceSource.volume = 1.0f;
                    voiceSource.Play();
                }
            }
        }
    }
}
