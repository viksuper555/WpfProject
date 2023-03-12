namespace WpfProject.Models
{
    public class Candidate
    {
        public string address { get; set; }
        public Location location { get; set; }
        public float score { get; set; }
        public Attributes attributes { get; set; }
        public Extent extent { get; set; }
    }

}
