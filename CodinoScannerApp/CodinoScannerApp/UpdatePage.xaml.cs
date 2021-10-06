using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodinoScannerApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodinoScannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePage : ContentPage
    {
        private IRepositoryInterface _repo;
        private string _docID;
        private Document _currDoc;
        public UpdatePage(IRepositoryInterface repo, string docID)
        {
            InitializeComponent();
            _repo = repo;
            _docID = docID;
        }

        public void PrepareWindow(Document doc)
        {
            _currDoc = doc;
            tbxDocumentName.Text = _currDoc.DocumentName;
            tbxDocumentDescription.Text = _currDoc.DocumentDescription;
            var isHome = _currDoc.DocumentLocation.Equals("Home");
            if (isHome)
            {
                rbHome.IsChecked = true;
            }
            else
            {
                rbAway.IsChecked = true;
            }
        }
        

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            //do validation of some kind?
            if (!string.IsNullOrWhiteSpace(tbxDocumentDescription.Text) &&
                !string.IsNullOrWhiteSpace(tbxDocumentName.Text))
            {
                //update
                _currDoc.DocumentName = tbxDocumentName.Text;
                _currDoc.DocumentDescription = tbxDocumentDescription.Text;
                if (rbHome.IsChecked)
                {
                    _currDoc.DocumentLocation = "Home";
                }
                else
                {
                    _currDoc.DocumentLocation = "Away";
                }


                await _repo.UpdateDocument(_currDoc);
                await DisplayAlert("Success", "Document Successfully Updated", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Invalid Document Name or Description", "Ok");
            }
        }



        protected override bool OnBackButtonPressed()
        {
            var pages = Navigation.NavigationStack.ToList();
            Navigation.RemovePage(pages[2]);
            return base.OnBackButtonPressed();
        }
    }
}