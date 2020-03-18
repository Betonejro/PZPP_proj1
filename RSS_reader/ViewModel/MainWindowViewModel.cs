using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSS_reader.Model;

namespace RSS_reader.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        List<string> mediaChannels = new List<string>();
        //napisać metode która pobiera linki z tego kanału na media2/rss i dodaje do tej listy

        public MainWindowViewModel()
        {

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
