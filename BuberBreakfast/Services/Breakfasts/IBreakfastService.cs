using BuberBreakfast.Contracts.Breakfast;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
  Task<BreakfastResponse> CreateBreakfastAsync(CreateBreakfastRequest request);
  Task<BreakfastResponse> GetBreakfastAsync(Guid id);
  Task<BreakfastResponse> UpsertBreakfastAsync(Guid id, UpsertBreakfastRequest request);
  Task DeleteBreakfastAsync(Guid id);
}