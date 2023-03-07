
namespace Jump
{
    public class AirController : IAirController
    {
        public bool IsJumping { get ; set; }
        public bool IsFalling { get; set; }
        public bool IsGrounding { get; set; }
    }
}
