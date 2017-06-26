using System;

namespace Lang.Php
{
    [Serializable]
    public class MockMethodException : Exception
    {
        public MockMethodException() :
            base("This is only mock method and it provides no functionality at runtime")
        {

        }
    }
}
