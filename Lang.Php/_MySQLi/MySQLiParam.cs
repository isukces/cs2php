namespace Lang.Php
{
    [Skip]
    public class MySQLiParam<T>
    {
        [DirectCall("this")]
        public T Value { get; set; }
    }
}
