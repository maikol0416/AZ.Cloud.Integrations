using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Port;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using Moq;
using Util.Common;
using static System.Net.Mime.MediaTypeNames;

namespace UnitMSTest.Services
{
    [TestClass]
    public class ElevatorRepositoryTest
	{
        public List<Elevator> Elevators { get; set; }
        private Mock<IMongoCollection<Elevator>> _elevatorCollection;
        public Mock<IMainContextCosmos> _mockMainContext = new();
        public Mock<IConfigurateCosmosDB> _mockConfigurate = new();
        public Paginate<Elevator> PaginateObj { get; set; }

        public ElevatorRepositoryTest()
		{
            Elevators = new List<Elevator>()
            {
                new Elevator()
                {
                    Code="52cd71ce-cc3e-45eb-a262-f6e123ff8dae",
                    LastFloor=10,
                    Id=""
                },
                new Elevator()
                {
                    Code="52cd71ce-cc3e-45eb-a262-f6e123ff8daa",
                    LastFloor=2,
                    Id=""
                }


            };

            PaginateObj = new Paginate<Elevator>()
            {
                Count = 2,
                Data = Elevators,
                Operator = (LogicalOperators)1,
                Page = 1,
                PagesTotal = 1,
                RowsTotal = 2,
                FiltersPaginate = null
            };


            _elevatorCollection = new Mock<IMongoCollection<Elevator>>();
            _mockConfigurate.Setup(s => s.ConnectionString).Returns("connect");
            _mockConfigurate.Setup(s => s.DatabaseName).Returns("dbname");
            _mockMainContext = new Mock<IMainContextCosmos>();
            _mockMainContext.Setup(s => s.GetCollection<Elevator>("Elevator")).Returns(_elevatorCollection.Object);
        }

        [TestMethod]
        public async Task CreateTest()
        {
            
            Mock<IRepositoryBase<Elevator>> _mockRepositoryBase = new Mock<IRepositoryBase<Elevator>>();

            _mockRepositoryBase.Setup(s => s.CreateModel(Elevators.FirstOrDefault())).ReturnsAsync(Elevators.FirstOrDefault());

            IElevatorRepository elevatorRepository = new ElevatorRepository(_mockMainContext.Object);

            var result = await elevatorRepository.CreateModel(Elevators.FirstOrDefault());

            Assert.AreEqual(Elevators.FirstOrDefault(), result);
        }

        [TestMethod]
        public async Task UpdateTest()
        {

            Mock<IRepositoryBase<Elevator>> _mockRepositoryBase = new Mock<IRepositoryBase<Elevator>>();

            _mockRepositoryBase.Setup(s => s.UpdateModel(Elevators.FirstOrDefault())).ReturnsAsync(Elevators.FirstOrDefault());

            IElevatorRepository elevatorRepository = new ElevatorRepository(_mockMainContext.Object);

            var result = await elevatorRepository.CreateModel(Elevators.FirstOrDefault());

            Assert.AreEqual(Elevators.FirstOrDefault(), result);
        }

        [TestMethod]
        public async Task DeleteTest()
        {

            Mock<IRepositoryBase<Elevator>> _mockRepositoryBase = new Mock<IRepositoryBase<Elevator>>();

            _mockRepositoryBase.Setup(s => s.DeleteModel("Status","Active")).ReturnsAsync(true);

            IElevatorRepository elevatorRepository = new ElevatorRepository(_mockMainContext.Object);

            var result = await elevatorRepository.DeleteModel("Status", "Active");

            Assert.IsTrue(result);
        }

    }
}

