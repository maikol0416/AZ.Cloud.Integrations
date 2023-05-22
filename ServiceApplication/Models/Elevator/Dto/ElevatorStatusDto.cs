using System;
using System.Collections.Generic;

namespace ServiceApplication.Dto
{
	public class ElevatorStatusDto
	{
		public ElevatorStatusDto()
		{
		}

		public string CodeElevtor { get; set; }

        public List<ElevatorMovementDto> MovementHP { get; set; }

        public List<ElevatorMovementDto> MovementLP { get; set; }

        public int FloorActual { get; set; }

        public string Status { get; set; }
    }
}

