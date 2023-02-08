using Geolog.Contracts;
using System;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.Detailed
{
    public class WellSurveyRepository : IWellSurveyRepository
    {
        private readonly IIssl _isslService;
        private readonly IAdapter<WellClass.Well.IsslCs, WellSurvey> _wellSurveyAdapter;
        private readonly UserCredentials _userCredentials;

        public WellSurveyRepository(UserCredentials userCredentials, IIssl isslService, IAdapter<WellClass.Well.IsslCs, WellSurvey> wellSurveyAdapter)
        {
            _isslService = isslService ?? throw new ArgumentNullException(nameof(isslService));
            _wellSurveyAdapter = wellSurveyAdapter ?? throw new ArgumentNullException(nameof(wellSurveyAdapter));
            _userCredentials = userCredentials;
        }

        public async Task<WellSurvey> Get(string fieldId, string surveyId)
        {
            int field = Convert.ToInt32(fieldId);
            int survey = Convert.ToInt32(surveyId);
            WellClass.Well.IsslCs? wellSurveysDto = await _isslService.LoadWellIsslAsync(survey);
            if(wellSurveysDto?.Glub is not null)
            {
                    wellSurveysDto.Glub = await _isslService.CalclHliqudAsync("MockName", "1111", field, wellSurveysDto.Glub, wellSurveysDto.ID);                
            }
            WellSurvey wellSurveys = _wellSurveyAdapter.Convert(wellSurveysDto);

            return wellSurveys;
        }

        public async Task Set(string fieldId, string wellId, WellSurvey survey)
        {
            int field = Convert.ToInt32(fieldId);
            WellClass.Well.IsslCs? wellSurveysDto = _wellSurveyAdapter.ConvertBack(survey);
            WellClass.Well wellDto = new()
            {
                UWI = wellId,
                Issl = new System.Collections.Generic.List<WellClass.Well.IsslCs> { wellSurveysDto }
            };
            
            // Обработать. Бросить ошибку более высокого уровня, ошибку репозитория
            try
            {
                _ = await _isslService.SaveWellIsslAsync(_userCredentials.Username, _userCredentials.Password, field, wellDto);
            }
            catch(Exception e)
            {
                throw;
            }
           
        }
    }
}
