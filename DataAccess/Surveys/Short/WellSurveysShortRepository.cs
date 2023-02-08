using Geolog.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Short;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.Short
{
    public class WellSurveysShortRepository : IWellSurveysShortRepository
    {
        private readonly IIssl _isslService;
        private readonly UserCredentials _userCredentials;
        private readonly IAdapter<IEnumerable<WellClass.Well.IsslCs>?, ICollection<WellSurveyShort>> _wellSurveysShortAdapter;

        public WellSurveysShortRepository(IIssl isslService, UserCredentials userCredentials, IAdapter<IEnumerable<WellClass.Well.IsslCs>?, ICollection<WellSurveyShort>> wellSurveysShortAdapter)
        {
            _isslService = isslService ?? throw new ArgumentNullException(nameof(isslService));
            _userCredentials = userCredentials ?? throw new ArgumentNullException(nameof(userCredentials));
            _wellSurveysShortAdapter = wellSurveysShortAdapter ?? throw new ArgumentNullException(nameof(wellSurveysShortAdapter));
        }

        public async Task<IEnumerable<WellSurveyShort>> GetAll(string fieldId, string uwi)
        {
            int field = Convert.ToInt32(fieldId);
            IEnumerable<WellClass.Well.IsslCs>? wellSurveysDto = (await _isslService.LoadIsslListAsync(_userCredentials.Username, _userCredentials.Password, field, new string[] { uwi }, DateTime.Parse("1900-01-01"), DateTime.Parse("1900-01-01")))?.FirstOrDefault()?.Issl;
            
            IEnumerable<WellSurveyShort> wellSurveysShort = _wellSurveysShortAdapter.Convert(wellSurveysDto);              

            return wellSurveysShort;
        }
    }
}
