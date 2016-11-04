namespace ZplLabels.ZPL
{
    public class NativeZplGenerator : FieldGenerator
    {
        private string _zpl;
        
        public override string ToString()
        {
            return _zpl;
        }

        public NativeZplGenerator WithData(string value)
        {
            _zpl = value;
            return this;
        }
    }
}
