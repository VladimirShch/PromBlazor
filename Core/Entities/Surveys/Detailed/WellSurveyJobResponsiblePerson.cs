using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    // TODO: видимо, протечка абстракции. Какой login на уровне предметной области?
    // У исполнителя работ в общем случае не обязано быть никакого логина.
    // Только у тех, кто работает конкретно в этом приложении
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    public record WellSurveyJobResponsiblePerson(int Id, string Name, int Organization, string Login) : EntityHeader(Id, Name);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
}
