namespace therabia.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Permission { get; set; }
        public string Note { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
