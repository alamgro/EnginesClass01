namespace Jump
{
    public interface IAirController
    {
        public bool IsJumping { get; set; }
        public bool IsFalling { get; set; }
        public bool IsGrounding { get; set; }
    }
}
