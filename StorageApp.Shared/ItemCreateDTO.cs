namespace StorageApp.Shared
{
    public class ItemCreateDTO
    {
        public string Name { get; set; }

        public ContainerDTO Container { get; set; }

        public string Note { get; set; }
    }
}