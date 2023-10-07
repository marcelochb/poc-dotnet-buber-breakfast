using ErrorOr;

namespace BuberBreakfast.ServicesErrors;

public static class Errors 
{
  public static class Breakfasts
  {
    public static Error NotFound => Error.NotFound(
      code: "Breakfasts.NotFound",
      description: "Breakfast not found."
    );
  }
}