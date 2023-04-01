using ReservationFlight.Model.Catalog.Aviations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Common
{
    public class PatchDocumentRequest
    {
        public string Path { get; set; }
        public string Op { get; set; }
        public List<AviationUpdateRequest> Value { get; set; }
    }
}
