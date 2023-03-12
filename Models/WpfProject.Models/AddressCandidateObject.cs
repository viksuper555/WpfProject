using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProject.Models
{
    public class AddressCandidateObject
    {
        public Spatialreference spatialReference { get; set; }
        public Candidate[] candidates { get; set; }
    }

}