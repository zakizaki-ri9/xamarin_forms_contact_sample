using System;
namespace ContactBookViewer.Model
{
    /// <summary>
    /// 連絡帳クラス
    /// </summary>
    public class Contact
    {
        private string _name;
        private string _kana;
        private string _address;
        private string _tel;
        private string _email;

        public string Name { get => _name; set => _name = value; }
        public string Kana { get => _kana; set => _kana = value; }
        public string Address { get => _address; set => _address = value; }
        public string Tel { get => _tel; set => _tel = value; }
        public string Email { get => _email; set => _email = value; }

        /// <summary>
        /// CSV(カンマ区切り)形式のToStringメソッド
        /// </summary>
        /// <returns>カンマ区切りの文字列</returns>
        public string ToStringCsv()
        {
            return this.Name + "," +
                        this.Kana + "," +
                        this.Address + "," +
                        this.Tel + "," +
                        this.Email;
        }
    }
}
