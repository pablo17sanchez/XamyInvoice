using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamInvoice.Model;

namespace XamInvoice.ViewModel
{
    public class InvoicesViewModel : INotifyPropertyChanged
    {
        bool busy;
        public bool IsBusy
        {

            get { return busy; }
            set
            {
                if (busy == value)
                    return;

                busy = value;
              //  OnPropertyChanged();
              //  OnPropertyChanged(nameof(Message));
            }
        }

        public void PrintStatus(string helloWorld)
        {
            if (helloWorld == null)
                throw new ArgumentNullException(nameof(helloWorld));

         //   WriteLine(helloWorld);
        }
        public ObservableCollection<Invoice> Invoices { get; } = new ObservableCollection<Invoice>();
        public string Message { get; set; } = "Hello World!";
        Command addInvoiceCommand = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command AddInvoiceCommand =>
                          addInvoiceCommand ?? (addInvoiceCommand = new Command(async () => await ExecuteAddInvoiceCommandAsync()));


        async Task ExecuteAddInvoiceCommandAsync()
        {
            double total = 0.0;
            try
            {
                IsBusy = true;
                // 1. Add camera logic.
                await CrossMedia.Current.Initialize();

                MediaFile photo;
                if (CrossMedia.Current.IsCameraAvailable)
                {
                    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Receipts",
                        Name = "Receipt"
                    });
                }
                else
                {
                    photo = await CrossMedia.Current.PickPhotoAsync();
                }

                if (photo == null)
                {
                    PrintStatus("Photo was null :(");
                    return;
                }

            
                // 2. Add  OCR logic.
               /*  OcrResults text;

                 var client = new VisionServiceClient("ebccaf8faed7407eb5b2108193d7b13a");

                 using (var stream = photo.GetStream())
                     text = await client.RecognizeTextAsync(stream);

                 var numbers = from region in text.Regions
                               from line in region.Lines
                               from word in line.Words
                               where word?.Text?.Contains("$") ?? false
                               select word.Text.Replace("$", string.Empty);*/


         /*        double temp = 0.0;
                 total = numbers?.Count() > 0 ?
                         numbers.Max(x => double.TryParse(x, out temp) ? temp : 0) :
                         0;



                 PrintStatus($"Found total {total.ToString("C")} " +
                     $"and we had {text.Regions.Count()} regions");*/


                 // 3. Add to data-bound collection.
             /*    Invoices.Add(new Invoice
                 {
                     Total = total,
                     Photo = photo.Path,
                     TimeStamp = DateTime.Now
                 });*/
             }
             catch (Exception ex)
             {
                 await (Application.Current?.MainPage?.DisplayAlert("Error",
                     $"Something bad happened: {ex.Message}", "OK") ??
                     Task.FromResult(true));

                 PrintStatus(string.Format("ERROR: {0}", ex.Message));

             }
             finally
             {
                 IsBusy = false;
             }
            }

    }
}
