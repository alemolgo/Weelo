using AutoMapper;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Implements;
using co_weelo_testproject_dal.Interfaces;
using co_weelo_testproject_dal.ModelData;
using co_weelo_testproject_sl.Automapper;
using co_weelo_testproject_test.Fixtures;
using co_weelo_testproject_test.Fixtures.Utilities;
using NUnit.Framework;
using System.Text.Json;

namespace co_weelo_testproject_test.Data_Access_Layer
{
    public class OwnerDalTest
    {
        #region Properties
        IOwnerDal ownerDal;
        IMapper mapper;
        #endregion

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(mc =>
           {
               mc.AddProfile(new MappingProfileModule());
           });

            mapper = new Mapper(mapperConfig);
        }

        [TestCase("1", @".\Fixtures\JsonObjects\Owner_1.txt")]
        [TestCase("2", @".\Fixtures\JsonObjects\Owner_2.txt")]
        public void SearchById(int idOwner, string expextedObjectPath)
        {
            var expectedOwnerObject = JsonUtil.GetJsonObject<OwnerDto>(expextedObjectPath);
            WeeloDbContext weeloDbContext = new();
            WEELOContext context = weeloDbContext.CreateContext();

            using (context)
            {
                ownerDal = new OwnerDal(context, mapper);
                var searchedOwner = ownerDal.SearchById(idOwner);

                string generatedJsonString = JsonSerializer.Serialize(searchedOwner);
                string expectedJsonString = JsonSerializer.Serialize(expectedOwnerObject);

                bool currentCompare = generatedJsonString.Equals(expectedJsonString);
                Assert.AreEqual(currentCompare, true);
                context.Database.EnsureDeleted();
            }
        }
    }
}
