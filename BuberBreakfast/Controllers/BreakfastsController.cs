using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController
{
  private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
  public IActionResult CreateBreakfast(CreateBreakfastRequest request)
  {
    var breakfast = new Breakfast(Guid.NewGuid(),
                                  request.Name,
                                  request.Description,
                                  request.StartDateTime,
                                  request.EndDateTime,
                                  DateTime.UtcNow,
                                  request.Savory,
                                  request.Sweet);
                                  
    ErrorOr<Created> createBreakfast = _breakfastService.CreateBreakfast(breakfast);
    if (createBreakfast.IsError)
    {
        return Problem(createBreakfast.Errors);
    }

    return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast));
  }
  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakFast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors));
    }

  [HttpPut("{id:guid}")]
  public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
  {
    var breakfast = new Breakfast(
      id,
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      DateTime.UtcNow,
      request.Savory,
      request.Sweet);

    ErrorOr<Updated> upsertBreakfast = _breakfastService.UpsertBreakfast(breakfast);
    
    return upsertBreakfast.Match(
        breakfast => NoContent(),
        errors => Problem(errors));
    //TODO: return 201 if new breakfast was created

    // return NoContent();
  }
  [HttpDelete("{id:guid}")]
  public IActionResult DeleteBreakfast(Guid id)
  {
    ErrorOr<Deleted> deleteBreakfast = _breakfastService.DeleteBreakfast(id);

    return deleteBreakfast.Match(
        breakfast => NoContent(),
        errors => Problem(errors));

  }

  private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
  {
      return new BreakfastResponse(breakfast.Id,
                                            breakfast.Name,
                                            breakfast.Description,
                                            breakfast.StartDateTime,
                                            breakfast.EndDateTime,
                                            breakfast.LastModifiedDateTime,
                                            breakfast.Savory,
                                            breakfast.Sweet);
  }

  
}

