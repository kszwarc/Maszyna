namespace Maszyna.Models
{
    public struct TuringElement<T>
    {
        public TuringMachineModifiedElements Element { get; set; }
        public T Values { get; set; }
    }
}
