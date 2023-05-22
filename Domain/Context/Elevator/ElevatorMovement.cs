using System;
using Domain.Common;

namespace Domain.Entities
{
	public class ElevatorMovement: BaseEntity
    {
		public ElevatorMovement(int floor,int priority,string code,string elevatorCode)
		{
            Code = code;
            Floor = floor;
            Priority = priority;
            ElevatorCode = elevatorCode;
		}
        public ElevatorMovement()
        {

        }
        public string Code  { get; private set; }
        public int Floor { get; private set; }
        public int Priority { get; private set; }
        public string ElevatorCode { get; private set; }
    }
}

