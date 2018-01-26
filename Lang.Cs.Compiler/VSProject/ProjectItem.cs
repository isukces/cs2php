namespace Lang.Cs.Compiler.VSProject
{
    public class ProjectItem
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="name"></param>
        ///     <param name="buildAction"></param>
        /// </summary>
        public ProjectItem(string name, BuildActions buildAction)
        {
            Name        = name;
            BuildAction = buildAction;
        }

        /// <summary>
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public ProjectItem[] SubItems { get; set; }

        /// <summary>
        /// </summary>
        public BuildActions BuildAction { get; set; }

        private string _name = string.Empty;
    }
}