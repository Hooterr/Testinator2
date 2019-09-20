using AutoMapper;
using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The mapper for every model related to user
    /// </summary>
    public class UserMapper
    {
        #region Private Members

        /// <summary>
        /// AutoMapper configuration for this mapepr
        /// </summary>
        private readonly IMapper mMapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// All the mapper configuration is applied here
        /// </summary>
        public UserMapper()
        {
            // Configure the AutoMapper 
            mMapper = new MapperConfiguration(config =>
            {
                // Create UserData entity to UserContext map
                config.CreateMap<UserData, UserContext>()
                      // And the other way around
                      .ReverseMap();
                // Create LoginResultApiModel to UserData map
                config.CreateMap<LoginResultApiModel, UserData>();
            })
            // And create it afterwards
            .CreateMapper();
        }

        #endregion

        #region Mapping Methods

        public UserData Map(UserContext context) => mMapper.Map<UserData>(context);
        public UserContext Map(UserData entity) => mMapper.Map<UserContext>(entity);
        public UserData Map(LoginResultApiModel apiModel) => mMapper.Map<UserData>(apiModel);

        #endregion
    }
}
