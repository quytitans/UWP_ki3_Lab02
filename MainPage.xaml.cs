using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NotePadApp01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OpenFileClick(object sender, RoutedEventArgs e)
        {
            var piker = new FileOpenPicker();
            piker.ViewMode = PickerViewMode.List;
            piker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            piker.FileTypeFilter.Add(".txt");
            piker.FileTypeFilter.Add("*");

            StorageFile file = await piker.PickSingleFileAsync();
            if(file != null && file.ContentType == "text/plain")
            {
                var stringContent = await FileIO.ReadTextAsync(file);
                mainContent.Text = stringContent;
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Warning";
                contentDialog.Content = "Some thing went wrong, please try again";
                contentDialog.CloseButtonText = "Confirm";
            }

        }

        private async void SaveAsClick(object sender, RoutedEventArgs e)
        {
            loadingIcon(true);
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "New Document";
            var contentToSave = mainContent.Text;
            StorageFile file2 = await savePicker.PickSaveFileAsync();
            await FileIO.WriteTextAsync(file2, contentToSave);
            loadingIcon(false);
        }

        private void loadingIcon(bool check)
        {
            progressRing.IsActive = check;
        }
    }
}
