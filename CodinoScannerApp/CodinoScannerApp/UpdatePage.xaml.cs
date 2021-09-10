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
        }



        protected override bool OnBackButtonPressed()
        {
            var pages = Navigation.NavigationStack.ToList();
            Navigation.RemovePage(pages[1]);
            return base.OnBackButtonPressed();
        }
    }
}