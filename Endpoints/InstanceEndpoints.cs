using FlowAPI.Logic;

namespace FlowAPI.Endpoints;

public static class InstanceEndpoints
{
    public static void RegisterInstanceEndpoints(this WebApplication app)
    {
        // Start a new workflow instance
        app.MapPost("/flow/start/{schemaId}", (string schemaId, ProcessEngine engine) =>
        {
            try
            {
                var instance = engine.InitializeInstance(schemaId);
                return Results.Ok(instance);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        // Trigger a transition
        app.MapPost("/flow/instance/{id}/transition/{transitionId}", (string id, string transitionId, ProcessEngine engine) =>
        {
            try
            {
                engine.ApplyTransition(id, transitionId);
                return Results.Ok(new { message = "Transition successful." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        // Inspect current state of an instance
        app.MapGet("/flow/instance/{id}", (string id, ProcessEngine engine) =>
        {
            var instance = engine.GetInstance(id);
            return instance != null ? Results.Ok(instance) : Results.NotFound();
        });
    }
}
