namespace therabia.DTO
{
    public class DiscountDto
    {
        public string Code { get; set; } = string.Empty;
        public int Percent { get; set; }
        public bool Disable { get; set; }
    }
}
