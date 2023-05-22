using System;
namespace ServiceApplication.Dto
{
	public class ElevatorMovementDto: BaseDto
    {
		public ElevatorMovementDto()
		{
		}
        public string Code { get; set; }
        public int Floor { get; set; }
        public int Priority { get;  set; }
        public string Status { get; set; }
        public string ElevatorCode { get; set; }
    }
}

