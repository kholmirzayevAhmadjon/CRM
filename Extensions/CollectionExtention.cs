using CRM_system_for_training_centers.Models.Commons;

namespace CRM_system_for_training_centers.Extensions;

public static class CollectionExtention
{
    public static T Create<T>(this List<T> values, T model) where T : Auditable
    {
        var lastId = values.Count == 0 ? 1 : values.Last().Id + 1;
        model.Id = lastId;
        values.Add(model);
        return values.Last();
    }
}
