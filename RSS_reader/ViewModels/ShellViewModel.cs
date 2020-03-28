using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSS_reader.Model;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace RSS_reader.ViewModels
{
   public class ShellViewModel : Screen
    {
        MongoCRUD mongoCRUD = new MongoCRUD("BaseOfRssItems");
        Categories SimplyCategory = new Categories();
        string test="Internet";
       
      public BindableCollection<itemRSS> ItemRSSCollection { get; set; }
        public BindableCollection<Categories> categories { get; set; }
        public BindableCollection<itemRSS> NewItemRSSCollection { get; set; }

        public BindableCollection<itemRSS> ItemRSSToShow { get; set; }
        private Categories _selected;

        public Categories Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                test = value.category;
                NotifyOfPropertyChange(() => Selected);
            }
        }

        private itemRSS _secoundSelected;

        public itemRSS SecoundSelected
        {
            get { return _secoundSelected; }
            set { _secoundSelected = value;
                NotifyOfPropertyChange(() => SecoundSelected);
                
            }
        }


        public ShellViewModel()
        {
         
            
            ItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnAllRSSItems<itemRSS>("Collection"));
            categories = new BindableCollection<Categories>(mongoCRUD.returnOnlyAllCategoiresInMongoToList<Categories>("Collection"));
            NewItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnAllForOneCategory<itemRSS>("Collection", test));
         
        }
        public void NewCollection()
        {
           
            NewItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnAllForOneCategory<itemRSS>("Collection", test));

            Refresh();
            string taa = " ";
            //foreach (var item in NewItemRSSCollection)
            //{
            //    taa += item.Title;
            //}
            //System.IO.File.WriteAllText(@"C:\Users\Krute\OneDrive\Pulpit\TO.txt", taa);
        }
  

        //List<string> mediaChannels = new List<string>();
        //List<string> categoriesList = new List<string>();
        //List<string> valueOfCheckedCheckboxs = new List<string>();
        //MongoCRUD mongoCRUD = new MongoCRUD("BaseOfRssItems");

        //public BindableCollection<Categories> categories  { get; set; }
        //public BindableCollection<itemRSS> itemRSScategories  { get; set; }
        //public bool isChecked { get; set; }
        //private itemRSS _listOfCategories;
        //public itemRSS listOfCategories
        //{
        //    get => _listOfCategories;
        //    set
        //    {
        //        OnPropertyChange();
        //        _listOfCategories = value;

        //    }
        //}

        //private Categories _allcategory;
        //public Categories allcategory
        //{
        //    get => _allcategory;
        //    set
        //    {
        //        OnPropertyChange();
        //        _allcategory = value;

        //    }
        //}


        //private bool _checkboxofcategory;
        //public bool checkboxofcategory
        //{
        //    get => _checkboxofcategory;
        //    set
        //    {
        //        OnPropertyChange();
        //        _checkboxofcategory = value;
        //    }
        //}


        //public ShellViewModel()
        //  {

        //GetNewDataFromSite();
        //itemRSScategories = new BindableCollection<itemRSS>(mongoCRUD.returnAllRSSItems<itemRSS>("Collection"));
        //categories = new BindableCollection<Categories>(mongoCRUD.returnOnlyAllCategoiresInMongoToList<Categories>("Collection"));
        //checkboxofcategory = new RelayCommand(checkboxofcategoryHandler);

        //  }//tu dodawać nowe relay commands


        //public void checkboxofcategoryHandler(Object obj)
        //{
        //    checkboxofcategory = true;  
        //}

        //private void GetNewDataFromSite()
        //{
        //    var tagReader = new TagReader();
        //    tagReader.ReadTags();
        //    List<itemTag> items = new List<itemTag>();
        //    items = mongoCRUD.returnAllSources<itemTag>("Sources");

        //    var itemReader = new RssReader();
        //    itemReader.ReadItemsFromMultipleSources(items);
        //}
    }
}
