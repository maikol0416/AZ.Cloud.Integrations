using System;
using Domain.Common;

namespace Domain.Entities
{
	public class Elevator :BaseEntity
	{
		public Elevator(string code,int lastFloor)
		{
			Code = code;
			LastFloor = lastFloor;
		}

        public Elevator()
        {

        }
        public string Code { get; set; }
		public int LastFloor { get; set; }
	}
}

