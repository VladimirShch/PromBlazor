namespace Web_Prom.Core.Blazor.DataAccess.Common
{
    public interface IAdapter<Item1, Item2>
    {
        Item2 Convert(Item1 itemFrom);

        // Возможно, этот метод здесь не нужен - не везде требуется проводить конвертацию обратно - например, какие-то справочники, которые нельзя менять из программы
        // Сделать ITwoWayAdapter<Item1, Item2> : IAdapter, куда перенести этот метод?
        Item1 ConvertBack(Item2 itemFrom);
    }
}
