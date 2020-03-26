using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSS_reader.Model;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace RSS_reader.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        
        List<string> mediaChannels = new List<string>();
        List<string> categoriesList = new List<string>();
        List<string> valueOfCheckedCheckboxs = new List<string>();
        MongoCRUD mongoCRUD = new MongoCRUD("BaseOfRssItems");

        public BindableCollection<Categories> categories  { get; set; }
        public BindableCollection<itemRSS> itemRSScategories  { get; set; }
        public bool isChecked { get; set; }
        private itemRSS _listOfCategories;
        public itemRSS listOfCategories
        {
            get => _listOfCategories;
            set
            {
                OnPropertyChange();
                _listOfCategories = value;
                
            }
        }

        private Categories _allcategory;
        public Categories allcategory
        {
            get => _allcategory;
            set
            {
                OnPropertyChange();
                _allcategory = value;

            }
        }


        private bool _checkboxofcategory;
        public bool checkboxofcategory
        {
            get => _checkboxofcategory;
            set
            {
                OnPropertyChange();
                _checkboxofcategory = value;
            }
        }


        public MainWindowViewModel()
        {

            //GetNewDataFromSite();
            itemRSScategories = new BindableCollection<itemRSS>(mongoCRUD.returnAllRSSItems<itemRSS>("Collection"));
            categories = new BindableCollection<Categories>(mongoCRUD.returnOnlyAllCategoiresInMongoToList<Categories>("Collection"));
            //checkboxofcategory = new RelayCommand(checkboxofcategoryHandler);

        }//tu dodawać nowe relay commands

                
        public void checkboxofcategoryHandler(Object obj)
        {
            checkboxofcategory = true;  
        }

        private void GetNewDataFromSite()
        {
            var tagReader = new TagReader();
            tagReader.ReadTags();
            List<itemTag> items = new List<itemTag>();
            items = mongoCRUD.returnAllSources<itemTag>("Sources");
            
            var itemReader = new RssReader();
            itemReader.ReadItemsFromMultipleSources(items);
        }
    }
}
