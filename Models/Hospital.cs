using System.Numerics;

namespace HspMgmt_asgmt.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
