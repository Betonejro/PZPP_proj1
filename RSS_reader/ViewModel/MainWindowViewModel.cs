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
           
        public BindableCollection<itemRSS> itemsRss { get; set; }

        
        //public string Test2
        //{
        //    get => _test2;
        //    set
        //    {
        //        _test2 = value;
        //        OnPropertyChange();
        //    }
        //}
        //private string _test2;
       
          
        
       
        public MainWindowViewModel()
        {

            
            MongoCRUD mongoCRUD = new MongoCRUD("BazaTestowa");
            itemsRss = new BindableCollection<itemRSS>(mongoCRUD.returnAllRSSItems<itemRSS>("BazaTestowa"));


            var tagReader = new TagReader();
            tagReader.ReadTags();
        }//tu dodawać nowe relay commands

                

















        public void ReadMediaChannels()
        {
            var Tagger = new TagReader();
            foreach (var sources in Tagger.Tags)
            {
                mediaChannels.Add(sources.Href);
            }
        }

        public void SaveItemsFromChannelsToDatabase()
        {
            var Reader = new RssReader();

            Reader.ReadItemsFromMultipleSources(mediaChannels);
        }


    }
}
