namespace Nfh.Dal.Xml.Converters
{
    internal interface IConverter
    {
        public TTo Convert<TFrom, TTo>(TFrom model)
            where TFrom : notnull
            where TTo : notnull;
    }
}
