namespace Vinalyze_api
{
    public class Wine
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; } // this will provide a short description about the history, creator, location, etc.
        public string? FlavorProfile { get; set; } // this will be a description of the flavor profile of the wine
    }
}
