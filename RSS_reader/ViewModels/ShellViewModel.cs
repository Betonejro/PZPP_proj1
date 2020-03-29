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
        string test="";
        string guid = "";



      public BindableCollection<itemRSS> ItemRSSCollection { get; set; }
        public BindableCollection<Categories> categories { get; set; }
        public BindableCollection<itemRSS> NewItemRSSCollection { get; set; }

        public BindableCollection<itemRSS> ItemRSSToShow { get; set; }
        public List<Categories> ALotOFSelectedCategories = new List<Categories>();
        public List<string> ALotOFSelectedCategoriesInStringList= new List<string>();
        public BindableCollection<Categories> ALotOFSelectedCategoriesSupportCollection { get; set; }
        private Categories _selected;
        private itemRSS _guidLink;

        public itemRSS GuidLink
        {
            get { return _guidLink; }
            set
            {
                _guidLink = value;
                if(value == null)
                {

                }
                else
                guid = value.Guid;
                NotifyOfPropertyChange(() => GuidLink);

            }
        }


        public Categories Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                test = value.category;
                ALotOFSelectedCategories.Add(new Categories { category=value.category });
                ALotOFSelectedCategoriesInStringList.Add(value.category);
                if (value == null)
                {

                }
                else
                    ALotOFSelectedCategoriesSupportCollection.Add(new Categories { category = value.category });



                NotifyOfPropertyChange(() => Selected);
            }
        }

        private itemRSS _secoundSelected;

        public itemRSS SecoundSelected
        {
            get { return _secoundSelected; }
            set { 
                _secoundSelected = value;
                NotifyOfPropertyChange(() => SecoundSelected);
                
            }
        }
       

        public ShellViewModel()
        {

            ALotOFSelectedCategoriesSupportCollection = new BindableCollection<Categories>(ALotOFSelectedCategories);
            ItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnAllRSSItems<itemRSS>("Collection"));
            categories = new BindableCollection<Categories>(mongoCRUD.returnOnlyAllCategoiresInMongoToList<Categories>("Collection"));
            NewItemRSSCollection = new BindableCollection<itemRSS>();
          //  GetNewDataFromSite();


        }
      
        public void NewCollection()
        {

            //NewItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnAllForOneCategory<itemRSS>("Collection", test));
            NewItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnItemRSSFindedByCategory<itemRSS>("Collection", ALotOFSelectedCategoriesInStringList));
            Refresh();
         



        }
        public void OpenWebSite()
        {
            System.Diagnostics.Process.Start(guid);
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

        public void RestartCategories()
        {
            test = "";
            NewItemRSSCollection.Clear();
            ALotOFSelectedCategoriesSupportCollection.Clear();
            ALotOFSelectedCategories.Clear();
            Refresh();


        }


    }
}
