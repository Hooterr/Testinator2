using AutoMapper;
using System.Collections.Generic;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The mapper for every model related to grading
    /// </summary>
    public class GradingMapper
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
        public GradingMapper()
        {
            // Configure the AutoMapper 
            mMapper = new MapperConfiguration(config =>
            {
                // Create TestFileContext to TestListItemViewModel map
                config.CreateMap<GradingPresetFileContext, GradingPresetListItemViewModel>()
                      .ForMember(vm => vm.AbsoluteFilePath, options => options.MapFrom(context => context.FilePath))
                      // And the other way around
                      .ReverseMap();
            })
            // And create it afterwards
            .CreateMapper();
        }

        #endregion

        #region Mapping Methods

        public GradingPresetListItemViewModel Map(GradingPresetFileContext context) => mMapper.Map<GradingPresetListItemViewModel>(context);
        public GradingPresetFileContext Map(GradingPresetListItemViewModel viewModel) => mMapper.Map<GradingPresetFileContext>(viewModel);

        public List<GradingPresetListItemViewModel> Map(IEnumerable<GradingPresetFileContext> contexts) => mMapper.Map<List<GradingPresetListItemViewModel>>(contexts);
        public List<GradingPresetFileContext> Map(IEnumerable<GradingPresetListItemViewModel> viewModels) => mMapper.Map<List<GradingPresetFileContext>>(viewModels);

        #endregion
    }
}
