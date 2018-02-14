using System.Collections.ObjectModel;

namespace ContactBookViewer.Model
{
    /// <summary>
    /// アドレス帳クラス
    /// </summary>
    public class Contact
    {
        private string _name;
        private string _kana;
        private string _address;
        private ObservableCollection<string> _tel;
        private ObservableCollection<string> _email;

        public string Name { get => _name; set => _name = value; }
        public string Kana { get => _kana; set => _kana = value; }
        public string Address { get => _address; set => _address = value; }
        public ObservableCollection<string> Tel { get => _tel; set => _tel = value; }
        public ObservableCollection<string> Email { get => _email; set => _email = value; }
    }
}
