using System;
using System.Collections.ObjectModel;
using ContactBookViewer.Model;

namespace ContactBookViewer.Interface
{
    public interface IContactOperation
    {
        ///// <summary>
        ///// アドレス帳エクスポート
        ///// </summary>
        ///// <param name="list">出力対象のContactリスト</param>
        ///// <returns>エクスポートしたパス、出力していない場合はstring.Emptyが返される</returns>
        ///// <remarks>
        ///// <para>Android,iOSそれぞれのユーザデータ保存フォルダへ</para>
        ///// <para>"yyyyMMddHHmmssfff.txt"で出力する</para>
        ///// </remarks>
        //string Export(ObservableCollection<Contact> list);

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
