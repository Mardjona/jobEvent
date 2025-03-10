using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jobEvent
{
    internal class EventItems
    {
        public int Id { get; set; }
        public string titlee { get; set; }
        public string descriptionn { get; set; }
        public string director { get; set; }

        public string date { get; set; }

        public EventItems(int v1, string v2, string v3, string v4, string v5)
        {
            this.Id = v1;
            this.titlee = v2;
            this.descriptionn = v3;
            this.director = v4;
            this.date = v5;
        }

       
    }
}
