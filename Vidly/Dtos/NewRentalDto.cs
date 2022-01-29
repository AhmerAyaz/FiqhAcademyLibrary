using System.Collections.Generic;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        public string CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}