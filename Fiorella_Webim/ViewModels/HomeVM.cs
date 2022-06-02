using Fiorella_Webim.Models;
using System.Collections.Generic;

namespace Fiorella_Webim.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> sliders { get; set; }
        public PageIntro pageIntro { get; set; }

        public IEnumerable<Category> categories { get; set; }
    }
}
