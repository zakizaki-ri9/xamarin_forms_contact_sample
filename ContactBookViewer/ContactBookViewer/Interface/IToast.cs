using System;
using System.Collections.ObjectModel;
using ContactBookViewer.Model;

namespace ContactBookViewer.Interface
{
    public interface IToast
    {
        /// <summary>
        /// トースト表示処理
        /// </summary>
        /// <param name="message">表示するメッセージ</param>
        void Show(string message);
    }
}
