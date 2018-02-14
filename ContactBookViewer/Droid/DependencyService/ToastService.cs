using System;
using System.Collections.ObjectModel;
using System.IO;

using ContactBookViewer.DependencyService;
using ContactBookViewer.Model;
using ContactBookViewer.Droid.DependencyService;

using Android.Widget;

using Xamarin.Forms;

[assembly: Dependency(typeof(ToastService))]
namespace ContactBookViewer.Droid.DependencyService
{
    public class ToastService : IToast
    {
        public void Show(string message)
        {
            Toast.MakeText(
                Android.App.Application.Context,
                message,
                ToastLength.Short).Show();
        }
    }
}
