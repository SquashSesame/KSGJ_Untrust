namespace App
{
    public static class SoundList
    {
        /*
            SoundManager のBGM/Jingle/SEのリスト登録順にあわせて定義を追加する。 
        */
        public enum BGM {
            NONE,
            TITLE,
            STAGE01,
            STAGE02,
            //
            MAX,
        }

        public enum JINGLE {
            NONE,
            //
            MAX,
        }

        public enum SE {
            NONE,
            GAME_START,
            OPEN_TAB,
            LEVEL_UP,
            //
            MAX,
        }

        public enum VOICE {
            NONE,
        }
    }

}
