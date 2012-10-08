namespace FIfairyDomain
{
    public class PrePatEmailFileInfo
    {
        public virtual string Name { get; set; }

        public virtual int Length { get; set; }

        #region equality 
        public bool Equals(PrePatEmailFileInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name) && other.Length == Length;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PrePatEmailFileInfo)) return false;
            return Equals((PrePatEmailFileInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ Length;
            }
        }
        #endregion
    }
}