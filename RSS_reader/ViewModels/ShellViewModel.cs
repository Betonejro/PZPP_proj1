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
            GetNewDataFromSite();


        }
      
        public void NewCollection()
        {

            //NewItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnAllForOneCategory<itemRSS>("Collection", test));
            var returnedItems = mongoCRUD.returnItemRSSFindedByCategory<itemRSS>("Collection", ALotOFSelectedCategoriesInStringList);
            foreach (var item in returnedItems)
            {
                item.PubDate = item.GetDatatime().ToString();
                item.Description = item.GetDescriptionText();
            }
            //NewItemRSSCollection = new BindableCollection<itemRSS>(mongoCRUD.returnItemRSSFindedByCategory<itemRSS>("Collection", ALotOFSelectedCategoriesInStringList));
            NewItemRSSCollection = new BindableCollection<itemRSS>(returnedItems);
            Refresh();
         



        }
        public void OpenWebSite()
        {
            try { 
            System.Diagnostics.Process.Start(guid);
                }
            catch (Exception)
            {

            }
        }
        private void GetNewDataFromSite()
        {
            var tagReader = new TagReader();
            tagReader.ReadTags();
            List<itemTag> items = new List<itemTag>();
            items = mongoCRUD.returnAllSources<itemTag>("Sources");
            var sources = GetXRandomItemsFromList(5, items);
            var itemReader = new RssReader();
            itemReader.ReadItemsFromMultipleSources(sources);
        }

        private List<itemTag> GetXRandomItemsFromList(int count, List<itemTag> items)
        {
            if (count > 0 && count <= items.Count)
            {
                List<itemTag> newList = new List<itemTag>();

                newList = items.OrderBy(x => Guid.NewGuid()).Take(count).ToList();

                return newList;
            }
            else
            {
                throw new Exception("GetXRandomitemsFromList wrong count !");
            }
        }

        public void RestartCategories()
        {
            test = "";
            NewItemRSSCollection.Clear();
            ALotOFSelectedCategoriesSupportCollection.Clear();
            ALotOFSelectedCategories.Clear();
            Refresh();


        }

        public void OpenThisSite()
        {
            System.Diagnostics.Process.Start(guid);
        }


    }
}
