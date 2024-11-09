namespace App
{
    /// ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
    /// 全ゲームデータ
    /// ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
    [System.Serializable]
    public class GameData
    {
        /// <summary>
        /// ユーザーデータ
        /// </summary>
        public User dataUser = new User();

        /// <summary>
        /// 設定
        /// </summary>
        public Setting setting = new Setting();

        /// <summary>
        /// コピー
        /// </summary>
        public void CopyFrom(GameData other)
        {
            dataUser.CopyFrom(other.dataUser);
            setting.CopyFrom(other.setting);
        }
    }
    
}