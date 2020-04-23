using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Extensions
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.label = label;
            this.Y = y;
        }

        [DataMember(Name = "label")]
        public string label = "";


        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}
