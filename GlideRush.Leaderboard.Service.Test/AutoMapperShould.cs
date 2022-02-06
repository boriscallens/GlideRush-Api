using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace GlideRush.Leaderboard.Service.Test
{
    public class AutoMapperShould: IClassFixture<UnitTestFixture>
    {
        private readonly IMapper _mapper;

        public AutoMapperShould(UnitTestFixture fixture)
        {
            _mapper = fixture.GetRequiredService<IMapper>();
        }

        [Fact]
        public void HaveAValidConfiguration()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}