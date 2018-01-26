namespace Lang.Php.Compiler
{
    public class TranslationMessage
    {
        public TranslationMessage(string text, MessageLevels level)
        {
            Text  = text;
            Level = level;
        }
        public string        Text  { get; private set; }
        public MessageLevels Level { get; private set; }

    }

}
