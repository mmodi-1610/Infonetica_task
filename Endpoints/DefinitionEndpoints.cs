using FlowAPI.Domain;
using FlowAPI.Logic;

namespace FlowAPI.Endpoints;

public static class DefinitionEndpoints
{
    public static void RegisterDefinitionEndpoints(this WebApplication app)
    {
        // Create new workflow definition
        app.MapPost("/flow/define", (FlowSchema schema, ProcessEngine engine) =>
        {
            try
            {
                var id = engine.AddDefinition(schema);
                return Results.Ok(new { message = "Workflow defined.", id });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        // Retrieve a definition by ID
        app.MapGet("/flow/define/{id}", (string id, ProcessEngine engine) =>
        {
            var schema = engine.GetSchema(id);
            return schema != null ? Results.Ok(schema) : Results.NotFound();
        });
    }
}
