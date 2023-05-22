using System;
using Domain.Entities;
using Domain.Port;
using Infrastructure.Repository;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Infrastructure;
using MongoDB.Driver;
using System.Collections.Generic;
using Util.Common;
using System.Linq;

namespace UnitMSTest.Services
{
    [TestClass]
    public class ElevatorMovementRepositoryTest
	{
        public List<ElevatorMovement> ElevatorsMovements { get; set; }
        public Mock<IMainContextCosmos> _mockMainContext = new();
        public Mock<IConfigurateCosmosDB> _mockConfigurate = new();
        public Paginate<ElevatorMovement> PaginateObj { get; set; }
        private Mock<IMongoCollection<ElevatorMovement>> _elevatorCollection;

        public ElevatorMovementRepositoryTest()
		{
            ElevatorsMovements = new List<ElevatorMovement>()
            {
                new ElevatorMovement(10,1,"52cd71ce-cc3e-45eb-a262-f6e123ff8daa","52cd71ce-cc3e-45eb-a262-f6e123ff8dae"),
                new ElevatorMovement(10,2,"52cd71ce-cc3e-45eb-a262-f6e123ff8dae","52cd71ce-cc3e-45eb-a262-f6e123ff8daa"),
            };

            PaginateObj = new Paginate<ElevatorMovement>()
            {
                Count = 2,
                Data = ElevatorsMovements,
                Operator = (LogicalOperators)1,
                Page = 1,
                PagesTotal = 1,
                RowsTotal = 2,
                FiltersPaginate = null
            };

            _elevatorCollection = new Mock<IMongoCollection<ElevatorMovement>>();
            _mockConfigurate.Setup(s => s.ConnectionString).Returns("connect");
            _mockConfigurate.Setup(s => s.DatabaseName).Returns("dbname");
            _mockMainContext = new Mock<IMainContextCosmos>();
            _mockMainContext.Setup(s => s.GetCollection<ElevatorMovement>("ElevatorMovement")).Returns(_elevatorCollection.Object);
        }

        [TestMethod]
        public async Task CreateTest()
        {
            Mock<IRepositoryBase<ElevatorMovement>> _mockRepositoryBase = new Mock<IRepositoryBase<ElevatorMovement>>();

            _mockRepositoryBase.Setup(s => s.CreateModel(ElevatorsMovements.FirstOrDefault())).ReturnsAsync(ElevatorsMovements.FirstOrDefault());

            IElevatorMovementRepository elevatorRepository = new ElevatorMovementRepository(_mockMainContext.Object);

            var result = await elevatorRepository.CreateModel(ElevatorsMovements.FirstOrDefault());

            Assert.AreEqual(ElevatorsMovements.FirstOrDefault(), result);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            Mock<IRepositoryBase<ElevatorMovement>> _mockRepositoryBase = new Mock<IRepositoryBase<ElevatorMovement>>();

            _mockRepositoryBase.Setup(s => s.UpdateModel(ElevatorsMovements.FirstOrDefault())).ReturnsAsync(ElevatorsMovements.FirstOrDefault());

            IElevatorMovementRepository elevatorRepository = new ElevatorMovementRepository(_mockMainContext.Object);

            var result = await elevatorRepository.CreateModel(ElevatorsMovements.FirstOrDefault());

            Assert.AreEqual(ElevatorsMovements.FirstOrDefault(), result);
        }

        [TestMethod]
        public async Task DeleteTest()
        {

            Mock<IRepositoryBase<ElevatorMovement>> _mockRepositoryBase = new Mock<IRepositoryBase<ElevatorMovement>>();

            _mockRepositoryBase.Setup(s => s.DeleteModel("Status", "Active")).ReturnsAsync(true);

            IElevatorMovementRepository elevatorRepository = new ElevatorMovementRepository(_mockMainContext.Object);

            var result = await elevatorRepository.DeleteModel("Status", "Active");

            Assert.IsTrue(result);
        }
    }
}

