using System;
namespace ServiceApplication.Dto
{
	public class ElevatorDto:BaseDto
	{
		public ElevatorDto()
		{
		}
        public string Code { get; set; }
        public int LastFloor { get; set; }
		public string Status { get; set; }
	}
}

