
using System.Collections.ObjectModel;
using ContactBookViewer.Model;

namespace ContactBookViewer.DependencyService
{
    public interface IContact
    {
        /// <summary>
        /// アドレス帳取得
        /// </summary>
        /// <returns>
        /// <para>ローカルに保存されたアドレス帳から</para>
        /// <para>Contactクラスのプロパティ分の情報のみを抜き出した</para>
        /// <para>リストを返却する</para>
        /// </returns>
        ObservableCollection<Contact> GetContactList();
    }
}
