namespace PixelBattles.API.Server
{
    public class Error
    {
        public string Code { get; private set; }

        public string Description { get; private set; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
        
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Error p = obj as Error;
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Code == p.Code) && (Description == p.Description);
        }

        public bool Equals(Error p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Code == p.Code) && (Description == p.Description);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode() ^ Description.GetHashCode();
        }

        public static bool operator ==(Error a, Error b)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Code == b.Code && a.Description == b.Description;
        }

        public static bool operator !=(Error a, Error b)
        {
            return !(a == b);
        }
    }
}
