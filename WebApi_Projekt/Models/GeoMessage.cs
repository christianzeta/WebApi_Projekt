using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_Projekt.Models
{
    public class GeoMessage
    {
        int Id { get; set; }
        string Message { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
    }
}
