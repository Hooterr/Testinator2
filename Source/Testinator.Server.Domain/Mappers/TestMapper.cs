using AutoMapper;
using System.Collections.Generic;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The mapper for every model related to tests
    /// </summary>
    public class TestMapper
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
        public TestMapper()
        {
            // Configure the AutoMapper 
            mMapper = new MapperConfiguration(config =>
            {
                // Create TestFileContext to TestListItemViewModel map
                config.CreateMap<TestFileContext, TestListItemViewModel>()
                      // And the other way around
                      .ReverseMap();
            })
            // And create it afterwards
            .CreateMapper();
        }

        #endregion

        #region Mapping Methods

        public TestListItemViewModel Map(TestFileContext context) => mMapper.Map<TestListItemViewModel>(context);
        public TestFileContext Map(TestListItemViewModel viewModel) => mMapper.Map<TestFileContext>(viewModel);

        public List<TestListItemViewModel> Map(IEnumerable<TestFileContext> contexts) => mMapper.Map<List<TestListItemViewModel>>(contexts);
        public List<TestFileContext> Map(IEnumerable<TestListItemViewModel> viewModels) => mMapper.Map<List<TestFileContext>>(viewModels);

        #endregion
    }
}
