
namespace GMTK_2023.Utils
{
    public delegate void ChangeEvent<T>(T oldValue, T newValue);

    public class ValueObserver<T>
    {
        public event ChangeEvent<T> OnValueChanged;

        public T Value
        {
            get => m_value;
            set
            {
                if (!m_value.Equals(value))
                {
                    T oldValue = m_value;
                    m_value = value;
                    OnValueChanged?.Invoke(oldValue, value);
                }
            }
        }

        private T m_value = default;

        public ValueObserver() { }

        public ValueObserver(T value)
        {
            m_value = value;
        }
    }
}
