
namespace DevoirMaison2021
{
    class Modifier
    {
        public enum EnumModifierType { ATTACK_SPEED, HIT_DELAY }
        public EnumModifierType ModifierType { get; set; }
        public object value;
        public long Duration { get; set; }
        public long Time { get; set; } = 0;
        public Modifier(EnumModifierType _type, object _value, long _duration = -1){
            ModifierType = _type;
            value = _value;
            Duration = _duration;
        }
        public void SetModifier<T>(T _value)
        {
            value = _value;
        }
        public T GetModifier<T>()
        {
            return (T)value;
        }
    }
}
